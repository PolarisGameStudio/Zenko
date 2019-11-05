using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotator : MonoBehaviour
{
	private Sprite mysprite;
    public RectTransform rt;
	float rotation;
	float timer;
    // Start is called before the first frame update
    void Start()
    {
    	//mysprite = this.GetComponent<Image>().sprite;
    	//Debug.Log(mysprite.bounds.size.x);
        rotation = 0;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
		rt.eulerAngles = new Vector3(0,rotation + oscillate(timer,3, 12), 0);        
    }
    float oscillate(float time, float speed, float scale)
    {
        return Mathf.Sin(time * speed / Mathf.PI) * scale;
    }
}
