using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
  //버튼과 시스템 패널 스크립트 넣을곳
  public static UIManager Instance { get; private set; }

    private PauseButton pausedButton;
    private SystemPanel systemPanel;
    public Text moneyText;
    public Player player;


    void Update()
    {
        moneyText.text =$"￦{player.dollar}";
    }
    private void Awake()
    {
        Setup();
        FindUIComponents();
    }

    private void Start()
    {
        InitializeUI();
    }

    public void UpdateMoneyText(float amount)
    {
        if (moneyText != null)
        {
            moneyText.text = $"￦{amount}";
        }
    }

    //싱글톤 패턴 구현 및 씬 전환시에도 유지가 되는 메서드
    private void Setup()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //필요한 ui를 찾는 메서드
    private void FindUIComponents()
    {
        pausedButton = FindObjectOfType<PauseButton>();
        systemPanel =  FindObjectOfType<SystemPanel>();    
    }



    //ui초기 상태설정
    private void InitializeUI()
    {
        if (systemPanel != null)
        {
            systemPanel.gameObject.SetActive(false);
        }
    }


    //ui매니저를 통해 공동으로 사용하는 메서드
    public void TogglePauseState()
    {
        if (systemPanel.gameObject.activeSelf)
        {    
            systemPanel.gameObject.SetActive(false);   
            ResumeGame();
        }
        else
        { 
            systemPanel.gameObject.SetActive(true);
            PauseGame();
        }
    }

    /// <summary>
    /// 게임을 일시정지하고 시스템 패널을 표시
    /// </summary>
    public void PauseGame()
    {
        
        Time.timeScale = 0f; // 게임 시간 정지
    }

    /// <summary>
    /// 게임을 재개하고 시스템 패널을 숨김
    /// </summary>
    public void ResumeGame()
    {
       
        Time.timeScale = 1f; // 게임 시간 정상화
    }

}
