using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUserInfo : MonoBehaviour
{
    public TextMeshProUGUI userName;
    public TextMeshProUGUI characterClass;
    public TextMeshProUGUI level;

    public Button LevelUpButton;
    public Button deleteButton;    
    public Button rankingButton;

    private UserData userData;

    private void Awake()
    {
        LevelUpButton.onClick.AddListener(LevelUpButtonClick);
        deleteButton.onClick.AddListener(DeleteButtonClick);
        rankingButton.onClick.AddListener(RankingButtonClick);
    }
    public void UserInfoOpen(UserData userData)
    {
        this.userData = userData;

        userName.text = userData.userName;
        characterClass.text = userData.characterClass;
        level.text = $"Lv{userData.level}";
    }
    private void LevelUpButtonClick()
    {
        if (userData != null)
        {
            DatabaseManager.Instance.LevelUp(userData.email);
        }
    }

    private void DeleteButtonClick()
    {
        Debug.Log("Delete button clicked");
        UIManager.Instance.PageOpen("Popup");
        UIManager.Instance.popup.PopupOpen(
      "ȸ��Ż��",
           "���� Ż���Ͻðڽ��ϱ�?",
           () => {
               // Ȯ�� ��ư Ŭ�� �� ����
               if (userData != null)
               {
                   DatabaseManager.Instance.DeleteAccount(userData.email);
               }
           });
    }

    private void RankingButtonClick()
    {
        UIManager.Instance.PageOpen("Rangking");
    }


}
