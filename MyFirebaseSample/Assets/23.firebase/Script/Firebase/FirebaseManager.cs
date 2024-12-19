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
        //���̾�̽� �ʱ�ȭ ���� üũ. �񵿱�(Async)�Լ��̹Ƿ� �Ϸ�� �� ���� ���
        DependencyStatus status = await FirebaseApp.CheckAndFixDependenciesAsync();
        //�ʱ�ȭ ����
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
        //�ʱ�ȭ ����
        else
        {
            Debug.LogWarning($"���̾�̽� �ʱ�ȭ ���� : {status}");
        }
    
    }

    //ȸ������ �Լ�
    public async void Create(string email, string passwd, Action<FirebaseUser,UserData> callback = null)
    {
        try
        {
            var result = await Auth.CreateUserWithEmailAndPasswordAsync(email, passwd);

            //������ ȸ���� database reference�� ����
            usersRef = DB.GetReference($"users/{result.User.UserId}");

            //ȸ���� �����͸� database�� ����
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

            // �����Ͱ� �ִ��� Ȯ��
            if (snapshot != null && snapshot.Exists)
            {
                // �������� ������ ó�� (���� ��������)
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
                            // �ʿ��� �ٸ� �ʵ�鵵 �߰�...

                            rankingList.Add(userData);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogWarning($"������ �Ľ� ����: {e.Message}");
                        continue;
                    }
                }

                // ���� ���� �������� ����
                rankingList.Sort((a, b) => b.level.CompareTo(a.level));
            }

            callback?.Invoke(rankingList);
        }
        catch (Exception e)
        {
            Debug.LogError($"��ŷ ��ȸ ����: {e.Message}");
            callback?.Invoke(new List<UserData>());
        }
    }
    //�α���
    public async void SignIn(string email, string passwd, Action<FirebaseUser,UserData> callback = null)
    {
        try
        {
            var result = await Auth.SignInWithEmailAndPasswordAsync(email, passwd);

            usersRef = DB.GetReference($"users/{result.User.UserId}");

            DataSnapshot userDataValues = await usersRef.GetValueAsync();
            UserData userData = null;
            if (userDataValues.Exists)
            {//DB�� ��ΰ� �����ϴ��� �˻�
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
    //���� ���� ����
    public async void UpdateUserProfile(string displayName, Action<FirebaseUser> callback = null)
    {
        //userProfile����
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
