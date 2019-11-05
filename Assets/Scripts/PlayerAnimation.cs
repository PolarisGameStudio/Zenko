using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // Start is called before the first frame update
	public Material dissolveMat;
	public float lowValue = -1;
	public float highValue = 1;


    void Start()
    {
    	dissolveMat.SetFloat("Vector1_B5CA3B27", highValue);
    	StartCoroutine(Appear(.5f));

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I)){
			StartCoroutine(Appear(1));
        }   
        if(Input.GetKeyDown(KeyCode.O)){
			StartCoroutine(Disappear(1));
        }      
    }
    public IEnumerator Disappear(float fadetime){


		for(float t = 0.0f; t<fadetime; t+= Time.deltaTime){
			float normalizedTime = t/fadetime;
//			Debug.Log(Mathf.Lerp(lowValue,highValue,normalizedTime));//kick
			dissolveMat.SetFloat("Vector1_B5CA3B27", Mathf.Lerp(lowValue,highValue,normalizedTime));
			yield return null;
		}
//			Debug.Log(highValue);
			dissolveMat.SetFloat("Vector1_B5CA3B27", highValue);


    }
    public IEnumerator Appear(float fadetime){
		for(float t = 0.0f; t<fadetime; t+= Time.deltaTime){
			float normalizedTime = t/fadetime;
//			Debug.Log(Mathf.Lerp(highValue,lowValue,normalizedTime));//kick
			dissolveMat.SetFloat("Vector1_B5CA3B27", Mathf.Lerp(highValue,lowValue,normalizedTime));
			yield return null;
		}
			//Debug.Log(lowValue);
			dissolveMat.SetFloat("Vector1_B5CA3B27", lowValue);

    }
}
