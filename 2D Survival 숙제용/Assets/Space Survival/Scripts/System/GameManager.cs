using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//���� ��ü ������ �Ѱ��ϴ�  ������Ʈ
public class GameManager : MonoBehaviour
{
    // ���� ��ü�� �ϳ��� �����ؾ� �Ѵ�.
    private static GameManager instance;
    public static GameManager Instance => instance;

    internal List<Enemy> enemies = new List<Enemy>();//���� �����ϴ� �� List
    internal Player player; // ���� �����ϴ� Player
    

    //����Ƽ���� �̱��� ������ �����ϴ� ���
    private void Awake()
    {
        //instance = this;//���ӸŴ����� �ϳ��� ���Ÿ� �̷��� �ϼ�
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {

        //MyClass myClass = MyClass.GetMyClass();//��ü����
        ////���� myClass�� �ʿ� �������� null�� �����ϴ� �� ������ ������
        ////GC�� ���� ��ü�� �����
    }
    //��Ÿ �������� ȣ���Ͽ� ��� ���� ���� (Enemy.Die)
    public void RemoveAllEnemies()
    {
        List<Enemy> removeTargets = new List<Enemy>(enemies);//enemies ����Ʈ�� ����

        foreach (Enemy enemy in removeTargets)
        {
            enemy.Die();
        }
    }

    public void GameOver()
    {
        GameOverSceneCtrl.killCount = player.killCount;
        enemies.Clear();
        //GameManager�� DontDestroyOnLoad �����̱� ������
        //enemies ����Ʈ�� �� ������ ���� ��.
        DataManager.Instance.OnSave();             
        SceneManager.LoadScene("GameOverScene");
    }

    public void ReStart()
    {
        SceneManager.LoadScene("GameScene");
        UIManager.Instance.OnRestart();
    } 

    public class DefalultSinglton
    {
        //���� ���μ��� ���� ������ ���� å���� �� �ν��Ͻ��� ������ ����
        private static DefalultSinglton instance;
        //�ܺο��� �����ڸ� ȣ���� �� ������ �⺻ ������ ������ ���´�.
        private DefalultSinglton() { }

        //�ܺο��� ���� ������ �ν��Ͻ��� �����Ͽ� ���� ������ ���� ����(�ٸ� ������ ������ �Ұ�)
        public static DefalultSinglton Instance
        {
            get
            {
                if (instance == null) instance = new DefalultSinglton();
                return instance;
            }
        }
        //�⺻���� ��ü������ ���� �̱��� ��ü�� ����� ���
        public class MyClass
        {
            //null�϶� �����ϸ� ���������� public ��� private�� ����
            private static MyClass nonCollectableMyClass;//������ ������ �ȵǴ� myclass �ν��Ͻ��� ����

            private MyClass() { }

            public int processCount;//��������(non-static), ����å���� �����

            public static MyClass GetMyClass()
            {
                if (nonCollectableMyClass == null)
                {
                    //GetMyClass()�� ���� ȣ�� ���� ��쿡�� true
                    nonCollectableMyClass = new MyClass();
                    return nonCollectableMyClass;
                }
                else
                {
                    return nonCollectableMyClass;
                }
            }

        }
    }
}
