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

	bool loaded;

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

    public static void Loaded(){
    	if(Instance == null)
    	Instance = GameObject.Find("Handler").GetComponent<Loading>();
    	GameModeHandler.TurnMeOn();
    	Instance.image.enabled = false;
    	Instance.text.enabled = false;
    	Instance.loaded = true;
    }

    private void TurnOn(){
    	Debug.Log("turnt");
    	image.enabled = true;
    	text.enabled = true;

    }
}
