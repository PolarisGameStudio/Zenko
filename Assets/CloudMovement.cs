using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
	private Vector3 normalizeDirection1;
	private Vector3 normalizeDirection2;
	public Transform clouds1;
	public Transform clouds2;
	public float speed;

    // Start is called before the first frame update
    void Start()
    {
     	normalizeDirection1 = new Vector3(0,0,10);   
     	normalizeDirection2 = new Vector3(0,0,10);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveClouds(clouds1);
        MoveClouds(clouds2);
    }
    public void MoveClouds(Transform thiscloud){
    	thiscloud.position += normalizeDirection1*speed*Time.deltaTime;
    	if(thiscloud.position.z>10){
    		thiscloud.position = new Vector3(thiscloud.position.x, thiscloud.position.y, -9);
    	}
    }
}
