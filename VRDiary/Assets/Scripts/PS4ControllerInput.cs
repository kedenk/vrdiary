using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS4ControllerInput : MonoBehaviour {

    public enum RotationsAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationsAxes axes = RotationsAxes.MouseXAndY;
    public float sensitivityX = 30F;
    public float sensitivityY = 30F;

    public string rAnalogAxesInput = "PS4_RAnalog";
    public string lAnalogAxesInput = "PS4_LAnalog";

    // Use this for initialization
    void Start () {

        string[] jNames = Input.GetJoystickNames();
        foreach(string s in jNames)
            Debug.Log("Controller connected: " + s);
	}
	
	// Update is called once per frame
	void Update () {

        for (int i = 0; i < 20; i++)
        {
            if (Input.GetKeyDown("joystick 1 button " + i))
            {
                // do something
                Debug.Log("joystick 1 button " + i);
            }
        }

        if (axes == RotationsAxes.MouseXAndY && (Input.GetAxis(rAnalogAxesInput) != 0 || Input.GetAxis(lAnalogAxesInput) != 0))
        {
            Debug.Log("AnalogSticks: L-Analog: " + Input.GetAxis(lAnalogAxesInput) + " -- R-Analog: " + Input.GetAxis(rAnalogAxesInput));
        }
    }
}
