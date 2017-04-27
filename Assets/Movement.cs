using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {   
    }

    public void MoveCameraDown()
    {
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("CameraMov"), "time", 45));
    }
    public void MoveCameraUp()
    {
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("CameraMovRev"), "time", 20));

    }
}
