using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject Sphere;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void startGame()
	{
		Debug.Log ("Game Starts");
		Sphere.GetComponent<Rigidbody>().isKinematic = false;

		Sphere.GetComponent<Rigidbody>().AddForce(0, 0, -150f, ForceMode.VelocityChange);
	}
}
