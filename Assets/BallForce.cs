using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallForce : MonoBehaviour {
    

	// Use this for initialization
	void Start () {
        this.GetComponent<Rigidbody>().isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.GetComponent<Rigidbody>().isKinematic = false;

            this.GetComponent<Rigidbody>().AddForce(0, 0, -150f, ForceMode.VelocityChange);
        }

    }
}
