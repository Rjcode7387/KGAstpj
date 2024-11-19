using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class PoketmonData
{
    /// <summary>
    /// 포켓몬 이름
    /// </summary>
    public string Name;

    /// <summary>
    /// 포켓몬 기본 체력
    /// 전투 시작시 최대 HP로 설정됨
    /// </summary>
    public int BaseHP;

    /// <summary>
    /// 포켓몬 기본 공격력
    /// 물리 공격 데미지 계산에 사용
    /// </summary>
    public int BaseAttack;

    /// <summary>
    /// 포켓몬 기본 방어력
    /// 받는 데미지 감소에 사용
    /// </summary>
    public int BaseDefense;

    /// <summary>
    /// 포켓몬 기본 속도
    /// 턴 순서 결정에 사용
    /// </summary>
    public int BaseSpeed;

    /// <summary>
    /// 이 포켓몬 배울 수 있는 스킬 목록
    /// 레벨업이나 특정 조건에서 습득 가능
    /// </summary>
    //public List<SkillData> LearnableSkills;
}
