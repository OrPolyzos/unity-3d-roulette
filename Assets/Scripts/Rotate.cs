using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public float Speed;
    public float subt;
    public GameObject EventSystem;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (Speed >= 0)
        {
            Speed = Speed - subt;
            transform.Rotate(0, 0, Speed * Time.deltaTime);
        }
        if (EventSystem.GetComponent<State>().GameState.Equals("GameStarted")) {
            if (Speed <= 0)
            {
                EventSystem.GetComponent<GameController>().MessagePanel.GetComponent<PanelMovement>().IWasMovedDown = false;
                EventSystem.GetComponent<State>().GameState = "GameEnded";
				StartCoroutine( EventSystem.GetComponent<GameController> ().displayAwardInformation ());
            }
        }
    }
    
    public void SetSpeedAgain()
    {
        Speed = Random.Range(200f, 250f);
        subt = 0.25f;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.Equals("Sphere"))
        {
            subt = 0.375f;
        }
    }
}
