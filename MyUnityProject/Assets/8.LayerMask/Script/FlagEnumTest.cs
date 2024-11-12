using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enum : int와 밀접한 관계가 있음.
public enum State
{
    Idle = -1,
    Move = -2,
    Attack = 8,
    Die
}

[Flags]
//Enum아펭 Flags Attribute를 추가할 경우
//해당 Enum은 중복 선택이 가능한 Bit Select 형태로 사용 가능
//주의 : Flags Attribute가 부착된 Enum의 각 항목의 값은
// 1에 한번만 비트 연산한 값이 아닐 경우 정상 자동하지 않음.
public enum Debuff
{
    None = 0,
    Poison = 1 << 0,    // 1
    Stun = 1 << 1,      // 2
    Weak = 1 << 2,      // 4
    burn = 1 << 3,      // 8
    Curse = 1 << 4,     // 16
    Every = -1
}
public class FlagEnumTest : MonoBehaviour
{
   public State state;

   //public List<Debuff> debuffList;
   public Debuff debuff;
    private void Start()
    {
        //print($"{state} : {(int)state}");
        print($"{debuff} value : {(int )debuff}");
        print($"{debuff.HasFlag(Debuff.Poison)}");//포이즌을 가지고 있느냐?

        Debuff playerDebuff = (int)Debuff.Poison + Debuff.Curse;

        Debuff cure = Debuff.Poison;

        int playerDebuffInt = (int)playerDebuff;

        // | 비트 or연산자
        //000001
        // or(|)
        //001000
        // =
        //001001
        //10001         |     00001
        int cureInt = (int)cure; //= playerDebuffInt | (int)cure;

        print($"{playerDebuffInt==cureInt}");

        Debuff curedPlayerDebuff = (Debuff)(playerDebuffInt - cureInt);

        print(curedPlayerDebuff);
    }



}
