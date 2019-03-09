using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatingButtonText : MonoBehaviour {
	public int levelnum;
	//public Text ratingtext;
	// Use this for initialization
	void Start () {
		this.GetComponent<Text>().text = LevelStorer.leveldic[levelnum].rating.ToString();
		//Debug.Log("Boop" + "Level " + levelnum + "giving " + );
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
