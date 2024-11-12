using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventTriggerTest : MonoBehaviour
{
    public void OnClick()
    {
        print("Click");
    }
    public void OnEnter()
    {
        print("Enter");
    }
    public void OnExit()
    {
        print("Exit");
    }

    public void OnAnyEvent(BaseEventData eventData)
    {
        print($"some event called, type : {(eventData as PointerEventData).position}");
    }

}
