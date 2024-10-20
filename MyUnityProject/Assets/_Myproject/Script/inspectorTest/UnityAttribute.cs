using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityAttribute : MonoBehaviour
{
    //유니티에서 제공하는 어트리뷰트(Attribute)

    //1.Serializedfield : 일반적으로 인스펙터에서 가려져야 하는 Private 또는 Protected변수를
    //Inpector에 접근 가능하도록 하는 기능

    [SerializeField]
    private int privateValue;

    //2. TextArea : Inpector문자열 입력란을 1줄이 아니라 여러줄로 입력이 가능하도록 표시
    [TextArea(2,5)]
    public string text;

    //3. Header : Inspector에서 중간에 특정 라벨을 끼워넣음
    [Header("헤더 테스트")]
    public int otherInVar;

    //4.Space :  Inspector에서 입력칸 사이에 간격을 띄움
    [Space(100)]
    public float floatVar;

    //5. Range : int또는 float 변수이 최대/최솟값을 제한하며, 슬라이더바로 값을 바꿀 수 있도록 해줌.
    //float 과 int만 사용가능
    [Range(0, 1)]
    public float rangedfloat;

    [Range(-5,5)]
    public int rangedint;

    //6. Tooltip : Inspector에서 해당 변수에 마우스를 올릴 경우 설명을 띄워줌
    [Tooltip("고레와 툴팁데스.")]
    public string otherText;

    //7. HideInInspector : 외부 객체에서 접근은 가능하나 Inspector에서 값을 가려야 할 때
    //주의 : Debug모드에서도 값을 볼 수 없음.
    [HideInInspector]
    public int publicintVar;

    //internal 접근지정자 : 같은 어셈블리(네임스페이스) 내에서만 접근 가능한 접근지정자
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
