using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SimpleMouseControl : MonoBehaviour
{
    
    //마우스 커서 화면에 잠금
    //Locked : 화면 중앙에 잠김
    //Confined : 화면 테두리 안에 갇힘
    //None : 안잠금
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //커서 보이는 여부 . Editor의 경우에 따로 설정안해도 Esc 키를 누르면 마우스가 보임
        Cursor.visible = false;
       
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
            
    //    }
    //}

    public static bool isFocusing;

}
