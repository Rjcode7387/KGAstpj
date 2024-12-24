using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChatManager : MonoBehaviour
{
    public static RoomChatManager Instance { get; private set; }
    public RoomChatUI chatUI;

    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
        Debug.Log("Event listener registered");
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
        Debug.Log("Event listener removed");
    }


    public void SendChatMessage(string message)
    {
        if (!PhotonNetwork.InRoom || string.IsNullOrEmpty(message)) return;

        Debug.Log($"Sending message: {message}"); 

        object[] data = new object[] { PhotonNetwork.NickName, message };
        PhotonNetwork.RaiseEvent(
            2,
            data,
            new RaiseEventOptions { Receivers = ReceiverGroup.All },
            SendOptions.SendReliable
        );
    }

    private void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == 2)
        {
            object[] data = (object[])photonEvent.CustomData;
            string nickname = (string)data[0];
            string message = (string)data[1];

            Debug.Log($"Received message - Nickname: {nickname}, Message: {message}"); // 수신된 메시지 확인

            chatUI.ReceiveChatMessage(nickname, message);
        }
    }
}
