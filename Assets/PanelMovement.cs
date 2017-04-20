using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("PanelMov"), "time", 12.5));

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
