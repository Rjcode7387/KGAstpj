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
        //파이어베이스 초기화 상태 체크. 비동기(Async)함수이므로 완료될 때 까지 대기
        DependencyStatus status = await FirebaseApp.CheckAndFixDependenciesAsync();
        //초기화 성공
        if (status == DependencyStatus.Available)
        {
            App = FirebaseApp.DefaultInstance;
            Auth = FirebaseAuth.DefaultInstance;
            DB = FirebaseDatabase.DefaultInstance;  
        }
        //초기화 실패
        else
        {
            Debug.LogWarning($"파이어베이스 초기화 실패 : {status}");
        }
    
    }

    //회원가입 함수
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
    //로그인
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
