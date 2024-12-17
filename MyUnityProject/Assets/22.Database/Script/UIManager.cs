using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public UISignup signUp;//회원가입 페이지
    public UILogin logIn;//로그인 페이지
    public UIUserInfo userInfo;// 사용자 정보 페이지
    public UIPopup popup;//팝업 (회원 가입완료) 
    public UIRangking rangking;

    private Dictionary<string, GameObject> pages = new Dictionary<string, GameObject>();

    private GameObject currentPage;//현재 열려있는 페이지

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
