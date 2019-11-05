using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
	Transform mylead;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(LevelBuilder.playertransform!=mylead){
        	mylead = LevelBuilder.playertransform;
        }
        else{
			// this.transform.position.x = mylead.position.x;
			// this.transform.position.z = mylead.position.z;
			this.transform.position = new Vector3(mylead.position.x, transform.position.y, mylead.position.z);
        }
    }
}
