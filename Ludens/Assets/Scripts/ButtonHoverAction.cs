using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverAction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool OnPointer = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("마우스가 버튼 위에 올라옴!");
        OnPointer = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("마우스가 버튼에서 벗어남!");
        OnPointer = false;
    }
}
