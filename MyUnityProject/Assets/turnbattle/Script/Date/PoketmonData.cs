using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class PoketmonData
{
    /// <summary>
    /// ���ϸ� �̸�
    /// </summary>
    public string Name;

    /// <summary>
    /// ���ϸ� �⺻ ü��
    /// ���� ���۽� �ִ� HP�� ������
    /// </summary>
    public int BaseHP;

    /// <summary>
    /// ���ϸ� �⺻ ���ݷ�
    /// ���� ���� ������ ��꿡 ���
    /// </summary>
    public int BaseAttack;

    /// <summary>
    /// ���ϸ� �⺻ ����
    /// �޴� ������ ���ҿ� ���
    /// </summary>
    public int BaseDefense;

    /// <summary>
    /// ���ϸ� �⺻ �ӵ�
    /// �� ���� ������ ���
    /// </summary>
    public int BaseSpeed;

    /// <summary>
    /// �� ���ϸ� ��� �� �ִ� ��ų ���
    /// �������̳� Ư�� ���ǿ��� ���� ����
    /// </summary>
    //public List<SkillData> LearnableSkills;
}
