using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//게임 전체 진행을 총괄하는  오브젝트
public class GameManager : MonoBehaviour
{
    // 게임 전체에 하나만 존재해야 한다.
    private static GameManager instance;
    public static GameManager Instance => instance;

    internal List<Enemy> enemies = new List<Enemy>();//씬에 존재하는 적 List
    internal Player player; // 씬에 존재하는 Player
    

    //유니티에서 싱글톤 패턴을 적용하는 방법
    private void Awake()
    {
        //instance = this;//게임매니져를 하나만 쓸거면 이렇게 하셈
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

        //MyClass myClass = MyClass.GetMyClass();//객체생성
        ////만약 myClass가 필요 없어져서 null을 대입하는 등 참조를 잃으면
        ////GC에 의해 객체가 사라짐
    }
    //폭타 아이템이 호출하여 모든 적을 제거 (Enemy.Die)
    public void RemoveAllEnemies()
    {
        List<Enemy> removeTargets = new List<Enemy>(enemies);//enemies 리스트를 복사

        foreach (Enemy enemy in removeTargets)
        {
            enemy.Die();
        }
    }

    public void GameOver()
    {
        GameOverSceneCtrl.killCount = player.killCount;
        enemies.Clear();
        //GameManager는 DontDestroyOnLoad 상태이기 때문에
        //enemies 리스트에 빈 변수만 갖게 됨.
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
        //현재 프로세스 내에 생성될 단일 책임을 진 인스턴스를 저장할 변수
        private static DefalultSinglton instance;
        //외부에서 생성자를 호출할 수 없도록 기본 생성자 접근을 막는다.
        private DefalultSinglton() { }

        //외부에서 단일 생성된 인스턴스에 접근하여 값을 가져올 수만 있음(다른 값으로 대입은 불가)
        public static DefalultSinglton Instance
        {
            get
            {
                if (instance == null) instance = new DefalultSinglton();
                return instance;
            }
        }
        //기본적인 객체지향형 언어에서 싱글톤 객체를 만드는 방법
        public class MyClass
        {
            //null일때 접근하면 위험함으로 public 대신 private로 쓴다
            private static MyClass nonCollectableMyClass;//참조를 잃으면 안되는 myclass 인스턴스를 저장

            private MyClass() { }

            public int processCount;//전역변수(non-static), 단일책임이 적용됨

            public static MyClass GetMyClass()
            {
                if (nonCollectableMyClass == null)
                {
                    //GetMyClass()가 최초 호출 됐을 경우에만 true
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
