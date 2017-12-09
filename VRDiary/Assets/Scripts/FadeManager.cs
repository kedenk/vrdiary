using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour {

	public static FadeManager Instance{set;get;}

	private bool isInTransition;
	private float transition;
	private bool isShowing;
	private float duration;

	private void Awake() {
		Instance = this;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isInTransition) {
			transition += (isShowing) ? Time.deltaTime * (1 / duration) : -Time.deltaTime * (1 / duration);
			Renderer[] rendererArray = gameObject.GetComponentsInChildren<Renderer> (true);
			List<Renderer> rendererList = new List<Renderer> (rendererArray);
			rendererList.Add(gameObject.GetComponent<Renderer> ());

			foreach (Renderer renderer in rendererList) {
				Color originalColor = renderer.material.color;
				originalColor.a = 1;
				Color transparentColor = renderer.material.color;
				transparentColor.a = 0;
				renderer.material.color = Color.Lerp(transparentColor, originalColor, transition);
			}

			if (transition > 1 || transition < 0) {
				isInTransition = false;
				gameObject.SetActive (false);
			}
		}
	}

	public void Fade(bool showing, float duration) {
		
		isShowing = showing;
		isInTransition = true;
		this.duration = duration;
		transition = (isShowing) ? 0 : 1;
	}
}
