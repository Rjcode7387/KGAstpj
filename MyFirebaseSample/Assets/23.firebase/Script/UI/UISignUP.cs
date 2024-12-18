using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISignUP : MonoBehaviour
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
        if (passwd.text.Length < 6)
        {
            UIManager.Instance.PageOpen<UIDialog>()
                .SetDialog("�˸�", "��� ��ȣ�� 6���� �̻�.", DialogCallback);
        }
        else
        {
            FirebassManager.instance.Create(email.text, passwd.text,CreateCallback);
        }
    }
    private void DialogCallback()
    {
        UIManager.Instance.PageOpen(GetType().Name);
    }
    private void CreateCallback(FirebaseUser user)
    {
        UIManager.Instance.PageOpen<UIDialog>().
            SetDialog("ȸ������ �Ϸ�","ȸ�� ������ �Ϸ� �Ǿ����ϴ�.\n �α��� ���ּ���. ",DialogCallback);
    }
    private void SignInButtonClick()
    {
        UIManager.Instance.PageOpen<UISignIn>();
    }
}
