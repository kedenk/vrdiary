using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasscodeManager : MonoBehaviour {
	public const int passcodeLength = 4;

	public List<GameObject> keyFields = new List<GameObject>();
	public PS4ControllerInput controllerInput;
	private List<ButtonType> currentPasscode = new List<ButtonType>();

	// Use this for initialization
	void Start () {
		controllerInput = GameObject.FindGameObjectWithTag("GameController").GetComponent<PS4ControllerInput>();
		controllerInput.onButtonPressed += onButtonPressed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void onButtonPressed(ButtonType bt) {
		switch (bt) {
		case ButtonType.TRIANGLE:
		case ButtonType.CIRCLE:
		case ButtonType.X:
		case ButtonType.QUADRAT:
			if (currentPasscode.Count <= passcodeLength) {
				currentPasscode.Add(bt);
				drawPasscode ();
			}
			break;
		case ButtonType.L1:
			currentPasscode.RemoveAt(currentPasscode.Count-1);
			drawPasscode ();
			break;
		default:
			break;
		}
	}

	void drawPasscode() {
		for (int i = 0; i < passcodeLength; i++) {
			keyFields [i].SetActive (i < currentPasscode.Count);
			if (i < currentPasscode.Count) {
				keyFields [i].GetComponent<Renderer> ().sharedMaterial.color = Constants.Colors.yellow;
					
			}
		}
	}
}
