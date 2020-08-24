using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingTextAnimator : MonoBehaviour
{
	int counter;
	int divider;
	int classifier;
	int whole;
	public Text text;
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        divider = 76;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        counter++;
//        Debug.Log(counter +  " " + counter%divider);
        classifier = counter%divider;
        whole = counter/divider;
        if(whole%2 == 0){
	        switch(classifier){
	        	case 25:
	        		text.text = "Loading .";
	        		break;
	        	case 50:
	        		text.text = "Loading ..";
	        		break;
	        	case 75:
	        		text.text = "Loading ...";
	        		break;
	        }       	
        }
        else{
        	switch(classifier){
	        	case 25:
	        		text.text = "Loading ..";
	        		break;
	        	case 50:
	        		text.text = "Loading .";
	        		break;
	        	case 75:
	        		text.text = "Loading ";
	        		break;

	        }    
        }
    }
    void ModulateText(){

    }
}
