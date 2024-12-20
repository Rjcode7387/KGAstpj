using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using PED = UnityEngine.EventSystems.PointerEventData;

public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public string coodinate;

    public Board board;


    public void OnPointerClick(PED eventData)
    {
        print(coodinate);
        board.SelectCell(this);
        //FirebaseManager.instance.SendTurn(coodinate);

    }

    public void OnPointerEnter(PED eventData)
    {
        
    }

    public void OnPointerExit(PED eventData)
    {
       
    }
}
