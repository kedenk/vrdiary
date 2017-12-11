using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGoScript : MonoBehaviour {

    public string character;
    public string displayedChar;

    private Animator anim;
    private int buttonPressHash = Animator.StringToHash("ButtonPress");

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

        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Animates the button. 
    /// Looks like a button press. 
    /// </summary>
    public void animatePress()
    {
        if( anim != null )
        {
            anim.Rebind();
            //anim.SetTrigger(buttonPressHash);
        }
        else
        {
            throw new System.Exception("No Animator assigned to button"); 
        }
    }
}
