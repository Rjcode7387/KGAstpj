using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTest : MonoBehaviour
{
    private void Start()
    {
        LogManager.Log(PhotonNetwork.NickName);
        
    }
}