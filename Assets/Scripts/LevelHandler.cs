/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler: MonoBehaviour {


	// Use this for initialization
	void Start () {
		//CreateBase();
		//AddTiles(levelidtoload);
	}
	void Update () {
		
	}
	void CreateBase(){
		int numCopies = rows*cols;
		for (int i =0; i<numCopies;i++){
			for(int j=0; j < tile.Length; j++){
				GameObject o = (GameObject) Instantiate(tile[j],new Vector3(-10,-10,0), tile[j].transform.rotation);
				o.SetActive(false);
				tileBank.Add(o);
			}
		}	

		for (int r= 0; r< rows; r++){
			for (int c = 0; c<cols; c++){
				Vector3 tilePos = new Vector3(c,0,r);
				for (int n =0; n< tileBank.Count; n++){
					GameObject o = tileBank[n];
					if(!o.activeSelf){
						o.transform.position = new Vector3(tilePos.x,tilePos.y,tilePos.z);
						o.SetActive(true);
						tiles[c,r] = new Tile(o, o.name, false	);
						n = tileBank.Count + 1;
					}
				}
			}
		}
		
	}
	void AddTiles(int levelid){
	}
}
*/