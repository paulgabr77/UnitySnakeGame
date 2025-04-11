using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private SnakeMovement snakeMovement;

    void Start()
    {
        snakeMovement = FindObjectOfType<SnakeMovement>();
    }

    void Update()
    {
        if (scoreText != null && snakeMovement != null)
        {
            scoreText.text = $"Scor: {snakeMovement.GetScore()}";
        }
    }
} 