using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    
    //public float maxHP = 10f;//�ϼ�
    private float maxHp;
    public float hp = 10f;//ü��
    public float damage = 10f;//���ݷ�
    public float moveSpeed = 3f;//�̵��ӵ�

    //�ʰ��
    public float hpAmount {  get { return hp/maxHp; } }//���� ���Ǵ� �׸��� ������Ƽ�� �����
    //Getter/Setter


    private Transform target;//�������
    public Image Hpbar;

    private Rigidbody2D rb;
    public ParticleSystem impactParticle;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }


    private IEnumerator Start()
    {
        GameManager.Instance.enemies.Add(this); //�� ����Ʈ�� �ڱ� �ڽ��� Add
        yield return null;//�������� ����
        target = GameManager.Instance.player.transform;
        maxHp = hp;

    }

    private void Update()
    {
        if (target == null) return;
        Vector2 moveDir = target?.position - transform.position?? Vector2.zero;
        Move(moveDir.normalized);
        //print(moveDir.magnitude);//vector.magnitude : �ش� ���Ͱ� "������ͷ�" ���ֵɶ�, ������ ���� 
        //print(moveDir.normalized);//������ ������ä ���̰� 1�� ������ ����
        Hpbar.fillAmount = hpAmount;
    }


    private void Move(Vector2 dir)//dir ���� Ŀ���� 1�� ������ �ϰ� �������.
    {
        Vector2 movePos = rb.position+(dir * moveSpeed*Time.fixedDeltaTime);
        //normalized ������ ������ä ���̰� 1�� ������ ����
        rb.MovePosition(movePos);
    }

    //Onhit
    public void TakeDamage(float damage)
    {
        hp -= damage;

        if (hp <= 0)//���
        {
            Die();

        }
    }
    int exp = 50;
    private void Die()
    {
        GameManager.Instance.enemies.Remove(this);
        GameManager.Instance.player.killcount++;
        GameManager.Instance.player.GaniExp(exp);
        Destroy(gameObject);
    }
    public float damageInterval;
    public float preDamageTime;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (preDamageTime + damageInterval > Time.time)
        {
            return;
        }

        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<Player>().TakeDamage(damage);
            var particle = Instantiate(impactParticle, collision.GetContact(0).point, Quaternion.identity);
            particle.Play();
            Destroy(particle.gameObject, 2f);
            preDamageTime = Time.time;
        }
    }

}
