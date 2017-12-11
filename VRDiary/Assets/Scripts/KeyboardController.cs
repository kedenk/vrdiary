using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour {

    public PS4ControllerInput controllerInput;
    public Camera mainCamera; 

    public float minX = -360.0f;
    public float maxX = 360.0f;

    public float minY = -45.0f;
    public float maxY = 45.0f;

    public float sensX = 100.0f;
    public float sensY = 100.0f;

    float rotationY = 0.0f;
    float rotationX = 0.0f;

    Dictionary<KeyCode, ButtonType> keyMapping; 

    // Use this for initialization
    void Start () {
        controllerInput = GameObject.FindGameObjectWithTag("GameController").GetComponent<PS4ControllerInput>();

        keyMapping = new Dictionary<KeyCode, ButtonType>(); 

        keyMapping.Add(KeyCode.UpArrow, ButtonType.TRIANGLE);
        keyMapping.Add(KeyCode.LeftArrow, ButtonType.QUADRAT);
        keyMapping.Add(KeyCode.RightArrow, ButtonType.CIRCLE);
        keyMapping.Add(KeyCode.DownArrow, ButtonType.X);

        keyMapping.Add(KeyCode.Backspace, ButtonType.L1); 

        keyMapping.Add(KeyCode.Escape, ButtonType.SHARE); 
    }

    // Update is called once per frame
    void Update()
    {
        //
        // Check for key presses
        // 
        foreach(KeyCode kc in keyMapping.Keys)
        {
            if( Input.GetKeyDown(kc) && controllerInput != null)
            {
                controllerInput.virtualButtonPress(keyMapping[kc]);
            }
        }

        //
        // For camera moving
        //
        if (Input.GetKey(KeyCode.C) && mainCamera != null )
        {
            rotationX += Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
            rotationY += Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;
            rotationY = Mathf.Clamp(rotationY, minY, maxY);
            
            mainCamera.transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        else
        {
            mainCamera.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }
}
