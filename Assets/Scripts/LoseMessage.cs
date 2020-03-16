using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseMessage : MonoBehaviour
{
	public GameObject spriteHolder;
	public static LoseMessage Instance;
	public Sprite[] images;
	public Sprite[] imagesSpanish;

	void Awake(){
		Instance = this;
	}
	public void AssignLoseSprite(int world){
		RectTransform rt = spriteHolder.GetComponent<RectTransform>();
		if(world>4)
		world=4;
		if(LanguageHandler.IsEnglish())
		{
			spriteHolder.GetComponent<Image>().sprite = images[world-1];
			rt.sizeDelta = new Vector2(263, rt.sizeDelta.y) ;	

		}
		
		else
		{
			spriteHolder.GetComponent<Image>().sprite = imagesSpanish[world-1];	
			rt.sizeDelta = new Vector2(444, rt.sizeDelta.y);	

		}
		
	}
}
