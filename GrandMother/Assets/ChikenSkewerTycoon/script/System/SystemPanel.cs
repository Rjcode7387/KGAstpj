using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class SystemPanel : MonoBehaviour
{
    //닫는 버튼, 게임 끝 버튼
    private Button closeButton;
    private Button gameoverButton;

    private void Awake()
    {
        FindButton();
        SetupButton();
    }
    private void FindButton()
    {
        Transform Background = transform.Find("Background");
        if (Background != null)
        {
            closeButton = Background.Find("ClosedButton").GetComponent<Button>();
            gameoverButton = Background.Find("GameoverButton").GetComponent<Button>();
        }
    }

    private void SetupButton()
    {
        closeButton?.onClick.AddListener(OnCloseButtonClick);
        gameoverButton?.onClick.AddListener(OnGameQuitButtonClick);
    }
        private void OnCloseButtonClick()
        {
            UIManager.Instance.ResumeGame();
        print("종료?");
        }

        
        private void OnGameQuitButtonClick()
        {
            Application.Quit();
        }

        private void OnDestroy()
        {
            closeButton?.onClick.RemoveListener(OnCloseButtonClick);
            gameoverButton?.onClick.RemoveListener(OnGameQuitButtonClick);
        }

    }
