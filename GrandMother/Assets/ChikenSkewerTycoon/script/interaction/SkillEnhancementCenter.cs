using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEnhancementCenter : MonoBehaviour
{
    //플레이어가 상호작용키 입력시 일시정지되며
    //스킬 선택할수 있는 UI창이 뜨고 스킬3개를 모은 달러를 지불하여 강화가능 
    //스킬(플레이어의 무브 칸수증가, 화로당 닭꼬치에 저장개수 증가(스킬레벨*2), 닭꼬치 판매액 증가)
    //스킬강화 UI를 닫을시 일시정지가 풀림
    //스킬 최대 레벨 3
    //강화는 총 2번가능

    //맥스 레벨
    private const int MAX_SKILL_LEVEL = 3;
    private int currentSkillLevel = 1;

    

    public void UpgradeMove(Player player)
    {
        if (!CanUpgrade())
        {
            return;
        }        
        player.moveDistance += 0.1f;
        currentSkillLevel++;
        print("할머니 너무 빨라요...");
    }

    public void UpgradeCapcity(Grill grill)
    {
        if (!CanUpgrade())
        {
            return;
        }
        grill.maxObjcet += 2;
        currentSkillLevel++;
        print("닭이 2개 늘어났닭");
    }

    public void UpgradeSellingprice(Counter counter)
    {
        if (!CanUpgrade())
        {
            return;
        }
        counter.UpgradeSellPrice();
        currentSkillLevel++;
        print("인생은 실전이다 ");     
    }

    public bool CanUpgrade()
    {
        return currentSkillLevel < MAX_SKILL_LEVEL;
    }
}
