/// <summary>
/// 모든 캐릭터의 기본이 되는 추상 클래스
/// 포켓몬과 적의 공통 속성과 메서드를 정의
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//적 포켓몬과 아군 포켓몬의 추상클래스
public abstract class PoketmonCharacter
{
    public string Name { get; protected set; } //포켓몬 이름
    public int MaxHp { get; protected set; } //최대체력
    public int CurrentHp { get; protected set; }// 현재체력
    public int Attack { get; protected set; }//공격력
    public int Defense { get; protected set; } //방어력
    public int AttackSpeed { get; protected set; } //속공속도
    public bool IsAlive => CurrentHp > 0; //사망 생존 구분

    public virtual void TakeDamage(int damage)
    {
        CurrentHp = Mathf.Max(0,CurrentHp-damage);
    }


}
