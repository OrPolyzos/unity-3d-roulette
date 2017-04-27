using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallForce : MonoBehaviour {

    public bool Icollided = false;
    public float force;

	// Use this for initialization
	void Start () {
        force = Random.Range(-105f, -95f);
	}
	
	// Update is called once per frame
	void Update () {
    }

    void OnCollisionEnter(Collision col)
    {
        if (Icollided == false)
        {
            Icollided = true;
            this.GetComponent<Rigidbody>().AddForce(0, 0, force, ForceMode.VelocityChange);
        }

    }
}
