using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CordinatesToggle : MonoBehaviour
{
    [SerializeField] private GameObject[] rows;

    private bool isToggled = false; // Track the toggle state

    void Start()
    {
        // Deactivate all objects in the array when the game starts
        foreach (var row in rows)
        {
            row.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            // Toggle the state
            isToggled = !isToggled;

            // Loop through each row
            foreach (var row in rows)
            {
                // Set the active state based on the toggle state
                row.SetActive(isToggled);
            }

            // Log the current state
            Debug.Log(isToggled ? "ON" : "OFF");
        }
    }
}
