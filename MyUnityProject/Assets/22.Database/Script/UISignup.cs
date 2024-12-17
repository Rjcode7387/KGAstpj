using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISignup : MonoBehaviour
{
    public TMP_InputField email;
    public TMP_InputField userName;
    public TMP_InputField password;

    public Button signUpButton;
    public Button loginButton;
    private void Awake()
    {
        signUpButton.onClick.AddListener(SignUpButtonClick);
        loginButton.onClick.AddListener(LogInButtonClick);
    }
    private void OnEnable()
    {
        signUpButton.interactable = true;
    }
    private void LogInButtonClick()
    {
        UIManager.Instance.PageOpen("LogIn");
    }
    private void SignUpButtonClick()
    {
        DatabaseManager.Instance.SignUp(email.text, userName.text, password.text);
        signUpButton.interactable = false;
    }
}
