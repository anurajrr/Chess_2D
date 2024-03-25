using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenshotscript : MonoBehaviour
{
    private void Update() 
    {
        // if(Input.GetMouseButton(0))
        if(Input.GetKey(KeyCode.W))
        {   
            Debug.Log("Screenshot taken");
            ScreenCapture.CaptureScreenshot("Screenshot-"+ DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")+ ".png",4);
        }    
    }
}
