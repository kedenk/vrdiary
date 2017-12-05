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

    // Use this for initialization
    void Start () {
        controllerInput = GameObject.FindGameObjectWithTag("GameController").GetComponent<PS4ControllerInput>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            controllerInput.virtualButtonPress(ButtonType.QUADRAT);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            controllerInput.virtualButtonPress(ButtonType.TRIANGLE);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            controllerInput.virtualButtonPress(ButtonType.CIRCLE);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            controllerInput.virtualButtonPress(ButtonType.X);
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
        } else
        {
            mainCamera.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }
}
