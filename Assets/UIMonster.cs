using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMonster : MonoBehaviour {
	public GameObject mypiece;
	public string mytype;
	public PieceHolders pieceHolder;
	public List<GameObject> piecesSpawned;
	public PlaneBehavior planeBehavior;
	// Use this for initialization
	void Start () {
		pieceHolder = GameObject.Find("PieceHolders").GetComponent<PieceHolders>();
		planeBehavior = GameObject.Find("Drag_Plane").GetComponent<PlaneBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public GameObject Spawnit(){
		GameObject spawned = Instantiate(mypiece, new Vector3(0, 0, 0), Quaternion.identity);
		RotateIcarus(spawned);
		spawned.transform.GetChild(1).gameObject.SetActive(false);
		piecesSpawned.Add(spawned);
		return spawned;
	}
	public void RotateIcarus(GameObject Icarus){
		if(mytype == "Left"){
			Icarus.transform.eulerAngles =  new Vector3(0f,270f,0f);
		}
		if(mytype == "Right"){
			Icarus.transform.eulerAngles = new Vector3(0f,90f,0f);
		}
		if(mytype == "Down"){
			Icarus.transform.eulerAngles = new Vector3(0f,180f,0f);
		}
		if(mytype == "LeftSeed"){
			Icarus.transform.eulerAngles =  new Vector3(0f,270f,0f);
		}
		if(mytype == "RightSeed"){
			Icarus.transform.eulerAngles = new Vector3(0f,90f,0f);
		}
		if(mytype == "DownSeed"){
			Icarus.transform.eulerAngles = new Vector3(0f,180f,0f);
		}
	}
	public void OnPointerDown(){
		PlaneBehavior.readyToDrop = false;
		if(pieceHolder.isAvailable(mytype) && TurnBehaviour.turn == 0){
			pieceHolder.updateValueDown(mytype);
			Color tempColor = GetComponent<Image>().color;
			tempColor.a = .5f;
			GetComponent<Image>().color = tempColor;

			Debug.Log("Moused");
			Vector3 spawnposition = planeBehavior.forcePlanePos();
			Debug.Log("SPAWNED IT AT " + spawnposition);
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
			else if(mytype == "WallSeed"){
				GameObject mynewpiece = Instantiate(mypiece, spawnposition, Quaternion.Euler(new Vector3(0,0,0)));
				mynewpiece.GetComponent<Dragger>().OnMouseDown();
				mynewpiece.GetComponent<Dragger>().startedDragging = true;	 	
			}
			else if(mytype == "LeftSeed"){
				GameObject mynewpiece = Instantiate(mypiece, spawnposition, Quaternion.Euler(new Vector3(0,270,0)));
				mynewpiece.GetComponent<Dragger>().OnMouseDown();
				mynewpiece.GetComponent<Dragger>().startedDragging = true;	 	
			}
			else if(mytype == "RightSeed"){
				GameObject mynewpiece = Instantiate(mypiece, spawnposition, Quaternion.Euler(new Vector3(0,90,0)));
				mynewpiece.GetComponent<Dragger>().OnMouseDown();
				mynewpiece.GetComponent<Dragger>().startedDragging = true;	 	
			}
			else if(mytype == "UpSeed"){
				GameObject mynewpiece = Instantiate(mypiece, spawnposition, Quaternion.Euler(new Vector3(0,0,0)));
				mynewpiece.GetComponent<Dragger>().OnMouseDown();
				mynewpiece.GetComponent<Dragger>().startedDragging = true;	 	
			}
			else if(mytype == "DownSeed"){
				GameObject mynewpiece = Instantiate(mypiece, spawnposition, Quaternion.Euler(new Vector3(0,180,0)));
				mynewpiece.GetComponent<Dragger>().OnMouseDown();
				mynewpiece.GetComponent<Dragger>().startedDragging = true;	 	
			}
		}
		


	}
	public void OnPointerUp(){

	}
}
