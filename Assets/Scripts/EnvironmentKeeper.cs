using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentKeeper : MonoBehaviour
{
	public static EnvironmentKeeper Instance;
	public Transform[] iceBank;
	public Transform[] snowBank;
	public Transform[] treeBank;
    public Transform[] rockBank;
    public Transform[] flowerBank;
    // Start is called before the first frame update
    void Awake()
    {
    	Instance = this;
    	Shuffle();
     //    treeBank = new Transform[transform.GetChild(2).GetChild(1).childCount];

    	// int i = 0;
    	// foreach(Transform child in transform.GetChild(2).GetChild(1)){
    	// 	//child.GetChild(0).gameObject.tag = "Canvas";
    	// 	treeBank[i] = child;
    	// 	i++;
    	// 	}
     //    rockBank = new Transform[transform.GetChild(2).GetChild(0).childCount];

    	// i = 0;
    	// foreach(Transform child in transform.GetChild(2).GetChild(0)){
    	// 	//child.GetChild(0).gameObject.tag = "Canvas";
    	// 	rockBank[i] = child;
    	// 	i++;
    	// }
     //    flowerBank = new Transform[transform.GetChild(2).GetChild(2).childCount];

     //    i = 0;
     //    foreach(Transform child in transform.GetChild(2).GetChild(2)){
     //        //child.GetChild(0).gameObject.tag = "Canvas";
     //        flowerBank[i] = child;
     //        i++;
     //    }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	public void Shuffle() {
		Transform tempGO;
		for (int i = 0; i < rockBank.Length; i++) {
			int rnd = Random.Range(0, rockBank.Length);
			tempGO = rockBank[rnd];
			rockBank[rnd] = rockBank[i];
			rockBank[i] = tempGO;
		}
        
        for (int i = 0; i < treeBank.Length; i++) {
            int rnd = Random.Range(0, treeBank.Length);
            tempGO = treeBank[rnd];
            treeBank[rnd] = treeBank[i];
            treeBank[i] = tempGO;
        }
	}
    public void Reset(){
		Vector3 Origin = new Vector3(-10,-10,0);
		foreach(Transform child in iceBank){
			child.position = Origin;
            child.gameObject.SetActive(false);
		}
		foreach(Transform child in snowBank){
			child.position = Origin;
            child.gameObject.SetActive(false);
		}
		foreach(Transform child in treeBank){
			child.position = Origin;
            child.gameObject.SetActive(false);
		}
        foreach(Transform child in rockBank){
            child.position = Origin;
            child.gameObject.SetActive(false);
        }
    	
    }
}
