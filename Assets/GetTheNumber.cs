using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTheNumber : MonoBehaviour {

    public string WinningNumber;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerStay(Collider sensor)
    {
        switch (sensor.gameObject.name)
        {
            case "Sensor N0":
                {
                    WinningNumber = "0";
                    break;
                }
            case "Sensor N1":
                {
                    WinningNumber = "1";
                    break;
                }
            case "Sensor N2":
                {
                    WinningNumber = "2";
                    break;
                }
            case "Sensor N3":
                {
                    WinningNumber = "3";
                    break;
                }
            case "Sensor N4":
                {
                    WinningNumber = "4";
                    break;
                }
            case "Sensor N5":
                {
                    WinningNumber = "5";
                    break;
                }
            case "Sensor N6":
                {
                    WinningNumber = "6";
                    break;
                }
            case "Sensor N7":
                {
                    WinningNumber = "7";
                    break;
                }
            case "Sensor N8":
                {
                    WinningNumber = "8";
                    break;
                }
            case "Sensor N9":
                {
                    WinningNumber = "9";
                    break;
                }
            case "Sensor N10":
                {
                    WinningNumber = "10";
                    break;
                }
            case "Sensor N11":
                {
                    WinningNumber = "11";
                    break;
                }
            case "Sensor N12":
                {
                    WinningNumber = "12";
                    break;
                }
        }
    }
}
