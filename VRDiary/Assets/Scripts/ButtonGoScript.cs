using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGoScript : MonoBehaviour {

    public string character;

	// Use this for initialization
	void Start () {

        TextMesh textMesh = gameObject.GetComponentInChildren<TextMesh>();
        if (textMesh != null)
        {
            textMesh.text = character;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
