using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class ScoreManager : MonoBehaviour
{
    public int score = 0; // Current score
    private TMP_Text scoreText; // Reference to the UI Text component.

    private AudioManager audio;

    void Start()
    {
        // Find the Text component and assign it to scoreText.
        scoreText = GetComponent<TMP_Text>();
        UpdateScoreText();

        audio = FindObjectOfType<AudioManager>();
    }

    public void IncrementScore(int points)
    {
        score += points;
        UpdateScoreText();
        audio.audioSrc.Play();
    }

    void UpdateScoreText()
    {
        // Update the UI Text component with the current score.
        scoreText.text = "Score: " + score;
    }

}
