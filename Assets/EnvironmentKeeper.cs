using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentKeeper : MonoBehaviour
{
	public static EnvironmentKeeper Instance;
	public Transform[] iceBank;
	public Transform[] snowBank;
	public Transform[] environmentBank;
    // Start is called before the first frame update
    void Awake()
    {
    	Instance = this;
    	Shuffle();
     //    environmentBank = new Transform[transform.GetChild(2).childCount];

    	// int i = 0;
    	// foreach(Transform child in transform.GetChild(2)){
    	// 	//child.GetChild(0).gameObject.tag = "Canvas";
    	// 	environmentBank[i] = child;
    	// 	i++;
    	// 	}
       // snowBank = new Transform[transform.GetChild(1).childCount];

    	// i = 0;
    	// foreach(Transform child in transform.GetChild(1)){
    	// 	//child.GetChild(0).gameObject.tag = "Canvas";
    	// 	snowBank[i] = child;
    	// 	i++;
    	// }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	public void Shuffle() {
		Transform tempGO;
		for (int i = 0; i < environmentBank.Length; i++) {
			int rnd = Random.Range(0, environmentBank.Length);
			tempGO = environmentBank[rnd];
			environmentBank[rnd] = environmentBank[i];
			environmentBank[i] = tempGO;
		}
	}
    public void Reset(){
		Vector3 Origin = new Vector3(-10,-10,0);
		foreach(Transform child in iceBank){
			child.position = Origin;
		}
		foreach(Transform child in snowBank){
			child.position = Origin;
		}
		foreach(Transform child in environmentBank){
			child.position = Origin;
		}
	
    }
}
