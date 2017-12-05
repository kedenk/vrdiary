using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PasscodeManager : MonoBehaviour {
	public const int passcodeLength = 4;

	public List<GameObject> visualizer = new List<GameObject>();
	public List<GameObject> keyFields = new List<GameObject>();
	//public PS4ControllerInput controllerInput;
	private List<ButtonType> currentPasscode = new List<ButtonType>();
	private KeyboardInput keyboardInput;
	private ButtonGoScript visualizerButtonGoScript;

	// Use this for initialization
	void Start () {
		//controllerInput = GameObject.FindGameObjectWithTag("GameController").GetComponent<PS4ControllerInput>();
		//controllerInput.onButtonPressed += onButtonPressed;
		keyboardInput = GameObject. FindGameObjectWithTag("PasscodeWall").GetComponent<KeyboardInput>();
		keyboardInput.onButtonPressed += onButtonPressed;
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
			visualizer[(int)bt].GetComponent<ButtonGoScript> ().animatePress();

			if (currentPasscode.Count < passcodeLength) {
				currentPasscode.Add(bt);
				drawPasscode ();

				if (currentPasscode.Count == passcodeLength) {
					checkPasscode ();
				}
			}

			break;
		case ButtonType.L1:
			if (currentPasscode.Count > 0) {
				currentPasscode.RemoveAt(currentPasscode.Count-1);
			}
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
				Color color = Constants.Colors.yellow;
				TextMesh textMesh = keyFields [i].GetComponentInChildren<TextMesh>();;

				switch (currentPasscode[i]) {
				case ButtonType.TRIANGLE:
					color = Constants.Colors.yellow;
					textMesh.text = "△";
					break;
				case ButtonType.CIRCLE:
					color = Constants.Colors.red;
					textMesh.text = "○";
					break;
				case ButtonType.X:
					color = Constants.Colors.green;
					textMesh.text = "✕";
					break;
				case ButtonType.QUADRAT:
					color = Constants.Colors.blue;
					textMesh.text = "□";
					break;
				default:
					break;
				}

				keyFields [i].GetComponent<Renderer> ().material.color = color;
			}
		}
	}

	void checkPasscode() {
		if (Enumerable.SequenceEqual(currentPasscode, Constants.Passcodes.userA)) {
			Debug.Log("Setup userA environment");
			gameObject.SetActive(false);
		} else if (Enumerable.SequenceEqual(currentPasscode, Constants.Passcodes.userB)) {
			Debug.Log("Setup userB environment");
		} else {
			Debug.Log("Wrong Passcode");
		}
	}
}
