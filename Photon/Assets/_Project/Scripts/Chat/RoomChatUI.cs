using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomChatUI : MonoBehaviour
{
    public Text roomNameLabel;
    public InputField messageInput;
    public Button sendButton;
    public RectTransform messageContent;
    public GameObject messageEntryPrefab;
    public ScrollRect scrollRect;

    private void Awake()
    {
        sendButton.onClick.RemoveAllListeners();
        sendButton.onClick.AddListener(SendChatMessage);
    }


    public void SendChatMessage()
    {
        string message = messageInput.text;
        Debug.Log($"Trying to send: {message}"); 

        if (string.IsNullOrEmpty(message)) return;

        RoomChatManager.Instance.SendChatMessage(message);
        messageInput.text = "";
        messageInput.ActivateInputField();
    }

    public void ReceiveChatMessage(string nickname, string message)
    {
        Debug.Log($"Creating message UI - Nickname: {nickname}, Message: {message}"); // UI 생성 확인

        GameObject entry = Instantiate(messageEntryPrefab, messageContent);
        Text nicknameText = entry.transform.Find("Nickname").GetComponent<Text>();
        Text messageText = entry.transform.Find("Message").GetComponent<Text>();

        nicknameText.text = nickname;
        messageText.text = message;

        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }
}
