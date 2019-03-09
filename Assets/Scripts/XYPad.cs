using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class XYPad : MonoBehaviour
 {
 	public float x;
 	public float y;

    private RectTransform rectTransform
    {
        get
        {
            return transform as RectTransform;
        }
    }
 
    void Update()
    {
        Vector2 localpoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, 
        	GetComponentInParent<Canvas>().worldCamera, out localpoint);
 		
        Vector2 normalizedPoint = Rect.PointToNormalized(rectTransform.rect, localpoint);

 		x=(localpoint.x-rectTransform.rect.x)/rectTransform.rect.width;
 		y=(localpoint.y-rectTransform.rect.y)/rectTransform.rect.height;
        Debug.Log(x + "" + y);

    }
 }
