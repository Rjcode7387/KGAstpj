using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoketmon : PoketmonCharacter
{
    public EnemyPoketmon(string enemyname, int enemyhp, int enemyatk, int enemydef, int enemyaspd)
    {
        Name = enemyname;
        MaxHp = enemyhp;
        CurrentHp = enemyhp;
        Attack = enemyatk;
        Defense = enemydef;
        AttackSpeed = enemyaspd;
    }

}
