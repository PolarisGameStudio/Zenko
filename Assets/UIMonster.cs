using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMonster : MonoBehaviour {
	public GameObject mypiece;
	public string mytype;
	public PieceHolders pieceHolder;
	// Use this for initialization
	void Start () {
		pieceHolder = GameObject.Find("PieceHolders").GetComponent<PieceHolders>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Spawnit(){
		Instantiate(mypiece, new Vector3(0, 0, 0), Quaternion.identity);

	}
	public void OnPointerDown(){
		PlaneBehavior.readyToDrop = false;
		if(pieceHolder.isAvailable(mytype)){
			pieceHolder.updateValueDown(mytype);
			Color tempColor = GetComponent<Image>().color;
			tempColor.a = .5f;
			GetComponent<Image>().color = tempColor;

			Debug.Log("Moused");
			Vector3 spawnposition = new Vector3(PlaneBehavior.planePos.x, 0, PlaneBehavior.planePos.z);
			if(mytype == "Wall"){
				GameObject mynewpiece = Instantiate(mypiece, spawnposition, Quaternion.identity);
				mynewpiece.GetComponent<Dragger>().OnMouseDown();
				mynewpiece.GetComponent<Dragger>().startedDragging = true;
			}
			else if(mytype == "Down"){
				GameObject mynewpiece = Instantiate(mypiece, spawnposition, Quaternion.Euler(new Vector3(0,180,0)));
				mynewpiece.GetComponent<Dragger>().OnMouseDown();
				mynewpiece.GetComponent<Dragger>().startedDragging = true;
			}
			else if(mytype == "Up"){
				GameObject mynewpiece = Instantiate(mypiece, spawnposition, Quaternion.Euler(new Vector3(0,0,0)));
				mynewpiece.GetComponent<Dragger>().OnMouseDown();
				mynewpiece.GetComponent<Dragger>().startedDragging = true;
			}		
			else if(mytype == "Left"){
				GameObject mynewpiece = Instantiate(mypiece, spawnposition, Quaternion.Euler(new Vector3(0,270,0)));
				mynewpiece.GetComponent<Dragger>().OnMouseDown();
				mynewpiece.GetComponent<Dragger>().startedDragging = true;
			}		
			else if(mytype == "Right"){
				GameObject mynewpiece = Instantiate(mypiece, spawnposition, Quaternion.Euler(new Vector3(0,90,0)));
				mynewpiece.GetComponent<Dragger>().OnMouseDown();
				mynewpiece.GetComponent<Dragger>().startedDragging = true;	 	
			}
		}
		


	}
	public void OnPointerUp(){

	}
}
