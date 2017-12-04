using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGoScript : MonoBehaviour {

    public string character;
    public string displayedChar; 

	// Use this for initialization
	void Start () {

        TextMesh textMesh = gameObject.GetComponentInChildren<TextMesh>();
        if (textMesh != null)
        {
            if( !string.IsNullOrEmpty(displayedChar) )
            {
                textMesh.text = displayedChar; 
            }
            else
            {
                textMesh.text = character;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
