using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] // json으로 변경할 것이기 때문에 직렬화
public class UserData
{
    public enum UserClass
    {
        Warrior,
        Wizard,
        Rogue,
        Archar
    }

    public string userId { get; set; }
    public string userName;
    public int level;
    public int gold;
    public int exp { get; set; }
    public int jewel;
    public int maxExp => level * 100;
    public UserClass userClass;

    public UserData() { }//기본 생성자

    public UserData(string userId)//회원가입할때 쓸 생성자
    { 
        this.userId = userId;
        userName = "칼을 든 궁수";
        level = 1;
        gold = 0;
        exp = 0;
        jewel = 0;
        userClass = UserClass.Archar;
    }

    public UserData(string userId, string userName, int level, int gold,int exp,int jewel, UserClass userClass) : this(userId)
    {
        //로그인할때 쓸 생성자
        this.userId = userId;
        this.userName = userName;
        this.level = level;
        this.gold = gold;
        this.jewel = jewel;
        this.userClass = userClass;
    }
}
