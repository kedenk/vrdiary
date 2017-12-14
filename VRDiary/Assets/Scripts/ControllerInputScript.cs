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
	public DateTime date;
	public string userID;
	public DateTime currentDate;

	//TODO: save diary|different users|different days
	//Evtl. save auslagern in eigene klasse

	void Start ()
	{

		inputField = gameObject.GetComponent<InputField>();
		controllerInput = controller.GetComponent<PS4ControllerInput>();
		controllerInput.onButtonPressed += onButtonPressed;
		controllerInput.onCharInput += onCharInput;
		userID = "Tobi";
		date = System.DateTime.Now;
		currentDate = date;
		Text placeholder = inputField.placeholder.GetComponent<Text>();
		placeholder.text = userID + "; " + date;
	}

	void onButtonPressed(ButtonType btn) {
		switch (btn) {
		case ButtonType.L1:
			if (inputField.text.Length > 0) {
				inputField.text = inputField.text.Remove (inputField.text.Length - 1);
			}
			break;
        case ButtonType.R1:
                inputField.text += " ";
                break;
		case ButtonType.SHARE:
			saveText ();
			break;
		case ButtonType.OPTIONS:
            loadText (currentDate, userID);
			break;
		//case ButtonType.DateChangeButton: TBD
		//	currentDate = currentDate.Add(new TimeSpan(24,0,0));
		//	loadText (currentDate, userID);
        //	break;
		default:
			break;
		}
	}

	void onCharInput(string charInput) {
		if (charInput != null) {
			inputField.text += charInput;
		}
	}

	public void saveText(){
		string d_string = currentDate.DayOfWeek.ToString();
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/diary_" + d_string + "_" + userID + ".if");
		Debug.Log ("File saved at " + Application.persistentDataPath + "/diary_" + d_string + "_" + userID + ".if");
		bf.Serialize(file, new TextField(inputField.text,userID, currentDate));
		file.Close();
	}

	public void loadText(DateTime currentDate, string userID){
		string d_string = currentDate.DayOfWeek.ToString();
		if(File.Exists(Application.persistentDataPath + "/diary_" + d_string + "_" + userID + ".if")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/diary_" + d_string + "_" + userID + ".if", FileMode.Open);
			TextField data = (TextField)bf.Deserialize(file);
			file.Close();

			inputField.text = data.userText;
			this.userID = data.userID;
			this.currentDate = data.currentDate;
			Debug.Log (userText);
		}
	}	

	// Update is called once per frame
	void Update () {
		//if (userText.Length != 0) {
			//inputField.text = userText;
		//	Debug.Log (userText);
		//}
	}


}
[System.Serializable]
public class TextField {

	public String userText;
	public String userID;
	public DateTime currentDate;

	public TextField(string userText, string userID, DateTime currentDate) {
		this.userText = userText;
		this.userID = userID;
		this.currentDate = currentDate;
	}

}