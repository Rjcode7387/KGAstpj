using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEnhancementCenter : MonoBehaviour
{
    //�÷��̾ ��ȣ�ۿ�Ű �Է½� �Ͻ������Ǹ�
    //��ų �����Ҽ� �ִ� UIâ�� �߰� ��ų3���� ���� �޷��� �����Ͽ� ��ȭ���� 
    //��ų(�÷��̾��� ���� ĭ������, ȭ�δ� �߲�ġ�� ���尳�� ����(��ų����*2), �߲�ġ �Ǹž� ����)
    //��ų��ȭ UI�� ������ �Ͻ������� Ǯ��
    //��ų �ִ� ���� 3
    //��ȭ�� �� 2������

    //�ƽ� ����
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
        print("�ҸӴ� �ʹ� �����...");
    }

    public void UpgradeCapcity(Grill grill)
    {
        if (!CanUpgrade())
        {
            return;
        }
        grill.maxObjcet += 2;
        currentSkillLevel++;
        print("���� 2�� �þ��");
    }

    public void UpgradeSellingprice(Counter counter)
    {
        if (!CanUpgrade())
        {
            return;
        }
        counter.UpgradeSellPrice();
        currentSkillLevel++;
        print("�λ��� �����̴� ");     
    }

    public bool CanUpgrade()
    {
        return currentSkillLevel < MAX_SKILL_LEVEL;
    }
}
