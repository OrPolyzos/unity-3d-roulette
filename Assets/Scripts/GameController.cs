using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject Sphere;
    public GameObject Roulette;

	public Button Jugar;
	public Sprite JugarNormal;
	public Sprite JugarLit;
	public bool isJugarClicked = false;
	
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void startGame()
	{
		Debug.Log ("Game Starts");
		if (isJugarClicked) {
			Jugar.image.overrideSprite = JugarNormal;
		} else {
			isJugarClicked = true;
			Jugar.image.overrideSprite = JugarLit;
			Sphere.GetComponent<Rigidbody> ().isKinematic = false;
            Roulette.GetComponent<Rotate>().enabled = true;
		}
	}
}
