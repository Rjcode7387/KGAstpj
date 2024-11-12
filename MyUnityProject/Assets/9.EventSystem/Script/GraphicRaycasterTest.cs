using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GraphicRaycasterTest : MonoBehaviour, IPointerEnterHandler , IBeginDragHandler,
    IEndDragHandler , IDragHandler
{
    //�巡�� ����
    public void OnBeginDrag(PointerEventData eventData)
    {
        EventSystemTestManager.instance.textMeshPro.text = name;
    }

    //�巡�� ��
    public void OnDrag(PointerEventData eventData)
    {
        GetComponent<RectTransform>().anchoredPosition = eventData.delta;
        //���������Ӱ� ���� �������� ������ ��ġ ����(�̵���)
    }

    //�巡�� ��
    public void OnEndDrag(PointerEventData eventData)
    {
        EventSystemTestManager.instance.textMeshPro.text = "Nothing";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        print("Mouse over");
    }
}
