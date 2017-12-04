using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS4ControllerInput : MonoBehaviour {

    #region events

    public delegate void CharacterInputEventHandler(string c); 

    public event CharacterInputEventHandler onCharInput; 

    #endregion

    public enum RotationsAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationsAxes axes = RotationsAxes.MouseXAndY;
    public float sensitivityX = 30F;
    public float sensitivityY = 30F;

    public string xAnalogAxesInput;
    public string yAnalogAxesInput;

    #region Materials

    public Material highlightedMaterial;
    public Material regularMaterial;

    public Material redButtonMaterial;
    public Material blueButtonMaterial;
    public Material yellowButtonMaterial;
    public Material greenButtonMaterial;
    public Material buttonUnSelectedMaterial;

    #endregion

    #region Categories 

    public GameObject categorie1;
    public GameObject categorie2;
    public GameObject categorie3;
    public GameObject categorie4;
    public GameObject categorie5;
    public GameObject categorie6;
    public GameObject categorie7;
    public GameObject categorie8;

    private List<GameObject> categorieObjList;

    #endregion

    private GameObject highlightedGO;
    private Dictionary<string, string> buttonMapping;

    #region Constants

    private const string DREIECK_BTN = "Dreieck";
    private const string KREIS_BTN = "Kreis";
    private const string X_BTN = "X";
    private const string VIERECK_BTN = "Viereck";

    #endregion


    // Use this for initialization
    void Start () {

        Debug.Log("Controller input started");
        string[] jNames = Input.GetJoystickNames();
        foreach (string s in jNames)
        {
            Debug.Log("Controller connected: " + s);
        }

        categorieObjList = new List<GameObject>();
        categorieObjList.Add(categorie1);
        categorieObjList.Add(categorie2);
        categorieObjList.Add(categorie3);
        categorieObjList.Add(categorie4);
        categorieObjList.Add(categorie5);
        categorieObjList.Add(categorie6);
        categorieObjList.Add(categorie7);
        categorieObjList.Add(categorie8);

        buttonMapping = new Dictionary<string, string>();
        buttonMapping.Add(DREIECK_BTN, "Button1");
        buttonMapping.Add(KREIS_BTN, "Button2");
        buttonMapping.Add(X_BTN, "Button3");
        buttonMapping.Add(VIERECK_BTN, "Button4");

        onCharInput += onCharTestMethod; 

        dehighlightPlates();
    }
	
	// Update is called once per frame
	void Update () {

        // comment out for debugging of button numbers
        //for (int i = 0; i < 20; i++)
        //{
        //    if (Input.GetKeyDown("joystick 1 button " + i))
        //    {
        //        Debug.Log("joystick 1 button " + i);
        //    }
        //}

        // Viereck
        if( Input.GetKeyDown("joystick 1 button 0"))
        {
            handleButtonPress(VIERECK_BTN);
        }

        // X
        if (Input.GetKeyDown("joystick 1 button 1"))
        {
            handleButtonPress(X_BTN);
        }

        // Kreis
        if (Input.GetKeyDown("joystick 1 button 2"))
        {
            handleButtonPress(KREIS_BTN);
        }

        // Dreieck
        if (Input.GetKeyDown("joystick 1 button 3"))
        {
            handleButtonPress(DREIECK_BTN);
        }


        if (axes == RotationsAxes.MouseXAndY && (Input.GetAxis(xAnalogAxesInput) != 0 || Input.GetAxis(yAnalogAxesInput) != 0))
        {
            //Debug.Log("AnalogSticks: y-Analog: " + Input.GetAxis(yAnalogAxesInput) + " -- x-Analog: " + Input.GetAxis(xAnalogAxesInput));
            float xAnalogValue = Input.GetAxis(xAnalogAxesInput);
            float yAnalogValue = Input.GetAxis(yAnalogAxesInput);

            dehighlightPlates();

            // area on the top
            if ((yAnalogValue >= 0.065 && yAnalogValue <= 1) && (xAnalogValue > -0.025 && xAnalogValue < 0.025))
            {
                highlightPlate(categorie1);
            }
            else if ((yAnalogValue > 0.015 && yAnalogValue < 1) && (xAnalogValue > 0 && xAnalogValue < 1))
            {
                highlightPlate(categorie2);
            }
            else if ( (yAnalogValue >= -0.025 && yAnalogValue <= 0.025) && (xAnalogValue > 0 && xAnalogValue < 1) )
            {
                highlightPlate(categorie3);
            }
            else if ((yAnalogValue <= -0.015 && yAnalogValue > -1) && (xAnalogValue > 0 && xAnalogValue < 1))
            {
                highlightPlate(categorie4);
            }
            else if ((yAnalogValue <= -0.065 && yAnalogValue > -1) && (xAnalogValue > -0.015 && xAnalogValue < 0.015))
            {
                highlightPlate(categorie5);
            }
            else if ((yAnalogValue <= -0.025 && yAnalogValue > -1) && (xAnalogValue < -0.025 && xAnalogValue > -0.065))
            {
                highlightPlate(categorie6);
            }
            else if ((yAnalogValue >= -0.025 && yAnalogValue < 0.025) && (xAnalogValue < -0.065 && xAnalogValue > -1))
            {
                highlightPlate(categorie7);
            }
            else if ((yAnalogValue >= 0.025 && yAnalogValue < 0.065) && (xAnalogValue < -0.065 && xAnalogValue > -1))
            {
                highlightPlate(categorie8);
            }
        }
        else
        {
            dehighlightPlates();
        }
    }

    #region private methods

    private void handleButtonPress(string button)
    {
        string bName = buttonMapping[button]; 
        if( bName != null && highlightedGO != null)
        {
            Transform t = highlightedGO.transform.Find(bName);
            ButtonGoScript script = t.gameObject.GetComponent<ButtonGoScript>();

            // corrosponding character of button
            string ch = script.character;
            script.animatePress();

            if ( onCharInput != null )
            {
                onCharInput(ch);
            }

            
            //Animator anim = t.gameObject.GetComponent<Animator>(); 
            //if( anim != null )
            //    t.gameObject.GetComponent<Animator>().Play(Animator.StringToHash("ButtonPress"));
        }
    }

    private void highlightPlate(GameObject categorie)
    {
        if (categorie == null)
            throw new Exception("Parameter is null"); 

        highlightedGO = categorie;
        categorie.GetComponent<Renderer>().material = highlightedMaterial;

        Transform t = highlightedGO.transform.Find("Button1");
        t.GetComponent<Renderer>().material = yellowButtonMaterial;
        t = highlightedGO.transform.Find("Button2");
        t.GetComponent<Renderer>().material = redButtonMaterial;
        t = highlightedGO.transform.Find("Button3");
        t.GetComponent<Renderer>().material = greenButtonMaterial;
        t = highlightedGO.transform.Find("Button4");
        t.GetComponent<Renderer>().material = blueButtonMaterial;
    }

    private void dehighlightPlates()
    {
        highlightedGO = null; 
        foreach (GameObject obj in categorieObjList)
        {
            if (obj != null)
            {
                obj.GetComponent<Renderer>().material = regularMaterial;

                for (int i = 1; i <= 4; i++)
                {
                    Transform t = obj.transform.Find("Button" + i);
                    t.GetComponent<Renderer>().material = buttonUnSelectedMaterial;
                }
            }
        }
    }

    #endregion

    #region testing

    private void onCharTestMethod(string c)
    {
        Debug.Log("[TEST] Input recognized: '" + c + "'.");
    }

    #endregion
}
