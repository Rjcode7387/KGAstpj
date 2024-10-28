using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class GameOverSceneCtrl : MonoBehaviour
{
    public static int killCount;

    public Button restartButton;

    private void Awake()
    {
        restartButton.onClick.AddListener(OnRestart);

        
    }

    public void OnRestart()
    {
        GameManager.Instance.ReStart();
    }
}
