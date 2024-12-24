using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviourPun,IPunObservable
{
	private Rigidbody rb;
    private Animator anim;
    private Transform pointer; //ĳ���Ͱ� �Ĵٺ� ��
    private Transform shotPoint;//����ü�� ������ ��



    private float hp = 100;
    private int shotCount = 0;
    public float moveSpeed; // �̵��ӵ�
    public float shotPower;// ����ü �߻� ��
    

    public Text hpText;   //ü���� ǥ���� text
    public Text shotText; // �߻� Ƚ���� ǥ���� text

    public Bomb bombPrefab;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb  = GetComponent<Rigidbody>();
        pointer = transform.Find("PlayerPointer");
        shotPoint = transform.Find("ShotPoint");
        tag = photonView.IsMine ? "Player" : "Enemy";
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

    //Fire�� ���ؼ� �����ϴ� bomb ��ü�� "���巹Ŀ��" (�����׹� �˰���)�����ؼ� �� Ŭ���̾�Ʈ����
    //���� �����ϰ�, Fire �Լ��� ȣ�� �޴� ������ �¶��ο��� �������� ȣ�����(Remote Procedyre Call)
    [PunRPC]
    private void Fire(Vector3 shotPoint, Vector3 shotDir, PhotonMessageInfo info)
    {
        print($"Fire Procedure called by {info.Sender.NickName}");
        print($"my local time : {PhotonNetwork.Time}");
        print($"server time when procedure called : {info.SentServerTime}");

        //"��������" : (�����׹��� ����) rpc�� ���� ������ �������� ȣ��� �ð����� �׻� �ʱ� ������
        //�ش� �����ð� ��ŭ ��ġ, �Ǵ� ���귮�� �������־�� �ִ��� ���濡���� �÷��̰� ����ȭ�� �� ����.

        //�����ؾ� �� �����ð�
        float lag = (float)(PhotonNetwork.Time - info.SentServerTime);

        Bomb bomb = Instantiate(bombPrefab, shotPoint, Quaternion.identity);
        bomb.rb.AddForce(shotDir * shotPower,ForceMode.Impulse);
        bomb.owner = photonView.Owner;

        //�����������
        bomb.rb.position += bomb.rb.velocity * lag;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //stream�� ���� �ְ� �޴� �����ʹ� server���� �޴� �ð� �������� queue���·� ���޵ȴ�.
        if (stream.IsWriting)
        {
            //�� �����͸� server�� ����
            stream.SendNext(hp);
            stream.SendNext(shotCount);
        }
        else
        {
            hp = (float)stream.ReceiveNext();
            shotCount = (int)stream.ReceiveNext();
        }
    }
}
