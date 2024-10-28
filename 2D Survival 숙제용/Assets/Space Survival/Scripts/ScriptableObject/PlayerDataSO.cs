using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//�÷��̾� �����͸� ����ϴ� ScriptableObject
//2. ���� ���� �޴��� ���
[CreateAssetMenu(fileName = "Player Data", menuName = "Scriptable Object/Player Data", order = 0)]
public class PlayerDataSO : ScriptableObject //1 .MonoBehaviour��� ScriptableObject�� ��� 
{

    // asset ���Ϸ� ������ �����͸� �Է��� �� ����
    public string charactername;
    public float hp;
    public float damage;
    public float movespeed;
    public Sprite sprite;
    public GameObject startSkillPrefab;
    public bool rotateRenderer;

}
