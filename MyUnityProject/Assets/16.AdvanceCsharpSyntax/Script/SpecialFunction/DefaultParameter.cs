using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultParameter : MonoBehaviour
{
    public Player newplayer;
    //�⺻ �Ű����� : �Ű������� ������ ���� �Ҵ��� ���ص� �⺻����� Ư�� ���� ���� �ǵ��� �� �� ����.
    //��Ÿ���� �ƴ� ������Ÿ�ӿ��� �� �� �ִ� ���̿��� ��.(���ͷ�)
    //[��ȯ��] �Լ��̸�(Ÿ�� �Ű������� = �⺻��){}
    private void Start()
    {
        GameObject a =CreateNewObject();
        //a.name ="New Obj";

        GameObject b = CreateNewObject("New Obj2");

        newplayer = CreatePlayer("������",0,1,2,3,4);
    }

    private GameObject CreateNewObject()
    {
        return new GameObject();
    }

    private GameObject CreateNewObject(string name = "Some Obj")
    {
        GameObject returnObject = new GameObject();
        returnObject.name = name;
        return returnObject;
    }
    //���丮 ����
    private Player CreatePlayer(string name, int level = 1, float damage = 5f, Vector2 position = default, GameObject obj = null)
    {
        Player returnPlayer = new Player();
        returnPlayer.name = name;
        returnPlayer.level = level;
        returnPlayer.damage = damage;
        returnPlayer.position = position;
        returnPlayer.redererobjcet = obj;

        return returnPlayer;
    }

    //params Ű���� : �Ķ���Ϳ� �迭�� �ް���� ��� �� ������ �迭 �Ķ���Ϳ� params Ű���带 �ٿ��θ�
    //�迭 ���� ��� ��ǥ(,)�� �����Ͽ� �迭�� �ڵ������� �� �ִ� �Ķ����. 

    private Player CreatePlayer(string name,int level =1, params int[] items)
    {
        Player returnObject = CreatePlayer(name,level);
        returnObject.items = items;
        return returnObject;
    }

    [Serializable]
    public class Player
    {
        public string name;
        public int level;
        public float damage;
        public Vector2 position;
        public GameObject redererobjcet;
        public int[] items;
    }
}
