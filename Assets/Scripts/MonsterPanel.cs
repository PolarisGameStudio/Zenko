using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPanel : MonoBehaviour {
	float myheight;
	float mywidth;
	bool mygiven;
	public float heightdivider;
	public float widthdivider;
	public static float monsterAreaHeight;
	RectTransform mytransform;
	// Use this for initialization
	void Start () {
		mygiven = false;
		mytransform = GetComponent<RectTransform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (CanvasBehaviour.given == true && mygiven == false) { //do this after the canvas behaviour coroutine giving size is done
			SetSize ();
			mygiven = true;
		}
		else if(CanvasBehaviour.height != mytransform.rect.height && myheight != null ){ //do this when the resolution changes (for tests mostly)
			SetSize();
		}
	}
	void SetSize(){
		myheight = CanvasBehaviour.height / heightdivider;
		mywidth = CanvasBehaviour.width / widthdivider;
		mytransform.sizeDelta = new Vector2 (mywidth, myheight);
		monsterAreaHeight = myheight;
	}
}
