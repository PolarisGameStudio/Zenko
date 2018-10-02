using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragileProperties : MonoBehaviour {
	public Material mymat;
	public Color mypink;
	public bool readyToLava;
	public bool lavaWhenReady;
	public Transform playert;
	public Color myred;
	public GameObject myhole;
	//static GameObject hole;
	public GameObject myfragile;

	// Use this for initialization
	void Start () {
		//hole = myhole;
		//mypink = GetComponentInChildren<MeshRenderer>().material.color;
	}
	
	// Update is called once per frame
	void Update () {
		if(readyToLava){
			Debug.Log(playert.position);
			if(Vector3.Distance(playert.position, transform.position) < .9){
				lavaWhenReady = true;
				readyToLava = false;
			}
		}
		if(lavaWhenReady){
			if(Vector3.Distance(playert.position, transform.position) < .1){
				//GetComponentInChildren<MeshRenderer>().material.color = myred;
				myhole.SetActive(true);
				myfragile.SetActive(false);
				lavaWhenReady = false;
						//tilerenderer.material.color = fragilered;

			}
		}
	}
}
