using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompleteProject;

public class RestoreButton : MonoBehaviour
{
    void OnEnable()
    {
        #if UNITY_ANDROID
        this.gameObject.SetActive(false);
        #endif
    }
    public void Restore(){
        Purchaser.Instance.Restore();
    }

}
