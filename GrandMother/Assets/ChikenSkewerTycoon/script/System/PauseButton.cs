using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
   //일시정지를 할려면 버튼이 필요
   private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnPauseButtonClick);
    }

    private void OnPauseButtonClick()
    {
        //유아이 매니저에서 설정
        UIManager.Instance.TogglePauseState();
    }
    private void OnDestroy()
    {
        //오브젝트 파괴시 이벤트 리스너 제거 
        button.onClick.RemoveListener(OnPauseButtonClick);
    }

}
