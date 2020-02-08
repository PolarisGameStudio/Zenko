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

			//GameModeHandler.TurnMeOn();

			return;
		}

		GameModeHandler.TurnMeOn();

		//Destroy(this.gameObject);

		}

	void FixedUpdate(){
		if(!finished){
			time = time + Time.fixedDeltaTime;
			//Debug.Log(time);
			if (time>10){
				if(!loaded)
					Loaded();	
				finished = true;		
			}

		}

	}
	// void Update(){
	// 	//if()
	// }

    public void Loaded(){
    	// if(Instance == null)
    	// Instance = GameObject.Find("Handler").GetComponent<Loading>();
    	GameModeHandler.TurnMeOn();
    	image.enabled = false;
    	text.enabled = false;
    	loaded = true;
    	publicText.enabled = false;
    	rotatingImage.enabled = false;
    }

    public void TurnOn(){
    	Debug.Log("turnt");
    	image.enabled = true;
    	text.enabled = true;
    	rotatingImage.enabled = true;
    	Debug.Log(rotatingImage);
    	Debug.Log(publicText);
    	publicText.enabled = true;
    }

   	public void DaDebug(string line){
   		if(publicText != null)
   			publicText.text = line;
   	}
}
