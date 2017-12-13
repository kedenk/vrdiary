using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ButtonType
{
    TRIANGLE = 0,
    CIRCLE = 1,
    X = 2,
    QUADRAT = 3,
	L1 = 4,
	L2 = 5,
	L3 = 6,
    R1 = 7,
    R2 = 8,
    R3 = 9,
    OPTIONS = 10,
    SHARE = 11,
    PS = 12,
    DPAD = 13
}

public class PS4ControllerInput : MonoBehaviour {

    #region events

    public delegate void CharacterInputEventHandler(string c);
    public delegate void ControllerButtonPressedHandler(ButtonType bt); 

    public event CharacterInputEventHandler onCharInput;
    public event ControllerButtonPressedHandler onButtonPressed; 

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
    private Dictionary<ButtonType, string> buttonMapping;
    private Dictionary<string, ButtonType> keyMapping;

    #region Constants

    private const int catButtonCount = 4; 

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

        buttonMapping = new Dictionary<ButtonType, string>();
        buttonMapping.Add(ButtonType.TRIANGLE, "Button1");
        buttonMapping.Add(ButtonType.CIRCLE, "Button2");
        buttonMapping.Add(ButtonType.X, "Button3");
        buttonMapping.Add(ButtonType.QUADRAT, "Button4");

        onCharInput += onCharTestMethod;
        onButtonPressed += onButtonPressedTestMethod; 

        dehighlightPlates();

        keyMapping = new Dictionary<string, ButtonType>();

#if UNITY_ANDROID
        
        //
        // Do here Android related things only
        // 

        Debug.Log("Android environment detected"); 

        keyMapping.Add("joystick 1 button 0", ButtonType.QUADRAT);
        keyMapping.Add("joystick 1 button 1", ButtonType.X);
        keyMapping.Add("joystick 1 button 2", ButtonType.TRIANGLE);
        keyMapping.Add("joystick 1 button 3", ButtonType.L1);
        keyMapping.Add("joystick 1 button 4", ButtonType.L2);
        keyMapping.Add("joystick 1 button 5", ButtonType.R2);
        keyMapping.Add("joystick 1 button 6", ButtonType.SHARE);
        keyMapping.Add("joystick 1 button 7", ButtonType.OPTIONS);
        keyMapping.Add("joystick 1 button 8", ButtonType.DPAD);
        //androidKeyMapping.Add("joystick 1 button 9", ButtonType.);
        keyMapping.Add("joystick 1 button 10", ButtonType.R3);
        keyMapping.Add("joystick 1 button 11", ButtonType.L3);
		keyMapping.Add("joystick 1 button 12", ButtonType.PS);
        keyMapping.Add("joystick 1 button 13", ButtonType.CIRCLE);
        keyMapping.Add("joystick 1 button 14", ButtonType.R1);

#endif
#if UNITY_STANDALONE

        //
        // Do here Computer plattform things only
        // 

        Debug.Log("Computer detected");

        keyMapping.Add("joystick 1 button 0", ButtonType.QUADRAT);
        keyMapping.Add("joystick 1 button 1", ButtonType.X);
        keyMapping.Add("joystick 1 button 3", ButtonType.TRIANGLE);
        keyMapping.Add("joystick 1 button 4", ButtonType.L1);
        keyMapping.Add("joystick 1 button 6", ButtonType.L2);
        keyMapping.Add("joystick 1 button 5", ButtonType.R1);
        keyMapping.Add("joystick 1 button 7", ButtonType.R2);
        keyMapping.Add("joystick 1 button 8", ButtonType.SHARE);
        keyMapping.Add("joystick 1 button 9", ButtonType.OPTIONS);
        keyMapping.Add("joystick 1 button 13", ButtonType.DPAD);
        keyMapping.Add("joystick 1 button 11", ButtonType.R3);
        keyMapping.Add("joystick 1 button 10", ButtonType.L3);
        keyMapping.Add("joystick 1 button 12", ButtonType.PS);
        keyMapping.Add("joystick 1 button 2", ButtonType.CIRCLE);

#endif
    }

    // Update is called once per frame
    void Update () {

        //comment out for debugging of button numbers
        for (int i = 0; i < 20; i++)
        {
            string buttonId = "joystick 1 button " + i; 
            if (Input.GetKeyDown(buttonId))
            {
                try
                {
                    if (onButtonPressed != null)
                        onButtonPressed(parseButtonId(buttonId));
                }
                catch(Exception e)
                {
                    Debug.LogError("Undefined button id. " + e.Message);
                }

            }
        }

        //
        // Observe standard selection keys
        //
        foreach(ButtonType bt in buttonMapping.Keys)
        {
            if (Input.GetKeyDown(getKeyMappingKey(bt)))
            {
                handleButtonPress(buttonMapping[bt]);
            }
        }

        if( Input.GetKeyDown( getKeyMappingKey(ButtonType.R3)) )
        {
            toggleButtonChar();
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

    public void virtualButtonPress(ButtonType bt)
    {
        if( onButtonPressed != null )
        {
            onButtonPressed(bt);
        }
    }

#region private methods

    private void handleButtonPress(string buttonName)
    {
        if(buttonName != null && highlightedGO != null)
        {
            Transform t = highlightedGO.transform.Find(buttonName);
            ButtonGoScript script = t.gameObject.GetComponent<ButtonGoScript>();

            // corrosponding character of button
            string ch = script.getCurrentChar();
            script.animatePress();

            if ( onCharInput != null )
            {
                onCharInput(ch);
            }
        }
    }

    private void highlightPlate(GameObject categorie)
    {
        showControllerPanel(true);

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
        showControllerPanel(false);

        highlightedGO = null; 
        foreach (GameObject obj in categorieObjList)
        {
            if (obj != null)
            {
                obj.GetComponent<Renderer>().material = regularMaterial;

                for (int i = 1; i <= catButtonCount; i++)
                {
                    Transform t = obj.transform.Find("Button" + i);
                    t.GetComponent<Renderer>().material = buttonUnSelectedMaterial;
                }
            }
        }
    }

    private void showControllerPanel(Boolean show)
    {
        Renderer[] renderer = gameObject.GetComponentsInChildren<Renderer>(true); 
        foreach(Renderer r in renderer)
        {
            r.enabled = show; 
        }
    }

    private ButtonType parseButtonId(string buttonID)
    {

        if( keyMapping.ContainsKey(buttonID) )
        {
            return keyMapping[buttonID]; 
        }
        else
        {
            throw new Exception("No Enum variable for button id '" + buttonID + "'.");
        }
    }

    private string getKeyMappingKey(ButtonType value)
    {
        foreach(string key in keyMapping.Keys)
        {
            if( keyMapping[key] == value)
            {
                return key; 
            }
        }

        return null; 
    }

    private void toggleButtonChar()
    {
        foreach (GameObject categorie in categorieObjList)
        {
            for (int i = 1; i <= catButtonCount; i++)
            {
                try
                {
                    Transform t = categorie.transform.Find("Button" + i);
                    ButtonGoScript script = t.gameObject.GetComponent<ButtonGoScript>();

                    script.toggleChars();
                } catch(Exception e)
                {
                    Debug.LogError(e.Message);
                }
            }
        }
    }

#endregion

#region testing

    private void onCharTestMethod(string c)
    {
        Debug.Log("[DEBUG] Input recognized: '" + c + "'.");
    }

    private void onButtonPressedTestMethod(ButtonType bt)
    {
        Debug.Log("[DEBUG] Button pressed: '" + bt + "'.");
    }

#endregion
}
