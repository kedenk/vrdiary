using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class PasscodeManager : MonoBehaviour {
	public const int passcodeLength = 4;

	public List<GameObject> visualizer = new List<GameObject>();
	public List<GameObject> keyFields = new List<GameObject>();
	public GameObject environmentA;
	public GameObject environmentB;
	private PS4ControllerInput controllerInput;
	public AudioClip successSound;
	public AudioClip failSound;
	private List<ButtonType> currentPasscode = new List<ButtonType>();

	// Use this for initialization
	void Start () {
		controllerInput = GameObject.FindGameObjectWithTag("GameController").GetComponent<PS4ControllerInput>();
		controllerInput.onButtonPressed += onButtonPressed;

		environmentA.SetActive (false);
		environmentB.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void onButtonPressed(ButtonType bt) {
		switch (bt) {
		case ButtonType.TRIANGLE:
		case ButtonType.CIRCLE:
		case ButtonType.X:
		case ButtonType.QUADRAT:
			visualizer[(int)bt].GetComponent<ButtonGoScript> ().animatePress();

			if (currentPasscode.Count < passcodeLength) {
				currentPasscode.Add(bt);
				drawPasscode ();

				if (currentPasscode.Count == passcodeLength) {
					checkPasscode ();
				}
			}

			break;
		case ButtonType.L1:
			if (currentPasscode.Count > 0) {
				currentPasscode.RemoveAt(currentPasscode.Count-1);
			}
			drawPasscode ();
			break;

        case ButtonType.PS:
            logout();
            break;

		default:
			break;
		}
	}

	void drawPasscode() {
		for (int i = 0; i < passcodeLength; i++) {
			keyFields [i].SetActive (i < currentPasscode.Count);

			if (i < currentPasscode.Count) {
				Color color = Constants.Colors.yellow;
				string sprite = "Triangle";

				switch (currentPasscode[i]) {
				case ButtonType.TRIANGLE:
					color = Constants.Colors.yellow;
					sprite = "Triangle";
					break;
				case ButtonType.CIRCLE:
					color = Constants.Colors.red;
					sprite = "Circle";
					break;
				case ButtonType.X:
					color = Constants.Colors.green;
					sprite = "Cross";
					break;
				case ButtonType.QUADRAT:
					color = Constants.Colors.blue;
					sprite = "Square";
					break;
				default:
					break;
				}

				Sprite[] sprites = Resources.LoadAll<Sprite> ("Sprites");
				keyFields [i].GetComponentInChildren<SpriteRenderer> ().sprite = sprites.Where(a => a.name == sprite).First();
				keyFields [i].GetComponent<Renderer>().material.color = color;
			}
		}
	}

	void checkPasscode() {
		if (Enumerable.SequenceEqual(currentPasscode, Constants.Passcodes.userA)) {

            setupEnv(environmentA);

        } else if (Enumerable.SequenceEqual(currentPasscode, Constants.Passcodes.userB)) {

            setupEnv(environmentB);

		} else {
			Debug.Log("Wrong Passcode");
			AudioSource.PlayClipAtPoint (failSound, transform.position);
		}
	}

    void setupEnv(GameObject env)
    {
        Debug.Log("Setup env " + env.name);
        Action callback = () => { gameObject.SetActive(false); };
        gameObject.GetComponent<FadeManager>().FadeOut(1f, callback);
        env.SetActive(true);
        env.GetComponent<FadeManager>().FadeIn(6f, null);

        //set camera
        setCamera(env);

        AudioSource.PlayClipAtPoint(successSound, transform.position);
    }

    private void setCamera(GameObject env)
    {
        setCamera(env.name);
    }

    private void setCamera(string envName)
    {
        Constants.CameraSetting cs = Constants.EnvCameraSettings.getEnvCameraSetting(envName);
        CameraConfig cameraScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CameraConfig>();
        cameraScript.setCamera(cs.pos, cs.scale);
    }

    public void logout()
    {
        // only logout if we are in an environment
        if (!gameObject.activeInHierarchy)
        {
            Debug.Log("logout");

            gameObject.SetActive(true);
            environmentA.SetActive(false);
			environmentB.SetActive(false);

            currentPasscode.Clear();

            gameObject.GetComponent<FadeManager>().FadeIn(1f, null);

            drawPasscode();

            setCamera("std");
        }
    }
}
