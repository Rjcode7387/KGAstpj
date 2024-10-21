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

    private void Start()
    {
        target = GameObject.Find("Player").transform;
        maxHp = hp;
        
    }

    private void Update()
    {
        Vector2 moveDir = target.position - transform.position;
        Move(moveDir.normalized);
        //print(moveDir.magnitude);//vector.magnitude : �ش� ���Ͱ� "������ͷ�" ���ֵɶ�, ������ ���� 
        //print(moveDir.normalized);//������ ������ä ���̰� 1�� ������ ����
        Hpbar.fillAmount = hpAmount;
    }


    private void Move(Vector2 dir)//dir ���� Ŀ���� 1�� ������ �ϰ� �������.
    {
        //normalized ������ ������ä ���̰� 1�� ������ ����
        transform.Translate(dir.normalized * moveSpeed * Time.deltaTime);
    }

    //Onhit
    public void TakeDamage(float damage)
    {
        hp -= damage;

        if (hp <= 0)//���
        {
            Player count = target.GetComponent<Player>();
            count.killcount++;
            Destroy(gameObject);

        }
    }
}
