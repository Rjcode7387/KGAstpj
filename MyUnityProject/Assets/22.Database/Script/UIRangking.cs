using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRangking : MonoBehaviour
{
    public TextMeshProUGUI rangkingText;
    public Button backButton;


    private void Awake()
    {
        backButton.onClick.AddListener(BackButtonClick);
    }

    private void OnEnable()
    {
        DatabaseManager.Instance.GetRanking(UpdateRankingUI);
    }

    private void BackButtonClick()
    {
        UIManager.Instance.PageOpen("UserInfo");
    }

    public void UpdateRankingUI(string rankingData)
    {
        rangkingText.text = rankingData;
    }
}

