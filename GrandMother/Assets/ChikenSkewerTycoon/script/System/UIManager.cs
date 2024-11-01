using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
  //��ư�� �ý��� �г� ��ũ��Ʈ ������
  public static UIManager Instance { get; private set; }

    private PauseButton pausedButton;
    private SystemPanel systemPanel;

    private void Awake()
    {
        Setup();
        FindUIComponents();
    }

    private void Start()
    {
        InitializeUI();
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
            
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    /// <summary>
    /// ������ �Ͻ������ϰ� �ý��� �г��� ǥ��
    /// </summary>
    public void PauseGame()
    {
        systemPanel.gameObject.SetActive(true);
        Time.timeScale = 0f; // ���� �ð� ����
    }

    /// <summary>
    /// ������ �簳�ϰ� �ý��� �г��� ����
    /// </summary>
    public void ResumeGame()
    {
        systemPanel.gameObject.SetActive(false);
        Time.timeScale = 1f; // ���� �ð� ����ȭ
    }

}
