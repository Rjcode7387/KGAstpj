using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultParameter : MonoBehaviour
{
    public Player newplayer;
    //기본 매개변수 : 매개변수에 전달할 값을 할당을 안해도 기본저긍로 특정 값이 전달 되도록 할 수 있음.
    //런타임이 아닌 컴파일타임에서 알 수 있는 값이여야 함.(리터럴)
    //[반환형] 함수이름(타입 매개변수형 = 기본값){}
    private void Start()
    {
        GameObject a =CreateNewObject();
        //a.name ="New Obj";

        GameObject b = CreateNewObject("New Obj2");

        newplayer = CreatePlayer("류지형",0,1,2,3,4);
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
    //팩토리 패턴
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

    //params 키워드 : 파라미터에 배열을 받고싶은 경우 맨 마지막 배열 파라미터에 params 키워드를 붙여두면
    //배열 생성 대신 쉼표(,)로 구분하여 배열을 자동생성할 수 있는 파라미터. 

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
