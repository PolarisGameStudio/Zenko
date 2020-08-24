using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasBehaviour : MonoBehaviour {

	public static float height;
	public static float width;
	public static bool given;
	RectTransform RTransform;

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
		given = true;

	}
}
