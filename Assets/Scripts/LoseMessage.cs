using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseMessage : MonoBehaviour
{
	public GameObject spriteHolder;
	public static LoseMessage Instance;
	public Sprite[] images;

	void Awake(){
		Instance = this;
	}
	public void AssignLoseSprite(int world){
		spriteHolder.GetComponent<Image>().sprite = images[world-1];
	}
}
