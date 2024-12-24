using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Experimental.GlobalIllumination;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PanelManager : MonoBehaviourPunCallbacks
{
	public static PanelManager Instance;

	public LoginPanel login;
	public MenuPanel menu;
	public LobbyPanel lobby;
	public RoomPanel room;

	
	Dictionary<string, GameObject> panelDic;

	private void Awake() {
		Instance = this;
		panelDic = new Dictionary<string, GameObject>()
		{
			{ "Login", login.gameObject},
			{ "Menu", menu.gameObject},
			{ "Lobby", lobby.gameObject},
			{ "Room", room.gameObject }
		};

		PanelOpen("Login");
	}

	public void PanelOpen(string panelName) {
		foreach (var row in panelDic) {
			row.Value.SetActive(row.Key == panelName);
		}
	}

    public override void OnEnable()
    {
		base.OnEnable();

        //MonoBehaviourPunCallbacks�� ����� Ŭ������ OnEnable��
		//������ �� �� �� �θ��� OnEnable�� ȣ���ؾ���
		//print("����");
    }

    // ���� ������ ���� �Ǿ����� ȣ��
    public override void OnConnected()//�����Լ� ����� ������ base.OnConnected ���ص� ��
    {
		PanelOpen("Menu");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
		LogManager.Log($"�α׾ƿ��ǹ���! : {cause}");
		PanelOpen("Login");
    }
    //���� ���� �Ͽ����� ȣ��
    public override void OnCreatedRoom()
    {
		PanelOpen("Room");
    }
	//�濡 ����
    public override void OnJoinedRoom()
    {
		PanelOpen("Room");
        Hashtable roomCustomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
        if (roomCustomProperties.ContainsKey("Difficulty"))
        {
            room.OnDifficultyChange((Difficulty)roomCustomProperties["Difficulty"]);
        }

    }
    //�濡�� ������ �� ȣ��
    public override void OnLeftRoom()
    {
		PanelOpen("Menu");
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        room.JoinPlayer(newPlayer);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        room.LeavePlayer(otherPlayer);
    }
    public override void OnJoinedLobby()
    {
		PanelOpen("Lobby");
    }
    public override void OnLeftLobby()
    {
		PanelOpen("Menu");
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        lobby.UpdataRoomList(roomList);
    }
    public override void OnRoomPropertiesUpdate(Hashtable P)
    {
        if (P.ContainsKey("Difficulty"))
        {
            room.OnDifficultyChange((Difficulty)P["Difficulty"]);
        }
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (changedProps.ContainsKey("CharacterSelect"))
        {
            room.OnCharacterSelectChange(targetPlayer, changedProps);
        }
    }
}
	

	

