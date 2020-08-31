using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverer : MonoBehaviour {
	public Color startcolor;
	private MeshRenderer renderer;
	public bool canplace;

	void Start () {
		renderer = GetComponent<MeshRenderer>();
		canplace = true;
	}
	
	public void Enter(){
		if(LevelManager.isdragging){
			if(LevelBuilder.tiles[(int)transform.position.x, -(int)transform.position.z].isTaken){
			    PlaneBehavior.tilex = (int)transform.position.x;
			    PlaneBehavior.tiley = (int)transform.position.z;
			}
			else{
			    PlaneBehavior.tilex = (int)transform.position.x;
			    PlaneBehavior.tiley = (int)transform.position.z;
			    PlaneBehavior.readyToDrop = true;
			}	
		}	
	}

	public void Leave(){
		if(LevelBuilder.tiles[(int)transform.position.x, -(int)transform.position.z].isTaken){
		    PlaneBehavior.readyToDrop = false;
		}
		else{
		    PlaneBehavior.readyToDrop = false;
		}
	}
}
