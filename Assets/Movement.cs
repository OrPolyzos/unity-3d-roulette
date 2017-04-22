using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	// Use this for initialization
	void Start () {
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("CameraMov"), "time", 45));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
