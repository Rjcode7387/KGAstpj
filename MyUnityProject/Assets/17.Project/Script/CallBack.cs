using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//함수를 호출 하고난 결과 어떤 다른 함수가 호출되야 할때, 그걸 콜백함수라고 부름
public class CallBack : MonoBehaviour
{
    //보통은 특정 함수 수행 후에 다른 함수가 호출되길 원할때 , 그 함수를
    //C#ver:대리자 형태로 넘김.
    //Javascript ver : 함수포이터로 넘김.
    public GameObject destoryTarget;
    public CallBackTestPopup popup;

    public Action callback;
    public void OnDestroyButtonClick()
    {
        popup.ShowPopup(OnYes);
    }
    public void OnYes(bool yes)
    {
        if (yes)
        {
            Destroy(destoryTarget);
        }
        else
        {
            print("생존하셧습니다.");
        }
    }

}
