using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScoreHandler : MonoBehaviour
{
    [Header("Game Manager Reference")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        gameManager = GameManager.Instance;

        scoreText = GetComponent<TextMeshProUGUI>();

        if (gameManager != null)
        {
            gameManager.OnScoreChangeEvent += OnScoreChangedEventHandler;
            OnScoreChangedEventHandler(gameManager.Score);
        }
    }

    private void OnDestroy()
    {
        if (gameManager != null)
        {
            gameManager.OnScoreChangeEvent += OnScoreChangedEventHandler;
        }

    }

    private void OnScoreChangedEventHandler(int _newScore)
    {
        scoreText.text = $"Score: {_newScore}";
    }
}
