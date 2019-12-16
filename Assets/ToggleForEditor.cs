using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToggleForEditor : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        #if UNITY_EDITOR
    		this.transform.GetComponent<Image>().enabled = true;
    	#else
    		this.gameObject.SetActive(false);
    	#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
