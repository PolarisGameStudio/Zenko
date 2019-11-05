using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragileBehaviour : MonoBehaviour {
	public bool readytolava;
	public GameObject player;
	// Use this for initialization
	void Start () {
		readytolava = false;
	}
	
	// Update is called once per frame
	void Update () {
//			float distance = Vector3.Distance(player.transform.position , this.transform.position);

//		Debug.Log(distance);
		if(readytolava){

			float distance = Vector3.Distance(player.transform.position , this.transform.parent.transform.position);
//			Debug.Log(distance);
			if(distance<0.98){
				GetComponent<Animator>().SetInteger("Phase",1);
				readytolava = false;
				SfxHandler.Instance.PlayFragile();
			}
		}
	}
}
