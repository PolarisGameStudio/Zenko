using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    //La foto se guardara en Assets/StreamingAssets
    #if UNITY_EDITOR
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)){
            ScreenCapture.CaptureScreenshot(FilePath());
        }
        
    }
    string FilePath(){
        string fileName = "a." + Screen.width + "x" + Screen.height+ " " + System.DateTime.Now.ToString("yyyy-MM-dd") + ".png";
        Debug.Log(fileName);
        return System.IO.Path.Combine(Application.streamingAssetsPath,fileName);
    }
    #endif
}
