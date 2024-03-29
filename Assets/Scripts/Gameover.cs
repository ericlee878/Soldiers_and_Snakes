using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gameover : MonoBehaviour
{
    // Accesses Soldier 
    private Soldier soldier;

    // Reference to the Game Over canvas
    private TMP_Text gameOverCanvas;




    // Start is called before the first frame update
    void Start()
    {
        // Soldier accessed
        soldier = FindObjectOfType<Soldier>();

        // Find the Text component and assign it to gameOverCanvas.
        gameOverCanvas = GetComponent<TMP_Text>();

        // Ensure the Game Over canvas is initially disabled
        gameOverCanvas.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is dead
        if (soldier.isDead)
        {
            // If the player is dead, show the Game Over canvas
            gameOverCanvas.text = "Game Over";
        }
        else
        {
            // If the player is not dead, hide the Game Over canvas
            gameOverCanvas.text = "";
        }
    }
}
