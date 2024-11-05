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
    [Header("�߲�ġ ���� ����")]
    public GameObject chickenSkewerPrefab;  // �߲�ġ ������
    private Vector2 spawnOffset = new Vector2(0, 0.3f);      // ���� ��ġ ������
    private List<GameObject> activeChickenSkewers = new List<GameObject>();

    public int ChickenSkewers = 0;//�߲�ġ
    public float makingTime = 2f;
    public float makingSpeed = 1f;//����ӵ�
    public int maxObjcet = 4;//���� �ѵ�
    
    public bool isPurchased = false;//�������� ���� 
    private float currentMakingTime; // ���� ���� ����ð�

    private SpriteRenderer spriteRenderer;
    private Color originalcolor;
    public Text maxText;
    public Text tutorialText;

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
        if(tutorialText != null)
        {
            tutorialText.gameObject.SetActive(true);
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
                    SpawnChickenSkewer();
                    ChickenSkewers++;
                    Debug.Log("�� �������̾�");
                }
            }
            MaxStatus();
        }
        
    }
    private void SpawnChickenSkewer()
    {
        Vector2 spawnPos = (Vector2)transform.position+spawnOffset;
        GameObject newSkewer = Instantiate(chickenSkewerPrefab, spawnPos, Quaternion.identity);
        activeChickenSkewers.Add(newSkewer);
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
        if(tutorialText != null)
        {
            tutorialText.gameObject.SetActive(false);
        }
        Debug.Log("�׸��� ���ŵǾ� ���� Ȱ��ȭ�Ǿ����ϴ�!");
    }
    //�߲�ġ�� �÷��̾ �������� �޼���
    public bool TakeChickenSkewers()
    {
        Player player = GameManager.Instance.player;

        // ������ �� �ִ� �߲�ġ ���� ���
        int availableSpace = player.maxHoldingChickenSkewers - player.holdingChickenSkewers.Count;
        int chickensToTake = Mathf.Min(ChickenSkewers, availableSpace);

        if (chickensToTake > 0)
        {
            // ���� ������ŭ �ѹ��� �߲�ġ ��������
            for (int i = 0; i < chickensToTake; i++)
            {
               
                // ���������� ������ �߲�ġ�� �÷��̾�� ����
                GameObject skewer = activeChickenSkewers[activeChickenSkewers.Count - 1];
                activeChickenSkewers.RemoveAt(activeChickenSkewers.Count - 1);

                skewer.GetComponent<ChickenSkewer>().SetFollowTarget(player.transform);
                player.holdingChickenSkewers.Add(skewer);

                ChickenSkewers--;
            }
            return true;
        }
        return false;

    }


}
