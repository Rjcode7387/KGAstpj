/// <summary>
/// �÷��̾ �����ϴ� ������ Ŭ����
/// Character Ŭ������ ��ӹ޾� �⺻ ����� Ȯ��
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoketmon : PoketmonCharacter
{
    public List<Skill>Skills { get; protected set; }

    /// <summary>
    /// �÷��̾� ���ϸ� ������
    /// </summary>
    /// <param name="name">������ �̸�</param>
    /// <param name="hp">�ʱ� ü��</param>
    /// <param name="atk">���ݷ�</param>
    /// <param name="def">����</param>
    /// <param name="aspd">�ӵ�</param>
    public PlayerPoketmon(string name, int hp, int atk, int def, int aspd)
    {
        Name = name;
        MaxHp = hp;
        CurrentHp = hp;
        Attack = atk;
        Defense = def;
        AttackSpeed = aspd;

        Skills = new List<Skill>();
    }
}
