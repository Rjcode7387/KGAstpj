using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grill : MonoBehaviour
{
 
   
    //�׸� Ȱ��ȭ�� �ִ� �����ѵ����� �ӵ��� �ð��� �����ؼ� ����� Max���Ľ� �۵�����
    //Ȱ��ȭ�� �� �׸����� ��ȣ �ۿ�� �׸��� �߲�ġ�� ����� �÷��̾ ��ȣ �ۿ�� �÷��̾ �߲�ġ��  ������
    //���뿡�� ��ȣ�ۿ�Ű�� ������ ��� �ִ� �߲�ġ�� ���뿡 ������
    //�ִ� ������ �߲�ġ�� �������� �ٽ� �۵�
    //

    public int ChickenSkewers = 0;//�߲�ġ
    public float makingTime = 5f;
    public float makingSpeed = 1f;//����ӵ�
    public int maxObjcet = 4;//���� �ѵ�
    
    public bool isPurchased = false;//�������� ���� 
    private float currentMakingTime; // ���� ���� ����ð�

    private void Start()
    {
        ChickenSkewers = 0;//�߲�ġ ���� �ʱ�ȭ      
        isPurchased = false;
        currentMakingTime = 0f;
        
    }

    private void Update()
    {
        //Ȱ��ȭ �����϶� ��� ���� 
        if (isPurchased)
        {
             currentMakingTime += Time.deltaTime * makingSpeed;
            if (currentMakingTime >= makingTime)
            {
                currentMakingTime = 0f;
                if (ChickenSkewers < maxObjcet)
                {
                    ChickenSkewers++;
                    Debug.Log("�� �������̾�");
                }
            }

        }
        
    }

    

    //ȭ�θ� Ȱ��ȭ�ϴ� �޼ҵ� 
    public void PurchaseGrill()
    {
        isPurchased = true;
        Debug.Log("�׸��� ���ŵǾ� ���� Ȱ��ȭ�Ǿ����ϴ�!");
    }
    //�߲�ġ�� �÷��̾ �������� �޼���
    public bool TakeChickenSkewers()
    {

        if (ChickenSkewers > 0)
        {
            GameManager.Instance.player.holdingChickenSkewers += ChickenSkewers;
            ChickenSkewers = 0;//�߲�ġ�� �ʱ�ȭ
            return true;
        }
        return false;
    }


}
