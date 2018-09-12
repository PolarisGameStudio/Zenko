using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssignCanvasAndButton : MonoBehaviour {

	public bool isnext;
	public bool isreset;
	SceneLoading sl;
	// Use this for initialization
	void Awake () {
		GameObject canvasobj = GameObject.Find("Main Canvas");
		sl = canvasobj.GetComponent<SceneLoading>();
		Button btn1 = GetComponent<Button>();
		if(isnext)
		btn1.onClick.AddListener(sl.NextlevelButton);
		if(isreset)
		btn1.onClick.AddListener(sl.ResetLevelButton);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
