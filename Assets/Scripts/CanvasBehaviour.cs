using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasBehaviour : MonoBehaviour {

	public static float height;
	public static float width;
	public static bool given;
	RectTransform RTransform;
	// Use this for initialization
	void Start () {
		given = false;
		RTransform = GetComponent<RectTransform> ();
		setWidth ();

	}

	private void setWidth () {

		StartCoroutine(delayWidth());

	}

	private IEnumerator delayWidth () {

		yield return 0;

		width = RTransform.rect.width;
		height = RTransform.rect.height;

		//Debug.Log(height + " + " + width);
		given = true;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			setWidth ();
		}
		if (height != RTransform.rect.height) {
			setWidth ();
		}
	}
}
