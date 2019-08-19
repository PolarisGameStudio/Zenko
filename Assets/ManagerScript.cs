using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ManagerScript : MonoBehaviour {
	
	public static ManagerScript Instance { get; private set; }
	public static int Counter { get; private set; }

	void Start(){
		Instance = this;
	}

	public void IncrementCounter(){
		Counter++;
	}

	public void RestartGame(){
		//PlayServices.AddScoreToLeaderboard()
	}
}