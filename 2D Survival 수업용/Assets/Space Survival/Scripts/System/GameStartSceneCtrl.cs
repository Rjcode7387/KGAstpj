using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartSceneCtrl : MonoBehaviour
{
    public static int killCount;

    public Button startButton;

    private void Awake()
    {
        startButton.onClick.AddListener(OnStart);
    }

    public void OnStart()
    {
        GameManager.Instance.ReStart();
    }
}
