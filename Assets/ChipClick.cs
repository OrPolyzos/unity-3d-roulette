using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChipClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject PlayerDetails;

    public bool ChipIsBeingClicked = false;
    public bool InsideMultiBet = false;
    public float TimeBeingHold = 0;

    public void Update()
    {
        if (ChipIsBeingClicked)
        {
            TimeBeingHold += Time.deltaTime;
        }
        if (TimeBeingHold >= 0.35f)
        {
            if (!PlayerDetails.GetComponent<GameController>().ExecutingMultibet)
            {
                StartCoroutine(PlayerDetails.GetComponent<GameController>().ChipHoldClick(gameObject.transform.parent.gameObject));
            }
            //StartCoroutine(PlayerDetails.GetComponent<GameController>().ChipHoldClick(gameObject.transform.parent.gameObject));
            //PlayerDetails.GetComponent<GameController>().ChipHoldClick(gameObject.transform.parent.gameObject);
            //StartCoroutine(Delay());
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        ChipIsBeingClicked = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        ChipIsBeingClicked = false;
        if(TimeBeingHold < 1f)
        {
            PlayerDetails.GetComponent<GameController>().ChipClick(gameObject.transform.parent.gameObject);
        }
        TimeBeingHold = 0;
    }
    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
    }
}
