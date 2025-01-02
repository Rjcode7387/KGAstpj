using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviourPun,IPunObservable
{
	private Rigidbody rb;
    private Animator anim;
    private Transform pointer; //캐릭터가 쳐다볼 곳
    private Transform shotPoint;//투사체가 생성될 곳



    public float hp = 100;
    private int shotCount = 0;
    public float moveSpeed; 
    public float shotPower;


    public Text hpText;  //체력 표시 text
    public Text shotText; //발사 횟수 표시 text

    public Bomb bombPrefab;

    private Transform eyes;
    private float lastActionTime;
    private const float EYE_DEACTIVATE_DELAY = 1f;
    private bool isDead = false;



    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb  = GetComponent<Rigidbody>();
        pointer = transform.Find("PlayerPointer");
        shotPoint = transform.Find("ShotPoint");
        tag = photonView.IsMine ? "Player" : "Enemy";
        eyes = transform.Find("Renderer/Eyes");

        if (eyes != null)
        {
            eyes.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        shotText.text = shotCount.ToString();
        hpText.text = hp.ToString();
        anim.SetTrigger("Attack");
        if (false == photonView.IsMine) return;
        Move();
        if (Input.GetButtonDown("Fire1"))
        {
            //Fire();
            photonView.RPC("Fire", RpcTarget.All, shotPoint.position, shotPoint.forward);
            shotCount++;
            
        }
        if (photonView.IsMine)
        {
            if (Input.GetAxis("Horizontal") != 0 ||
                Input.GetAxis("Vertical") != 0 ||
                Input.GetButtonDown("Fire1"))
            {
                photonView.RPC("ActivateEyes", RpcTarget.All);
            }

            if (eyes != null && eyes.gameObject.activeSelf &&
                Time.time > lastActionTime + EYE_DEACTIVATE_DELAY)
            {
                photonView.RPC("DeactivateEyes", RpcTarget.All);
            }
        }
        if (!isDead && hp <= 0)
        {
            isDead = true;
            photonView.RPC("Die", RpcTarget.All);
            GameManager.Instance.CheckWinCondition();
        }
    }

    private void FixedUpdate()
    {
       if(false == photonView.IsMine) return ;
        Rotate();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        rb.velocity =  new Vector3(x, 0, z) * moveSpeed;

        anim.SetBool("IsMoving", rb.velocity.magnitude > 0.01f);
    }

    private void Rotate()
    {
        Vector3 pos = rb.position;
        pos.y = 0;
        Vector3 forward = pointer.position - pos;
        rb.angularVelocity = Vector3.zero;
        rb.rotation = Quaternion.LookRotation(forward,Vector3.up);
    }

    private void Hit(float damage)
    {
        hp-=damage;
        
    }
    private void Heal(float amount)
    {
        hp += amount;
        
    }
 

    //Fire를 통해서 생성하는 bomb객체는 "데드레커닝"(추측항법 알고리즘)을 통해서
    //각 클라이언트들이 직접 생성하고, Fire함수를 호출 받는 시점을 온라인으로 호출받음.(Remote Procedure Call)
    [PunRPC]
    private void Fire(Vector3 shotPoint, Vector3 shotDir, PhotonMessageInfo info)
    {
        print($"Fire Procedure called by {info.Sender.NickName}");
        print($"my local time : {PhotonNetwork.Time}");
        print($"server time when procedure called : {info.SentServerTime}");

        //"지연보상" : (추측항법을 위해) RPC를 받은 시점은 서버에서 호출된 시간보다 항상 늦기 때문에,
        //해당 지연시간만큼 위치, 또는 연산량을 보정해주어야 최대한 원격에서의 플레이가 동기화될 수 있음.

        //보정해야 할 지연시간
        float lag = (float)(PhotonNetwork.Time - info.SentServerTime);

        Bomb bomb = Instantiate(bombPrefab, shotPoint, Quaternion.identity);
        bomb.rb.AddForce(shotDir * shotPower,ForceMode.Impulse);
        bomb.owner = photonView.Owner;

        
        bomb.rb.position += bomb.rb.velocity * lag;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //Stream을 통해 주고받는 데이터는 Server에서 받는 시간 기준으로 Queue형태로 전달
        //데이터 자체도 큐
        if (stream.IsWriting)
        {
            //내 데이터를 Server로 보냄
            stream.SendNext(hp);
            stream.SendNext(shotCount);
        }
        else
        {
            hp = (float)stream.ReceiveNext();
            shotCount = (int)stream.ReceiveNext();
        }
    }

    [PunRPC]
    private void ActivateEyes()
    {
        if (eyes != null)
        {
            eyes.gameObject.SetActive(true);
            lastActionTime = Time.time;
        }
    }

    [PunRPC]
    private void DeactivateEyes()
    {
        if (eyes != null)
        {
            eyes.gameObject.SetActive(false);
        }
    }
    [PunRPC]
    private void Die()
    {
        
        enabled = false; 
       
    }
}
