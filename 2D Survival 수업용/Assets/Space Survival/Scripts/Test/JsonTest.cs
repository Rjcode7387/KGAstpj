using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonTest : MonoBehaviour
{
    public EnemyDataSO testData;

    public EnemyData loadedData;

    void Start()
    {
       
    }

    public void Save()
    {
        //��ü�� Json�����ͷ� ��ȯ
        string json = JsonUtility.ToJson(testData);
        json = JsonConvert.SerializeObject(testData);
        //���� ���� ������ȭ �� ���ڿ��� ���� Ȯ���� �� ����.
        //��ü�� �Էµ� ���� ��� string���� ��ȯ �ǹǷ� ,�а� ���� ������ ȿ�������� �ʴ�.
        print(json);
        //StreamingAssets ���� : ����� ���� ������ �״�� ����Ǿ� �������Ͽ� ���ԵǾ�� �� ���ϵ��� �־���� ����
        //������ �״�� �����ǰ� �״�� �ε�ǹǷ� ���� �Ŀ��� ���� ������ �� ������
        //�÷��̾ ���� ���� ������ �� �ִ°��� �������� ����.
        string path = $"{Application.streamingAssetsPath}/{testData.name}.json";

        File.WriteAllText(path, json);
    }
    public void Load()
    {
       string path = $"{Application.streamingAssetsPath}/{testData.name}.json";
       string json = File.ReadAllText(path);
        //Json�����͸� ��ü�� ��ȯ
        print(json);
        loadedData = JsonUtility.FromJson<EnemyData>(json);
        loadedData = JsonConvert.DeserializeObject<EnemyData>(json);
        //JsonUtility = C#���� ����ϴ� ���ͷ� ������Ÿ���� ��κ� ����ȭ�� �����ϳ�.
        //�迭,����Ʈ ���� �ݷ���(Hashtable,Dictionary ��)�� ����ȭ�� �Ұ���
        print(loadedData.enemyName);
        print(loadedData.level);

    }
}

//Json�� ���� ����ȭ/ ������ȭ �� ��ü
[Serializable]
public class EnemyData
{
    public string enemyName;
    public int level;
    public float hp;
    public float damage;
    public float moveSpeed;
}