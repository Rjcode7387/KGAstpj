using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class UserData
{
    public string email;
    public string userName;
    public string characterClass;
    public int level;

    

    public UserData(DataRow row) : this(
        row["email"].ToString(),
        row["username"].ToString(),
        row["class"].ToString(),
        int.Parse(row["level"].ToString())
        ) { }//�ٸ������ڸ� ���� �Ͽ� ������ �����ε�

    public UserData(string email,string userName, string characterClass,int level)
    {
        this.email = email;
        this.userName = userName;
        this.characterClass = characterClass;
        this.level = level;
    }
}
