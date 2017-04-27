using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{ 
    public GameObject PlayerDetails;
    public void OnPointerDown(PointerEventData eventData)
    {

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            return;
        }
        Debug.Log("Down");
        PlayerDetails.GetComponent<GameController>().PlayerHoldClick(gameObject);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PlayerDetails.GetComponent<GameController>().PlayerClick();
    }
}
