using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISignIn : MonoBehaviour
{
    public TMP_InputField email;
    public TMP_InputField passwd;

    public Button signUpButton;
    public Button signInButton;

    private void Awake()
    {
        signUpButton.onClick.AddListener(SignUpButtonClick);
        signInButton.onClick.AddListener(SignInButtonClick);

    }
    private void SignUpButtonClick()
    {
        UIManager.Instance.PageOpen<UISignUP>();
    }
    private void SignInButtonClick()
    {
        FirebassManager.instance.SignIn(email.text, passwd.text, (user) => 
        { UIManager.Instance.PageOpen<UIHome>().SetInfo(user); });
    }
}
