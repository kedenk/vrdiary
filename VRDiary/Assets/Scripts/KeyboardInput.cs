using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour {

	#region events

	public delegate void ControllerButtonPressedHandler(ButtonType bt); 

	public event ControllerButtonPressedHandler onButtonPressed; 

	#endregion

	// Use this for initialization
	void Start () {
		onButtonPressed += onButtonPressedTestMethod; 
	}

	// Update is called once per frame
	void Update () {
		List<string> buttonIDs = new List<string> (new string[] {"up", "right", "down", "left", "backspace"});
		foreach (string buttonID in buttonIDs) {
			if (Input.GetKeyDown (buttonID)) {
				try {
					if (onButtonPressed != null)
						onButtonPressed (parseButtonId (buttonID));
				} catch (Exception e) {
					Debug.LogError ("Undefined button id. " + e.Message);
				}

			}
		}
	}

	private ButtonType parseButtonId(string buttonID)
	{
		switch(buttonID)
		{
		case "left": return ButtonType.QUADRAT; 

		case "down": return ButtonType.X;

		case "right": return ButtonType.CIRCLE;

		case "up": return ButtonType.TRIANGLE;

		case "backspace": return ButtonType.L1;
		default:
			throw new Exception("No Enum variable for button id '" + buttonID + "'.");
		}
	}

	#region testing

	private void onButtonPressedTestMethod(ButtonType bt)
	{
		Debug.Log("[DEBUG] Button pressed: '" + bt + "'.");
	}

	#endregion
}