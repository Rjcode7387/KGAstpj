using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Transform playerPosition;
    public static bool isGameReady;


    private void Awake()
    {
        Instance = this;
    }
    //photon에서 컨트롤 동기화 하는 방법
    //1. 프리펩에photonview 컴포넌트를 붙이고 , photonNetwork.Instantiate를 통해 원격 클라이언트들에게도 
    //동기화된 오브젝트를 생성하도록 함.
    //2. photonview가 observing 할수 있도록 view 컴포넌트를 부탁
    //3.


    private IEnumerator Start()
    {
        yield return new WaitUntil(() => isGameReady);
        yield return new WaitForSeconds(1f);

        //GetPlayerNumber 확장함수 : 포톤 네트워크에 연결된 다른 플레이어들 사이에 동기화 된 플레이어 
        //번호. acter number와 다름.(scene마다 선착순으로 0~플레이어 수만큼 부여됨)
        //GetPlayerNumber 확장함수가 동작하기 위해서는 씬에 playerNumbering 컴포넌트가 필요하다.
        int playerNumber = PhotonNetwork.LocalPlayer.GetPlayerNumber();
        Vector3 playerPos = playerPosition.GetChild(playerNumber).position;
        GameObject playerObj = PhotonNetwork.Instantiate("Player", playerPos, Quaternion.identity);
        playerObj.name = $"Player {playerNumber}";


        //이 밑에서는 내가 MasterClient가 아니면 동작하지 안흥ㅁ
        if (false == PhotonNetwork.IsMasterClient)
        {
            yield break;
        }
        //Master Client만 5초맏 Pill을 PhotonNetwork를 통해 Instantiate.
        while (true)
        {
            //PhotonNetwork.Instantiate를 통해 생성할 경우, position과 rotation이 반드시 필요
            Vector3 spawnPos = Random.insideUnitSphere * 15;
            spawnPos.y = 0;
            Quaternion spawnRot = Quaternion.Euler(0, Random.Range(0, 180f),0);

            //각 pill마다 random color(color)와 random healAmount(float)를 주입하고싶으면?

            Vector3 color = new Vector3(Random.value, Random.value, Random.value);
            float healAmount = Random.Range(10f, 30f);


            PhotonNetwork.Instantiate("Pill",spawnPos, spawnRot, 
                data:new object[] { color,healAmount});

            yield return new WaitForSeconds(5f);
        }

    }
}
