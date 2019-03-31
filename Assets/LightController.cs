using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {
	public static Vector3 lightorigin;
	public static float openingorigin;
	public static Light light;
	// Use this for initialization
	void Awake () {
		lightorigin = GameObject.Find("SpotLight").transform.position;
		light = GameObject.Find("SpotLight").GetComponent<Light>();
		openingorigin = light.spotAngle;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public static void setLight(int shiftx, int shifty){
		/*if(LevelBuilder.totaldimension == 8){
			light.transform.position = lightorigin;
			light.spotAngle = openingorigin;
		}
		if(LevelBuilder.totaldimension == 10){
			//light.transform.position = light.transform.position + new Vector3(shiftx,0,-shifty);
			//GameObject.Find("SpotLight").transform.position
			light.transform.position = new Vector3(lightorigin.x +shiftx,lightorigin.y,lightorigin.z-shifty);
			light.spotAngle = openingorigin+6;
		}*/
	}
}
