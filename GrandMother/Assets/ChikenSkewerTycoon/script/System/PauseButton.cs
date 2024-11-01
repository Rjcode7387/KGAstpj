using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
   //�Ͻ������� �ҷ��� ��ư�� �ʿ�
   private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnPauseButtonClick);
    }

    private void OnPauseButtonClick()
    {
        //������ �Ŵ������� ����
        UIManager.Instance.TogglePauseState();
    }
    private void OnDestroy()
    {
        //������Ʈ �ı��� �̺�Ʈ ������ ���� 
        button.onClick.RemoveListener(OnPauseButtonClick);
    }

}
