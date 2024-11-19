/// <summary>
/// ���� ���� ������ ����� ó���ϴ� Ŭ����
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
/// <summary>
/// �������� �������� ����ϴ� �޼���
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
