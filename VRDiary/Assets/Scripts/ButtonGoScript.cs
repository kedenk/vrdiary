using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGoScript : MonoBehaviour {

    public string character;
    public string characterBig; 
    public string displayedChar;

    private Animator anim;
    private int buttonPressHash = Animator.StringToHash("ButtonPress");

    private TextMesh textMesh; 

    // Use this for initialization
    void Start () {

        textMesh = gameObject.GetComponentInChildren<TextMesh>();

        if (textMesh != null)
        {
            textMesh.color = Color.black;
            if ( !string.IsNullOrEmpty(displayedChar) )
            {
                textMesh.text = displayedChar; 
            }
            else
            {
                textMesh.text = characterBig;
            }
        }

        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Returns the current visualized character
    /// </summary>
    /// <returns></returns>
    public string getCurrentChar()
    {
        return textMesh.text;
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

    public void toggleChars()
    {
        string current = textMesh.text; 
        if ( !current.Equals(displayedChar) && !string.IsNullOrEmpty(character) && !string.IsNullOrEmpty(characterBig) )
        {
            if( current.Equals(character) )
            {
                textMesh.text = characterBig; 
            }
            else
            {
                textMesh.text = character; 
            }
        }
    }
}
