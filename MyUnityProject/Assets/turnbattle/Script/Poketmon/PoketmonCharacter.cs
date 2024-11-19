/// <summary>
/// ��� ĳ������ �⺻�� �Ǵ� �߻� Ŭ����
/// ���ϸ�� ���� ���� �Ӽ��� �޼��带 ����
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�� ���ϸ�� �Ʊ� ���ϸ��� �߻�Ŭ����
public abstract class PoketmonCharacter
{
    public string Name { get; protected set; } //���ϸ� �̸�
    public int MaxHp { get; protected set; } //�ִ�ü��
    public int CurrentHp { get; protected set; }// ����ü��
    public int Attack { get; protected set; }//���ݷ�
    public int Defense { get; protected set; } //����
    public int AttackSpeed { get; protected set; } //�Ӱ��ӵ�
    public bool IsAlive => CurrentHp > 0; //��� ���� ����

    public virtual void TakeDamage(int damage)
    {
        CurrentHp = Mathf.Max(0,CurrentHp-damage);
    }


}
