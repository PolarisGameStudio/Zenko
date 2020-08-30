using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{

	[SerializeField]
	public GameModeHandler gameScript;
	public static Loading Instance;
	public Image image;
	public Text text;
	public Image rotatingImage;
	public Text publicText;
	bool loaded;
	float time;
	bool finished;

	void Awake(){
		if(Instance == null ){
			Instance = this;
			DontDestroyOnLoad(this.gameObject);
			if(!loaded){
				TurnOn();
			}
			return;
		}
		GameModeHandler.TurnMeOn();
		}

	void FixedUpdate(){
		if(!finished){
			time = time + Time.fixedDeltaTime;
			if (time>10){
				if(!loaded)
					Loaded();	
				finished = true;		
			}

		}
	}
    public void Loaded(){
		if(!MapsHolder.mapsLoaded){
			StartCoroutine(LoadWhenMapsLoaded());
			return;
		}
    	GameModeHandler.TurnMeOn();
    	image.enabled = false;
    	text.enabled = false;
    	loaded = true;
    	publicText.enabled = false;
    	rotatingImage.enabled = false;
    }

	IEnumerator LoadWhenMapsLoaded(){
		while(!MapsHolder.mapsLoaded){
			yield return null;
		}
		GameModeHandler.TurnMeOn();
    	image.enabled = false;
    	text.enabled = false;
    	loaded = true;
    	publicText.enabled = false;
    	rotatingImage.enabled = false;
	}

    public void TurnOn(){
    	image.enabled = true;
    	text.enabled = true;
    	rotatingImage.enabled = true;
    	publicText.enabled = true;
    }

   	public void DaDebug(string line){
   		if(publicText != null)
   			publicText.text = line;
   	}
}
