using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILogin : MonoBehaviour
{
    public TMP_InputField email;
    public TMP_InputField password;
    public Button loginButton;
    public Button signUpButton;

    private void Awake()
    {
        loginButton.onClick.AddListener(LoginButtonClick);
        signUpButton.onClick.AddListener(SignUpButtonClick);
    }
    private void OnEnable()
    {
        loginButton.interactable = true;
    }
    private void LoginButtonClick()
    {
        DatabaseManager.Instance.Login(email.text, password.text);
        loginButton.interactable = false;
    }
    private void SignUpButtonClick()
    {
        UIManager.Instance.PageOpen("Signup");
    }
}
