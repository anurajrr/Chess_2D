using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class Cordinate_Name : MonoBehaviour
{  
    public TextMeshProUGUI gameNameText;


    private void Update() 
    {
        if(!Application.isPlaying)
        {
            DisplayCordinates();
        }
    }

    private void DisplayCordinates()
    {
        // Get the text from the TextMeshPro component
        string gameName = gameNameText.text;

        // Set the GameObject's name to match the text
        gameObject.name = gameName.ToString();
    }
}
