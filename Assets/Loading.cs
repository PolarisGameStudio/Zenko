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

	void Awake(){
		if(Instance == null){
			Instance = this;
			DontDestroyOnLoad(this.gameObject);
			TurnOn();
			
			return;
		}

		GameModeHandler.TurnMeOn();

		Destroy(this.gameObject);

		}

    public static void Loaded(){
    	Instance.gameScript.enabled = true;
    	Instance.image.enabled = false;
    	Instance.text.enabled = false;
    }

    private void TurnOn(){
    	Debug.Log("turnt");
    	image.enabled = true;
    	text.enabled = true;
    }
}
