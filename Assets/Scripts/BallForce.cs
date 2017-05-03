using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallForce : MonoBehaviour {

    public bool Icollided = false;
    public float force;
    public AudioClip RollingBall;

	// Use this for initialization
	void Start () {
        force = Random.Range(-105f, -95f);
	}
	
	// Update is called once per frame
	void Update () {
        //if (this.GetComponent<AudioSource>().pitch > 0.8f)
        //{
        //    this.GetComponent<AudioSource>().pitch = this.GetComponent<AudioSource>().pitch - 0.0005f;
        //}
    }

    void OnCollisionEnter(Collision col)
    {
        if (Icollided == false)
        {
            Icollided = true;
            this.GetComponent<Rigidbody>().AddForce(0, 0, force, ForceMode.VelocityChange);
            //this.GetComponent<AudioSource>().Play();
        }


    }
}
