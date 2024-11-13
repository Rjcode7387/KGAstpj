using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxcontroler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 객체가 캐릭터 컨트롤러를 가지고 있는지 확인
        if (collision.gameObject.GetComponent<CharacterController>() != null)
        {
            // 상호작용 로직을 여기에 추가
            InteractWithPlayer();
        }
    }

    private void InteractWithPlayer()
    {
        // 플레이어와의 상호작용 로직 구현
        Debug.Log("Player와 상호작용");
    }

}
