/// <summary>
/// 전투 관련 데미지 계산을 처리하는 클래스
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
/// <summary>
/// 공격자의 데미지를 계산하는 메서드
public class BattleProcessor
{
    public static int CalculateDamage(PoketmonCharacter attacker, PoketmonCharacter defender, Skill skill = null)
    {
        int baseDamage = attacker.Attack;
        if (skill != null)
        {
            baseDamage += skill.Power;
        }
        return Mathf.Max(1, baseDamage - defender.Defense);
    }
}
