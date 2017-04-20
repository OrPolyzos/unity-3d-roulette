using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public float Speed;
    public float subt;
	// Use this for initialization
	void Start () {
        Speed = Random.Range(200f,250f);
        subt = 0.25f;
    }
	
	// Update is called once per frame
	void Update () {
        
        Speed = Speed - subt;
        if (Speed >= 0)
        {
            transform.Rotate(0, 0, Speed * Time.deltaTime);
        }
    }
    
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.Equals("Sphere"))
        {
            subt = 0.4f;
        }
    }
}
