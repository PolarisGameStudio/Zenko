using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLeft : MonoBehaviour
{
	public float magnitude;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = transform.position + (Vector3.left*magnitude);
    }
}
