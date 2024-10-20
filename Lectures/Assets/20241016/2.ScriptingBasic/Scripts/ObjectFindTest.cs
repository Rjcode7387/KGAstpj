using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFindTest : MonoBehaviour
{
    /*������ ���� �Ǳ� �� ������ ���� �� �� �ִ� ������Ʈ�� Inspector���� �Ҵ��Ͽ� ������ �� �ִ�.*/
    public GameObject target;
    //������ ������ ���۵Ǳ� �� ���� �� �� ���ų� , Inspector���� ���� �Ҵ��� �� ���� ��ü��
    private GameObject privateTarget;
    private GameObject findedTarget;
    private GameObject newTarget;
    private GameObject namedNewTarget;
    private GameObject componentAttachedTarget;

    


    private void Start()
    {
        #region Target ������Ʈ�� ã�ų� �߰�/���� �ϴ� ���

        /*1. ��ü ������ �̸����� ã��
            �� ����� ���� ������Ʈ�� �������� �� ��ü�� ��ȸ�ϸ� ������Ʈ�� �̸��� ã�⿡ ���ϰ� ũ�� �ɸ���.
            ���� �̸��� ������Ʈ�� ������ ������� � ������Ʈ�� Ž������ Ȯ���� �� ����.
            �̷� ������ Start�Լ������� ���������� ���δ�.
        */

        privateTarget = GameObject.Find("PrivateTarget");

        /*2. ��ü ������ Ư�� ������Ʈ�� ������ �ִ� ��ü�� ã�� ��.
         * ������Ʈ Ÿ������ ã�� ���ӿ�����Ʈ�� �����ϴ� ��
           findedTarget = FindObjectOfType(typeof(FindMe) as Component).gameObject;
         */

        findedTarget = FindObjectOfType<FindMe>().gameObject;

        print(privateTarget.name);
        print(findedTarget.name);

        /* 3. ��ü�� ���� �� ����*/
        newTarget = new GameObject();
        namedNewTarget = new GameObject("New Target");
        componentAttachedTarget = new GameObject("Component Attached Target",typeof(FindMe),typeof(SpriteRenderer));

        /*4. ��ü�� ����
          �����ε�� �Լ��� �����̸� �ټ��� �ִ�.
         */
        Destroy(privateTarget , 2f);
        #endregion



    }
}
