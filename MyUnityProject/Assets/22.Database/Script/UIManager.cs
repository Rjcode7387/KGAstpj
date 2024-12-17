using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public UISignup signUp;//ȸ������ ������
    public UILogin logIn;//�α��� ������
    public UIUserInfo userInfo;// ����� ���� ������
    public UIPopup popup;//�˾� (ȸ�� ���ԿϷ�) 
    public UIRangking rangking;

    private Dictionary<string, GameObject> pages = new Dictionary<string, GameObject>();

    private GameObject currentPage;//���� �����ִ� ������

    public static UIManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        pages.Add("Signup", signUp.gameObject);
        pages.Add("LogIn", logIn.gameObject);
        pages.Add("UserInfo", userInfo.gameObject);
        pages.Add("Popup", popup.gameObject);
        pages.Add("Rangking", rangking.gameObject);
    }
    private void Start()
    {
        foreach (GameObject page in pages.Values) 
        { 
            page.SetActive(false);
        }
        PageOpen("LogIn");
    }


    public void PageOpen(string pageName)
    {
        if (pages.ContainsKey(pageName))
        {
            currentPage?.SetActive(false);
            currentPage = pages[pageName];
            currentPage.SetActive(true);
        }
    }
}
