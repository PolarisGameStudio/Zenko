using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoxPad : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//HideIfClickedOutside(this.gameObject);
        //Debug.Log(RectTransformUtility.RectangleContainsScreenPoint(
               // this.gameObject.GetComponent<RectTransform>(),Input.mousePosition, 
                //Camera.main));
        //Debug.Log(EventSystem.current.IsPointerOverGameObject());
	}
    private void HideIfClickedOutside(GameObject panel) {
    	//Debug.Log( Input.mousePosition);
    	//Debug.Log()
        if (Input.GetMouseButton(0) && panel.activeSelf && 
            /**/ !EventSystem.current.IsPointerOverGameObject()) {
            panel.transform.parent.gameObject.SetActive(false);
        }
    }
}
