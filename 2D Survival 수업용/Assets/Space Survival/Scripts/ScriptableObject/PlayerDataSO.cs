using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//플레이어 데이터를 취급하는 ScriptableObject
//2. 파일 생성 메뉴를 명명
[CreateAssetMenu(fileName = "Player Data", menuName = "Scriptable Object/Player Data", order = 0)]
public class PlayerDataSO : ScriptableObject //1 .MonoBehaviour대신 ScriptableObject를 상속 
{

    // asset 파일로 생성후 데이터를 입력할 수 있음
    public string charactername;
    public float hp;
    public float damage;
    public float movespeed;
    public Sprite sprite;
    public GameObject startSkillPrefab;
    public bool rotateRenderer;

}
