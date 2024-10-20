using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTest : MonoBehaviour
{
    #region ��ü�� �����ϴ� 4���� ���
    public GameObject destroyTarget;
    public Component destroyComponentTarget;
    public GameObject destroyTargetWithDelay;
    public GameObject destroyTargetImmediately;
    private void Start()
    {
        /*1. GameObject�� Destroy*/
        Destroy(destroyTarget);

        /*2. Componenet�� ����� Object�� �ı�
             Destroy �Լ��� ȣ�� ���Ŀ��� �Ķ���ͷ� ������ ������Ʈ�� ������ �����ϴ�.(��ü�� ���� �ı����� �ʴ´�.)
             Destroy �Լ��� �Ķ���ͷ� ���޵� ������Ʈ�� ��� �ı��Ǵ� ���� �ƴ�, 
             �ı� �� ����Ʈ�� ����Ʈ�� �� �� ���� �������� ���۵Ǳ� �� �ı��ȴ�. ���� �ش� �����ӿ��� ���� ��ü�� �����ϴ°�       
         */
        Destroy(destroyComponentTarget);
        FindMe findme = destroyComponentTarget as FindMe;
        print(findme.message);

        //3. 2)�� ���� ������ , Destroy�Լ��� �����̸� �����ϴ� ���� �����ϴ�.
        Destroy(destroyTargetWithDelay, 3f);

        /*4. ����, ���� �������̴��� Ư�� ��ü�� ��� �ı��Ǳ⸦ ���Ѵٸ� DestoryImmediate() �� ����Ѵ�.
             �� �Լ��� ȣ��� ������ �ڵ���ο����� �ش� ��ü�� �����Ҽ� ����.
         */
        DestroyImmediate(destroyTargetImmediately);
        print(destroyTargetImmediately.name);
    }
    #endregion
}
