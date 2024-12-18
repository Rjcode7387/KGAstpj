using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grill : MonoBehaviour
{


    //그릴 활성화시 최대 저장한도까지 속도와 시간을 대입해서 만들고 Max도돨시 작동중지
    //활성화가 된 그릴에서 상호 작용시 그릴에 닭꼬치를 저장시 플레이어가 상호 작용시 플레이어가 닭꼬치를  가져감
    //계산대에서 상호작용키를 누르면 들고 있던 닭꼬치를 계산대에 저장함
    //최대 저장한 닭꼬치를 가져가면 다시 작동
    //
    private ChickenSkewerSpawner spawner;
    [SerializeField] private GameObject chickenSkewerPrefab;
    public int ChickenSkewers = 0;//닭꼬치
    public float makingTime = 2f;
    public float makingSpeed = 1f;//생산속도
    public int maxObjcet = 4;//저장 한도
    
    public bool isPurchased = false;//생산중지 시작 
    private float currentMakingTime; // 현재 제작 진행시간

    private SpriteRenderer spriteRenderer;
    private Color originalcolor;
    public Text maxText;
    public Text tutorialText;

    private void Start()
    {
        spawner = GetComponent<ChickenSkewerSpawner>();
        if (spawner == null)
        {
            spawner = gameObject.AddComponent<ChickenSkewerSpawner>();
        }
        spawner.Initialize(chickenSkewerPrefab);
        InitializeGrill();
    }

    private void InitializeGrill()
    {
        ChickenSkewers = 0;
        isPurchased = false;
        currentMakingTime = 0f;

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalcolor = spriteRenderer.color;
        Transparency(0.5f);

        SetupUI();
    }

    private void SetupUI()
    {
        if (maxText != null)
        {
            maxText.gameObject.SetActive(false);
        }
        if (tutorialText != null)
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
        //활성화 상태일때 계속 생산 
        if (isPurchased)
        {
            UpdateProduction();
            MaxStatus();
        }

    }
    private void UpdateProduction()
    {
        currentMakingTime += Time.deltaTime * makingSpeed;
        if (currentMakingTime >= makingTime)
        {
            currentMakingTime = 0f;
            if (ChickenSkewers < maxObjcet)
            {
                spawner.SpawnChickenSkewer(transform.position);
                ChickenSkewers++;
            }
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
    

    //화로를 활성화하는 메소드 
    public void PurchaseGrill()
    {
        isPurchased = true;
        Transparency(1f);
        if(tutorialText != null)
        {
            tutorialText.gameObject.SetActive(false);
        }
        Debug.Log("그릴이 구매되어 영구 활성화되었습니다!");
    }
    //닭꼬치를 플레이어가 가져가는 메서드
    public bool TakeChickenSkewers()
    {
        Player player = GameManager.Instance.player;
        int availableSpace = player.maxHoldingChickenSkewers - player.holdingChickenSkewers.Count;
        int chickensToTake = Mathf.Min(ChickenSkewers, availableSpace);

        if (chickensToTake > 0)
        {
            for (int i = 0; i < chickensToTake; i++)
            {
                GameObject skewer = spawner.TakeLastSkewer();
                if (skewer != null)
                {
                    skewer.GetComponent<ChickenSkewer>().SetFollowTarget(player.transform);
                    player.holdingChickenSkewers.Add(skewer);
                    ChickenSkewers--;
                }
            }
            return true;
        }
        return false;

    }


}
