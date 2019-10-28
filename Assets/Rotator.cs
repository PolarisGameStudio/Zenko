using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotator : MonoBehaviour
{
	private Sprite mysprite;
	float rotation;
	float timer;
    // Start is called before the first frame update
    void Awake()
    {
    	mysprite = this.GetComponent<Image>().sprite;
    	Debug.Log(mysprite.bounds.size.x);
        rotation = 0;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
		this.GetComponent<RectTransform>().sizeDelta = new Vector2 (rotation + oscillate(timer,5, 1), 719);        
    }
    float oscillate(float time, float speed, float scale)
    {
        return Mathf.Sin(time * speed / Mathf.PI) * scale;
    }
}
