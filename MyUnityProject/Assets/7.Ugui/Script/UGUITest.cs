using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UGUITest : MonoBehaviour
{
    public Image image;
    public Text text;
    public TextMeshProUGUI tmpugui;

    private void Start()
    {
        image.color = Color.black;
        text.text = "Hello UGUI";
        tmpugui.text = "Hello TMP"; //런타임에서 색깔 밑 텍스트 문구 바꾸기
    }


}
