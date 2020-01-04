using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileKeeper : MonoBehaviour
{
	public static TileKeeper Instance;



	public Transform[] iceBank;
	public Transform[] wallBank;
	public Transform[] snowBank;
	public Transform[] wallOnly;
	public Transform[] fragileBank;
	public Transform[] holeBank;
	public Transform[] flowerBank;
	public Transform zenko;
	public Transform zenkoStatue;
	public Transform goal;
    // Start is called before the first frame update
    void Awake()
    {
    	Instance = this;
    	Shuffle();
    	
    	flowerBank = new Transform[transform.GetChild(6).childCount];

    	int i = 0;
    	foreach(Transform child in transform.GetChild(6)){
    		//child.GetChild(0).gameObject.tag = "Canvas";
    		flowerBank[i] = child;
    		i++;
    	}


    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Shuffle() {
		Transform tempGO;
		for (int i = 0; i < wallBank.Length; i++) {
			int rnd = Random.Range(0, wallBank.Length);
			tempGO = wallBank[rnd];
			wallBank[rnd] = wallBank[i];
			wallBank[i] = tempGO;
		}
	}

	public void Reset(){
		Vector3 Origin = new Vector3(-10,-10,0);
		foreach(Transform child in iceBank){
			child.position = Origin;
			child.gameObject.SetActive(false);
		}
		Origin = new Vector3(-15,0,0);
		foreach(Transform child in wallBank){
			child.position = Origin;
		}
		foreach(Transform child in snowBank){
			child.position = Origin;
		}
		foreach(Transform child in wallOnly){
			child.position = Origin;
		}
		foreach(Transform child in fragileBank){
			child.position = Origin;
			child.gameObject.SetActive(false);
		}
		foreach(Transform child in holeBank){
			child.position = Origin;
			child.gameObject.SetActive(false);
		}
		foreach(Transform child in flowerBank){
			child.position = Origin;
		}		
//		zenko.gameObject.SetActive(false);
		goal.gameObject.SetActive(false);
		zenkoStatue.GetChild(0).gameObject.SetActive(false);
	}
}
