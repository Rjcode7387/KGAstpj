using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRangking : UIPage
{
    public TextMeshProUGUI[] rankTexts;      
    public TextMeshProUGUI[] nameTexts;      
    public TextMeshProUGUI[] levelTexts;     
    public TextMeshProUGUI[] expTexts;       
    public Button refreshButton;
    public Button closeButton;

    private void Awake()
    {
        refreshButton.onClick.AddListener(LoadRanking);
        closeButton.onClick.AddListener(() => UIManager.Instance.PageOpen<UIHome>());
    }

    private void OnEnable()
    {
        LoadRanking();
    }
    public void LoadRanking()
    {
        FirebaseManager.instance.TopRanking(10, (rankingList) =>
        {
            if (rankingList != null)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (i < rankTexts.Length && i < nameTexts.Length &&
                        i < levelTexts.Length && i < expTexts.Length)
                    {
                        if (i < rankingList.Count)
                        {
                            rankTexts[i].text = $"#{i + 1}";
                            nameTexts[i].text = rankingList[i].userName;
                            levelTexts[i].text = $"Lv.{rankingList[i].level}";
                            expTexts[i].text = $"EXP: {rankingList[i].exp}";
                        }
                        else
                        {
                            rankTexts[i].text = $"#{i + 1}";
                            nameTexts[i].text = "-";
                            levelTexts[i].text = "-";
                            expTexts[i].text = "-";
                        }
                    }
                }
            }
        });
    }

}
