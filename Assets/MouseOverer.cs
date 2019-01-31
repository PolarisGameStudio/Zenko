using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverer : MonoBehaviour {
	private Color startcolor;
	private MeshRenderer renderer;
	public bool canplace;

	// Use this for initialization
	void Start () {
		if(this.gameObject.tag == "Fragile"){
			renderer = this.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>();
		}
		else{
			renderer = GetComponent<MeshRenderer>();
		}
		canplace = true;
		startcolor = renderer.material.color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	 void OnMouseEnter()
	{
		if(LevelManager.isdragging){
			if(LevelBuilder.tiles[(int)transform.position.x, -(int)transform.position.z].isTaken){
				startcolor = renderer.material.color;
			    renderer.material.color = Color.red;

			}
			else{
			    startcolor = renderer.material.color;
			    renderer.material.color = Color.yellow;
			    PlaneBehavior.tilex = (int)transform.position.x;
			    PlaneBehavior.tiley = (int)transform.position.z;
			    PlaneBehavior.readyToDrop = true;
			}	
		}
		
	   // Debug.Log("Enter");
	}
	void OnMouseExit()
	{
		if(LevelBuilder.tiles[(int)transform.position.x, -(int)transform.position.z].isTaken){
		    renderer.material.color = startcolor;
		    PlaneBehavior.readyToDrop = false;

		}
		else{
		    renderer.material.color = startcolor;
		    PlaneBehavior.readyToDrop = false;
		}
	    //PlaneBehavior
	    //Debug.Log("Exit");
	}
}
