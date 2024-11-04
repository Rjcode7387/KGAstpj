using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private SpriteRenderer spriteRenderer;
    private Color originalcolor;
    public Text maxText;

    private void Start()
    {
        ChickenSkewers = 0;//�߲�ġ ���� �ʱ�ȭ      
        isPurchased = false;
        currentMakingTime = 0f;

        spriteRenderer = GetComponent<SpriteRenderer>();

        originalcolor = spriteRenderer.color;


        Transparency(0.5f);

        if (maxText != null)
        {
            maxText.gameObject.SetActive(false);
        }
    }

    private void Transparency(float alpha)
    {
        if (spriteRenderer != null)
        {
            Color newcolor = originalcolor;
            newcolor.a = alpha;
            spriteRenderer.color = newcolor;
        }
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
            MaxStatus();
        }
        
    }

    private void MaxStatus()
    {
        if (maxText != null)
        {
            if (ChickenSkewers >= maxObjcet)
            {
                maxText.gameObject.SetActive(true);
                maxText.text = "MAX";
            }
            else
            {
                maxText.gameObject.SetActive(false);
            }
        }
    }
    

    //ȭ�θ� Ȱ��ȭ�ϴ� �޼ҵ� 
    public void PurchaseGrill()
    {
        isPurchased = true;
        Transparency(1f);
        Debug.Log("�׸��� ���ŵǾ� ���� Ȱ��ȭ�Ǿ����ϴ�!");
    }
    //�߲�ġ�� �÷��̾ �������� �޼���
    public bool TakeChickenSkewers()
    {
        Player player = GameManager.Instance.player;

        if (ChickenSkewers > 0 && player.holdingChickenSkewers < player.maxHoldingChickenSkewers)
        {
            int available = player.maxHoldingChickenSkewers -player.holdingChickenSkewers;
            int takeTo = Mathf.Min(ChickenSkewers, available);

            player.holdingChickenSkewers += takeTo;
            ChickenSkewers -= available;
            return true;
        }
        return false;
        
    }


}
