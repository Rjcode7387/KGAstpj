using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Threading.Tasks;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager instance { get; private set; }

    public FirebaseApp App { get; private set; }
    public FirebaseAuth Auth { get; private set; }
    public FirebaseDatabase DB { get; private set; }

    private DatabaseReference usersRef;
    private DatabaseReference messageRef;
    private DatabaseReference roomRef;

    public event Action onLogIn;

    public event Action<Room, bool> onGameStart;

    public event Action<Turn> onTurnProcceed;

    private Room currentRoom;

    private bool isHost;

    public UserData CurrentUserData { get; private set; }   

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        onLogIn += OnLogIn;
    }
    private void OnLogIn()
    {
        messageRef = DB.GetReference($"msg/{Auth.CurrentUser.UserId}");

        messageRef.ChildAdded += OnMessageReceive;
    }

    private void OnMessageReceive(object sender, ChildChangedEventArgs args)
    {
        if (args.DatabaseError == null) // 에러 없음
        {
            string rawJson = args.Snapshot.GetRawJsonValue();

            print(rawJson);
            Message message = JsonConvert.DeserializeObject<Message>(rawJson);

            if (message.isNew)
            {
                if(message.type == MessageType.Message) 
                {
                    var popup = UIManager.Instance.PopupOpen<UIDialogPopup>();
                    popup.SetPopup($"From.{message.sender}", $"{message.message}\n{message.GetSendTime()}");
                }
                else if (message.type == MessageType.Invite)
                {
                    var popup = UIManager.Instance.PopupOpen<UITwoButtonPopup>();
                    popup.SetPopup("초대장", $"{message.sender}님의 게임에 참가하시겠습니까?",
                        ok => { if (ok) { JoinRoom(message.sender); } });
                }
            }
         

            args.Snapshot.Reference.Child("isNew").SetValueAsync(false);

        }
        else                            // 에러 발생
        {
            print("에러 발생!");
        }
    }

    public async Task CreateRoom(Room room)
    {
        currentRoom = room;

        isHost = true;

        roomRef = DB.GetReference($"rooms/{Auth.CurrentUser.UserId}");

        string json = JsonConvert.SerializeObject(room);

        await roomRef.SetRawJsonValueAsync(json);

        roomRef.Child("state").ValueChanged += OnRoomStateChange;
    }

    private void OnRoomStateChange(object sender, ValueChangedEventArgs e)
    {
        object value = e.Snapshot.GetValue(true);

        int state = int.Parse(value.ToString());
        if (state == 1)
        {
            //게임스타트
            onGameStart?.Invoke(currentRoom, true);
            roomRef.Child("turn").ChildAdded += OnTurnAdded;
        }
    }

    private void OnTurnAdded(object sender, ChildChangedEventArgs e)
    {
        string json = e.Snapshot.GetRawJsonValue();
        Turn turn = JsonConvert.DeserializeObject<Turn>(json);
        onTurnProcceed?.Invoke(turn);
    }

    public void SendTurn(int turnCount ,Turn turn)
    {
        turn.isHostTurn = isHost;

        string json = JsonConvert.SerializeObject(turn);

        roomRef.Child($"turn/{turnCount}").SetRawJsonValueAsync(json);
    }

    private async void JoinRoom(string host)
    {
        roomRef = DB.GetReference($"rooms/{host}");

        DataSnapshot roomSnapshot = await roomRef.GetValueAsync();

        string roomJson = roomSnapshot.GetRawJsonValue();

        Room room = JsonConvert.DeserializeObject<Room>(roomJson);

        currentRoom = room;

        isHost = false;

        await roomRef.Child("stste").SetValueAsync((int)RoomState.Playing);

        onGameStart?.Invoke(currentRoom, false);
        roomRef.Child("turn").ChildAdded += OnTurnAdded;
    }
    public void MessageToTarget(string target, Message message)
    {
        DatabaseReference targetRef = DB.GetReference($"msg/{target}");
        string messageJson = JsonConvert.SerializeObject(message);
        targetRef.Child(message.sender + message.sendTime).SetRawJsonValueAsync(messageJson);

    }
    private async void Start()
    {
        //파이어베이스 초기화 상태 체크. 비동기(Async)함수이므로 완료될 때 까지 대기
        DependencyStatus status = await FirebaseApp.CheckAndFixDependenciesAsync();
        //초기화 성공
        if (status == DependencyStatus.Available)
        {
            App = FirebaseApp.DefaultInstance;
            Auth = FirebaseAuth.DefaultInstance;
            DB = FirebaseDatabase.DefaultInstance;

            DataSnapshot dummyData =  await DB.GetReference("users").Child("dummy").GetValueAsync();

            if (dummyData.Exists)
            {
                print(dummyData.GetRawJsonValue());
            }
        }
        //초기화 실패
        else
        {
            Debug.LogWarning($"파이어베이스 초기화 실패 : {status}");
        }
    
    }

    //회원가입 함수
    public async void Create(string email, string passwd, Action<FirebaseUser,UserData> callback = null)
    {
        try
        {
            var result = await Auth.CreateUserWithEmailAndPasswordAsync(email, passwd);

            //생성된 회원의 database reference를 설정
            usersRef = DB.GetReference($"users/{result.User.UserId}");

            //회원의 데이터를 database에 생성
            UserData userData = new UserData(result.User.UserId);
            string userDataJson = JsonConvert.SerializeObject(userData);

            await usersRef.SetRawJsonValueAsync(userDataJson);

            callback?.Invoke(result.User,userData);
        }
        catch(FirebaseException e)
        {
            Debug.LogError(e.Message);
        }
    }
    public async void TopRanking(int count, Action<List<UserData>> callback)
    {
        try
        {
            DatabaseReference reference = DB.GetReference("users");
            var snapshot = await reference.OrderByChild("level").LimitToLast(count).GetValueAsync();

            List<UserData> rankingList = new List<UserData>();

            // 데이터가 있는지 확인
            if (snapshot != null && snapshot.Exists)
            {
                // 역순으로 데이터 처리 (높은 레벨부터)
                foreach (var child in snapshot.Children)
                {
                    try
                    {
                        Dictionary<string, object> dict = child.Value as Dictionary<string, object>;
                        if (dict != null)
                        {
                            UserData userData = new UserData();
                            userData.userId = dict["userId"]?.ToString();
                            userData.userName = dict["userName"]?.ToString();
                            userData.level = Convert.ToInt32(dict["level"]);
                            userData.exp = Convert.ToInt32(dict["exp"]);
                            // 필요한 다른 필드들도 추가...

                            rankingList.Add(userData);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogWarning($"데이터 파싱 오류: {e.Message}");
                        continue;
                    }
                }

                // 레벨 기준 내림차순 정렬
                rankingList.Sort((a, b) => b.level.CompareTo(a.level));
            }

            callback?.Invoke(rankingList);
        }
        catch (Exception e)
        {
            Debug.LogError($"랭킹 조회 실패: {e.Message}");
            callback?.Invoke(new List<UserData>());
        }
    }
    //로그인
    public async void SignIn(string email, string passwd, Action<FirebaseUser,UserData> callback = null)
    {
        try
        {
            var result = await Auth.SignInWithEmailAndPasswordAsync(email, passwd);

            usersRef = DB.GetReference($"users/{result.User.UserId}");

            DataSnapshot userDataValues = await usersRef.GetValueAsync();
            UserData userData = null;
            if (userDataValues.Exists)
            {//DB에 경로가 존재하는지 검사
                string json = userDataValues.GetRawJsonValue();
                userData = JsonConvert.DeserializeObject<UserData>(json);  
            
            }

            CurrentUserData = userData;
            print(userData);
            callback?.Invoke(result.User,userData);
            onLogIn?.Invoke();
        }
        catch (FirebaseException e)
        {
            
            Debug.LogError(e.Message);
        }
    }
    //유저 정보 수정
    public async void UpdateUserProfile(string displayName, Action<FirebaseUser> callback = null)
    {
        //userProfile생성
        UserProfile profile = new UserProfile() { DisplayName = displayName, PhotoUrl = new Uri("https://picsum.photos/120"), };
        await Auth.CurrentUser.UpdateUserProfileAsync(profile);

        UpdateUserData("userName", displayName);

        DataSnapshot userDataValues = await usersRef.GetValueAsync();
        UserData userData = null;
        if (userDataValues.Exists)
        {
            string json = userDataValues.GetRawJsonValue();
            userData = JsonConvert.DeserializeObject<UserData>(json);
        }
        CurrentUserData = userData;

        callback?.Invoke(Auth.CurrentUser);
    }

    public async void UpdateUserData(string childName, object value, Action<object> callback = null)
    {
        DatabaseReference targetRef = usersRef.Child(childName);
        await targetRef.SetValueAsync(value);
        DataSnapshot snapshot = await usersRef.GetValueAsync();

        callback?.Invoke(value);
    }
    public void SignOut()
    {
        Auth.SignOut();
        messageRef.ChildAdded -= OnMessageReceive;
    }
    public async void OrderDataBase(Action<List<UserData>> callback = null)
    {
        try
        {
            // 데이터를 level을 기준으로 정렬하여 가져오기
            Query query = DB.GetReference("users").OrderByChild("level");
            DataSnapshot snapshot = await query.GetValueAsync();

            if (snapshot.Exists)
            {
                List<UserData> userList = new List<UserData>();

                foreach (DataSnapshot child in snapshot.Children)
                {
                    try
                    {
                        string json = child.GetRawJsonValue();
                        if (!string.IsNullOrEmpty(json))
                        {
                            UserData userData = JsonConvert.DeserializeObject<UserData>(json);
                            userList.Add(userData);
                        }
                        else
                        {
                            Debug.LogWarning("Empty or null JSON detected for a user.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"Failed to parse user data: {ex.Message}");
                    }
                }

                userList.Sort((a, b) => b.level.CompareTo(a.level));

                callback?.Invoke(userList);
            }
            else
            {
                Debug.Log("No data found in the database.");
            }
        }
        catch (FirebaseException e)
        {
            Debug.LogError($"Failed to retrieve and sort data: {e.Message}");
        }
    }
}
