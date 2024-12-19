using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager instance { get; private set; }

    public FirebaseApp App { get; private set; }
    public FirebaseAuth Auth { get; private set; }
    public FirebaseDatabase DB { get; private set; }

    private DatabaseReference usersRef;

    public UserData CurrentUserData { get; private set; }   

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
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
    }
}
