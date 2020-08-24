using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingLogo : MonoBehaviour
{
	float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time = time + Time.deltaTime;
        this.transform.eulerAngles = new Vector3(0,0,time*45);
    }
}
