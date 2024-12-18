using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebassManager : MonoBehaviour
{
    public static FirebassManager instance { get; private set; }

    public FirebaseApp App { get; private set; }
    public FirebaseAuth Auth { get; private set; }
    public FirebaseDatabase DB { get; private set; }

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
        }
        //�ʱ�ȭ ����
        else
        {
            Debug.LogWarning($"���̾�̽� �ʱ�ȭ ���� : {status}");
        }
    
    }

    //ȸ������ �Լ�
    public async void Create(string email, string passwd, Action<FirebaseUser> callback = null)
    {
        try
        {
            var result = await Auth.CreateUserWithEmailAndPasswordAsync(email, passwd);




            callback?.Invoke(result.User);
        }
        catch(FirebaseException e)
        {
            Debug.LogError(e.Message);
        }
    }
    //�α���
    public async void SignIn(string email, string passwd, Action<FirebaseUser> callback = null)
    {
        try
        {
            var result = await Auth.SignInWithEmailAndPasswordAsync(email, passwd);
            callback?.Invoke(result.User);
        }
        catch (FirebaseException e)
        {
            Debug.LogError(e.Message);
        }
    }
}
