using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill 
{
    public string skillName;
    public int skillLevel;
    public bool isTargetting;         // ���� ����� ���� ���ϴ� ��ų����
    public GameObject[] skillPrefabs;  // 5���� �������� �����Ͽ� �� ������ �´� �������� �ε��ϵ��� Ȱ��
    public GameObject currentSkillObject;
}
