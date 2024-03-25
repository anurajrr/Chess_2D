using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{   
   

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    
}
