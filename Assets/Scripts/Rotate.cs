using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public float Speed;
    public float subt;
	// Use this for initialization
	void Start () {
        Speed = Random.Range(300f,350f);
	}
	
	// Update is called once per frame
	void Update () {
        subt = 0.25f;
        Speed = Speed - subt;
        if (Speed >= 0)
        {
            transform.Rotate(0, 0, Speed * Time.deltaTime);
        }


    }
}
