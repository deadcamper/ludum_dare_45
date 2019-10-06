using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Exit : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
       
    }

    public void OnMouseUpAsButton()
    {
        Debug.Log("Exiting game...");
        Application.Quit(0);
    }
}
