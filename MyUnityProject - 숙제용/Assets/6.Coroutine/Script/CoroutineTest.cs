using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineTest : MonoBehaviour
{
    MeshRenderer mr;

    public Material woodMaterial;
    public Material redMaterial;
    public Material stoneMaterial;

    public Transform transformTest;

    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
        stoneMaterial = mr.material;
    }
    void Start()
    {
        //정획히 3초 후에 MeshRenderer의 Material을 woodMaterial를 교체 하고싶어요.
        var enumerator = StringEnumerator();

        while (enumerator.MoveNext())
        {
            print(enumerator.Current);
        }


        //enumerator.MoveNext();
        //print(enumerator.Current);
        //enumerator.MoveNext();
        //print(enumerator.Current);

        //List<int> intlist = new List<int>() {1,2,3};

        //foreach (int some in intlist)
        //{
        //    print(someInt);
        //}
        //foreach (Transform someTransform in transformTest)
        //{
        //    print(someTransform.name);
        //}
        //하수용
        // StartCoroutine("MaterialChange");
        //고수용
        //코루틴 자체를 리턴함
        materialChangeCoroutine = StartCoroutine(MaterialChange(redMaterial,1f));
    }
    private Coroutine materialChangeCoroutine;



    void Update()
    {
        //if (Time.time > 3f)
        //{
        //    mr.material = woodMaterial;
        //}
        if (Input.GetButtonDown("Jump"))
        {
            mr.material = stoneMaterial;
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            print("코루틴 스탑");
            StopCoroutine(materialChangeCoroutine);
        }
    }
    private IEnumerator<string> StringEnumerator()
    {
        //IEnumerator를 반환하는 함수는
        //yield return 키워드를 통해
        //값을 순차적으로 반환 할 수 있음.
        yield return "A";
        yield return "B";
        yield return "C";//yield양보하다
    }

    private IEnumerator MaterialChange(Material mat,float interval )
    {
        while (true)
        {
            //코루틴이 3초동안 대기 합니다.
            yield return new WaitForSeconds(interval);
            mr.material = mat;
        }


        //while (true)
        //{
        //    yield return null;
        //    print("MaterialChange 코루틴 수행됨");
        //}
    }

}
