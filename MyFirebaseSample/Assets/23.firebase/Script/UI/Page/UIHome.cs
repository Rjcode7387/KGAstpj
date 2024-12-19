using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class UIHome : UIPage
{
    public Image profileImage;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI displayName;
    public TextMeshProUGUI gold;
    public TextMeshProUGUI jewel;
    public Text expText;
    public Button profileChangeButton;    
    public Button addGoldButton;
    public Button signOutButton;   
    public Slider expSlider;
    public Button addExpButton;
    public Button addjewelButton;
    public Button rankingButton;

    private void Awake()
    {       
        profileChangeButton.onClick.AddListener(ProfileChangeButtonClick);
        addGoldButton.onClick.AddListener(AddGoldButtonClick);
        signOutButton.onClick.AddListener(SignOutButtonClick);
        addExpButton.onClick.AddListener(AddExpButtonClick);
        addjewelButton.onClick.AddListener(AddJewelButtonClick);
        rankingButton.onClick.AddListener(RankingButtonClick);
    }
    private void RankingButtonClick()
    {
        UIManager.Instance.PageOpen<UIRangking>();
    }
    private void AddJewelButtonClick()
    {
        UserData data = FirebaseManager.instance.CurrentUserData;
        data.jewel += 10;

        FirebaseManager.instance.UpdateUserData("jewel", data.jewel, (x) => { SetUserData(data); });
    }
    private void AddExpButtonClick()
    {
        UserData data = FirebaseManager.instance.CurrentUserData;
        Debug.Log($"현재 경험치: {data.exp}, 레벨: {data.level}"); // 현재 상태 확인

        data.exp += 20;
        Debug.Log($"경험치 추가 후: {data.exp}"); // 경험치 추가 확인

        bool isLevelUp = false;
        int nextLevelExp = data.level * 100;

        if (data.exp >= nextLevelExp)
        {
            data.level++;
            isLevelUp = true;
            Debug.Log($"레벨업! 새 레벨: {data.level}"); // 레벨업 확인
            FirebaseManager.instance.UpdateUserData("level", data.level, null);
        }

        FirebaseManager.instance.UpdateUserData("exp", data.exp, (x) =>
        {
            Debug.Log($"경험치 업데이트 완료: {data.exp}"); // Firebase 업데이트 확인
            if (isLevelUp)
            {
                expSlider.value = 0f;
            }
            SetUserData(data);
        });
    }
    private void SignOutButtonClick()
    {
        FirebaseManager.instance.SignOut();
        UIManager.Instance.PageOpen<UIMain>();
    }
    private void AddGoldButtonClick()
    {
        UserData data = FirebaseManager.instance.CurrentUserData;
        data.gold += 10;

        FirebaseManager.instance.UpdateUserData("gold", data.gold, (x) => { SetUserData(data); });
    }
    public void ProfileChangeButtonClick()
    {
        UIManager.Instance.PopupOpen<UIInputFieldPopup>().
            SetPopup("닉네임 변경", "변경할 닉네임 입력.",ProfileChangeCallback);
    }
    private void ProfileChangeCallback(string newName)
    {
        FirebaseManager.instance.UpdateUserProfile(newName, SetInfo);
    }

    public void SetInfo(FirebaseUser user)
    {
        displayName.text = user.DisplayName;
        if (user.PhotoUrl != null)
        {
            SetPhoto(user.PhotoUrl.AbsoluteUri);
        }
    }

    public async void SetPhoto(string url)
    {
        if (Uri.TryCreate(url, UriKind.Absolute, out Uri uri))
        {
            using (HttpClient client = new HttpClient())
            {
                byte[] response = await client.GetByteArrayAsync(url);
                Texture2D texture = new Texture2D(1, 1);
                texture.LoadImage(response);
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                    new Vector2(0.5f, 0.5f));
                profileImage.sprite = sprite;
            }
        }
        else
        {
            profileImage.sprite = null;
        }
    }
    public void SetUserData(UserData userData)
    {
        gold.text = userData.gold.ToString();
        jewel.text = userData.jewel.ToString();
        levelText.text = $"{userData.level}";
        expText.text = $"{userData.exp} / {userData.maxExp}";
       
        int previousLevelExp = (userData.level - 1) * 100;
        
        int currentLevelExp = userData.exp - previousLevelExp;
       
        int requiredExp = 100; 

        expSlider.value = (float)currentLevelExp / requiredExp;

    }
}
