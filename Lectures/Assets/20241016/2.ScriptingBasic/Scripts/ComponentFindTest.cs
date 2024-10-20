using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentFindTest : MonoBehaviour
{
    #region Ÿ�� ���� ������Ʈ�� �˰� ������ , �ش� ������Ʈ�� ���� Ư�� ������Ʈ�� �����ϰų� ������Ʈ�� �߰�/���� �ؾ��� ���

    public GameObject target;

    private FindMe findMe;

    void Start()
    {
        /*1. target ������Ʈ���� ������Ʈ�� ã���� �Ҷ�
          parameter�� ������Ʈ Ÿ���� ã���� �Ҷ� (�ڽ� ��ڽ� �߻� type of �� As��)
          target.GetComponent(typeof(FindMe) as Component);
        */
        findMe = target.GetComponent<FindMe>();

        print(findMe.message);

        /*2. �ִ��� ������ ���θ� Ȯ���ؾ� �Ҷ�
          out Ű���� : parameter�� �⺻ �Լ��� ����Ÿ�Կ��ٰ� out �Ķ���Ϳ� �ִ� Ÿ�Ե� �����Ҽ��ִ�.
        */
        bool isFound = target.TryGetComponent<BoxCollider>(out BoxCollider boxCollider);

        if (isFound) 
        {
            print($"Found Box Collider. {boxCollider}");
        }
        else 
        {
            print($"There's no BoxCollider. {boxCollider}");
        }

        /*3. Target ������Ʈ�� �ڽ� ������Ʈ���� ������Ʈ�� ã���� �Ҷ�
         *   GetComponentInChildren�� �ڽĿ��� ã������ ���� �θ𿡼��� ã�´�.
         *   �θ𿡼��� ã�⿡ �ڽ� ������Ʈ�� �迭�� �޾� �θ� ������Ʈ�� ������Ʈ�� ����ϰ�
         *   �ڽ�Ŭ������ ������Ʈ�� �޼����� ����ϰԲ� �Ҽ��� �ִ�
         */
        FindMe[] children = target.GetComponentsInChildren<FindMe>();

        foreach (FindMe child in children)
        {
            print(child.message);
        }

        /*4. target ������Ʈ�� ������Ʈ�� �߰��ؾ��� ���*/
        FindMe newFindme = target.AddComponent<FindMe>();
        newFindme.message = "Component Added By Script";

        /*5. target ������Ʈ�� ������Ʈ�� �����ؾ��� ���
             �����ε�� �Լ��� �����̸� �ټ� �ִ�.
             component.gameObject�� ������Ʈ�� �����Ͽ� �ش� ������Ʈ�� ��ӹް� �ִ� ������Ʈ ��ü�� ������ �����ϴ�.
         */
        Destroy(findMe.gameObject, 2f);

    }
    #endregion

}
