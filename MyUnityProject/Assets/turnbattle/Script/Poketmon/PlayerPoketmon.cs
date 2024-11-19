/// <summary>
/// 플레이어가 조종하는 디지몬 클래스
/// Character 클래스를 상속받아 기본 기능을 확장
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoketmon : PoketmonCharacter
{
    public List<Skill>Skills { get; protected set; }

    /// <summary>
    /// 플레이어 포켓몬 생성자
    /// </summary>
    /// <param name="name">디지몬 이름</param>
    /// <param name="hp">초기 체력</param>
    /// <param name="atk">공격력</param>
    /// <param name="def">방어력</param>
    /// <param name="aspd">속도</param>
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
