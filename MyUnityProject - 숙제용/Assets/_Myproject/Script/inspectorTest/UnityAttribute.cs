using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityAttribute : MonoBehaviour
{
    //����Ƽ���� �����ϴ� ��Ʈ����Ʈ(Attribute)

    //1.Serializedfield : �Ϲ������� �ν����Ϳ��� �������� �ϴ� Private �Ǵ� Protected������
    //Inpector�� ���� �����ϵ��� �ϴ� ���

    [SerializeField]
    private int privateValue;

    //2. TextArea : Inpector���ڿ� �Է¶��� 1���� �ƴ϶� �����ٷ� �Է��� �����ϵ��� ǥ��
    [TextArea(2,5)]
    public string text;

    //3. Header : Inspector���� �߰��� Ư�� ���� ��������
    [Header("��� �׽�Ʈ")]
    public int otherInVar;

    //4.Space :  Inspector���� �Է�ĭ ���̿� ������ ���
    [Space(100)]
    public float floatVar;

    //5. Range : int�Ǵ� float ������ �ִ�/�ּڰ��� �����ϸ�, �����̴��ٷ� ���� �ٲ� �� �ֵ��� ����.
    //float �� int�� ��밡��
    [Range(0, 1)]
    public float rangedfloat;

    [Range(-5,5)]
    public int rangedint;

    //6. Tooltip : Inspector���� �ش� ������ ���콺�� �ø� ��� ������ �����
    [Tooltip("���� ��������.")]
    public string otherText;

    //7. HideInInspector : �ܺ� ��ü���� ������ �����ϳ� Inspector���� ���� ������ �� ��
    //���� : Debug��忡���� ���� �� �� ����.
    [HideInInspector]
    public int publicintVar;

    //internal ���������� : ���� �����(���ӽ����̽�) �������� ���� ������ ����������
    internal int internalIntVar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
