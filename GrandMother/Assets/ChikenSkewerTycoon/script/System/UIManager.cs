using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
  //��ư�� �ý��� �г� ��ũ��Ʈ ������
  public static UIManager Instance { get; private set; }

    private PauseButton pausedButton;
    private SystemPanel systemPanel;
    public Text moneyText;
    public Player player;


    void Update()
    {
        moneyText.text =$"��{player.dollar}";
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
            moneyText.text = $"��{amount}";
        }
    }

    //�̱��� ���� ���� �� �� ��ȯ�ÿ��� ������ �Ǵ� �޼���
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
    //�ʿ��� ui�� ã�� �޼���
    private void FindUIComponents()
    {
        pausedButton = FindObjectOfType<PauseButton>();
        systemPanel =  FindObjectOfType<SystemPanel>();    
    }



    //ui�ʱ� ���¼���
    private void InitializeUI()
    {
        if (systemPanel != null)
        {
            systemPanel.gameObject.SetActive(false);
        }
    }


    //ui�Ŵ����� ���� �������� ����ϴ� �޼���
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
    /// ������ �Ͻ������ϰ� �ý��� �г��� ǥ��
    /// </summary>
    public void PauseGame()
    {
        
        Time.timeScale = 0f; // ���� �ð� ����
    }

    /// <summary>
    /// ������ �簳�ϰ� �ý��� �г��� ����
    /// </summary>
    public void ResumeGame()
    {
       
        Time.timeScale = 1f; // ���� �ð� ����ȭ
    }

}
