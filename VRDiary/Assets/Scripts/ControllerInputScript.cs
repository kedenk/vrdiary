using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerInputScript : MonoBehaviour {

	public GameObject controller;

	private PS4ControllerInput controllerInput;
	private InputField inputField;

	//TODO: save diary|different users|different days

	void Start ()
	{

		inputField = gameObject.GetComponent<InputField>();
		controllerInput = controller.GetComponent<PS4ControllerInput>();
		controllerInput.onButtonPressed += onButtonPressed;
		controllerInput.onCharInput += onCharInput;
	}

	void onButtonPressed(ButtonType btn) {
		switch (btn) {
		case ButtonType.L1:
			inputField.text = inputField.text.Remove (inputField.text.Length - 1);
			break;
		}

	}

	void onCharInput(string charInput) {
		if (charInput != null) {
			inputField.text += charInput;
		}
	}

	// Update is called once per frame
	void Update () {

		//input.text += "test " ;
	}
}
