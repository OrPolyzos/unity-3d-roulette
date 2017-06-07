using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMovement : MonoBehaviour {

    // Use this for initialization
    public GameObject EventSystem;
    public GameObject Sphere;
    public string WinningNumber;
    public bool IWasMovedUp = false;
    public bool IWasMovedDown = false;
	void Start () {      
        
    }
	
	// Update is called once per frame
	void Update () {
        if (EventSystem.GetComponent<State>().GameState.Equals("GameStarted"))
        {
            if (!IWasMovedUp)
            {
                iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("PanelMov"), "time", 17.5));
                this.transform.GetChild(0).GetComponent<Text>().text = "The game has started!";
                IWasMovedUp = true;
            }
        }
        else if (EventSystem.GetComponent<State>().GameState.Equals("GameEnded"))
        {
            if (!IWasMovedDown)
            {
                WinningNumber = Sphere.GetComponent<GetTheNumber>().WinningNumber;
                this.transform.GetChild(0).GetComponent<Text>().text = "The Winning Number is: " + WinningNumber + "!";
                iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("PanelReturn"), "time",1));
                IWasMovedDown = true;
            }
        }
		else if (EventSystem.GetComponent<State>().GameState.Equals("AwardInformation"))
		{
            /*
            if (EventSystem.GetComponent<State>().WinningAmount != string.Empty)
            {
                this.transform.GetChild(0).GetComponent<Text>().text =
                EventSystem.GetComponent<State>().WinningPlayerName + " Won: " + EventSystem.GetComponent<State>().WinningAmount + " pesos";
            }
            else
            {
                this.transform.GetChild(0).GetComponent<Text>().text = "No winners. Try again!";
            }
            */
            if (EventSystem.GetComponent<GameController>().ShowingAwards)
            {
                this.transform.GetChild(0).GetComponent<Text>().text =
               "WIN: " + EventSystem.GetComponent<State>().WinningAmount + " PESOS";
            }
            else
            {
                this.transform.GetChild(0).GetComponent<Text>().text = "No winners. Try again!";
            }
        }
    }
}
