using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConfig : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Constants.CameraSetting cs = Constants.EnvCameraSettings.getEnvCameraSetting("std"); 

        setCamera(cs.pos, cs.scale);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setCamera(Vector3 pos, Vector3 scale)
    {
        Debug.Log("Setting camera settings");
        Debug.Log("Pos: " + pos);

        gameObject.transform.position = pos;
        gameObject.transform.localScale = scale;
    }
}
