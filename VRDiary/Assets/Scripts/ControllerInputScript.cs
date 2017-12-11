using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ControllManager : MonoBehaviour{


	private ControllerInputScript inputScript;

	void Start() {
	}



}

public class ControllerInputScript : MonoBehaviour {

	public GameObject controller;

	private PS4ControllerInput controllerInput;
	private InputField inputField;

	public string userText;
	public string date;
	public string userID;

	//TODO: save diary|different users|different days
	//Evtl. save auslagern in eigene klasse

	void Start ()
	{

		inputField = gameObject.GetComponent<InputField>();
		controllerInput = controller.GetComponent<PS4ControllerInput>();
		controllerInput.onButtonPressed += onButtonPressed;
		controllerInput.onCharInput += onCharInput;
		userID = "Tobi";
		date = System.DateTime.Now.DayOfWeek.ToString();

		Text placeholder = inputField.placeholder.GetComponent<Text>();
		placeholder.text = userID + "; " + date;
	}

	void onButtonPressed(ButtonType btn) {
		switch (btn) {
		case ButtonType.L1:
			if (userText.Length > 0) {
				userText = userText.Remove (inputField.text.Length - 1);
			}
			break;
		case ButtonType.L2:
			saveText ();
			break;
		case ButtonType.R2:
			loadText (date, userID);
			break;
		default:
			break;
		}
	}

	void onCharInput(string charInput) {
		if (charInput != null) {
			userText += charInput;
		}
	}

	public void saveText(){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/diary_" + date + "_" + userID + ".if");
		Debug.Log ("File saved at " + Application.persistentDataPath + "/diary_" + date + "_" + userID + ".if");
		bf.Serialize(file, new TextField(userText,userID, date));
		file.Close();
	}

	public void loadText(string date, string userID){
		if(File.Exists(Application.persistentDataPath + "/diary_" + date + "_" + userID + ".if")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/diary_" + date + "_" + userID + ".if", FileMode.Open);
			TextField data = (TextField)bf.Deserialize(file);
			file.Close();

			this.userText = data.userText;
			this.userID = data.userID;
			this.date = data.date;
			Debug.Log (userText);
		}
	}	

	// Update is called once per frame
	void Update () {
		if (userText.Length != 0) {
			inputField.text = userText;
			Debug.Log (userText);
		}
	}


}
[System.Serializable]
public class TextField {

	public String userText;
	public String userID;
	public String date;

	public TextField(string userText, string userID, string date) {
		this.userText = userText;
		this.userID = userID;
		this.date = date;
	}

}