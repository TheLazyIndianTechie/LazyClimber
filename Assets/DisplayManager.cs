using System;
using LazyClimber;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour
{
    //Variables 
    [SerializeField] private TMP_Text gameStateDisplayText;
    private Canvas _displayCanvas;

    private void Awake()
    {
        _displayCanvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        if (_displayCanvas != null) _displayCanvas.enabled = false;
    }

    //Add listeners for messages
    private void OnEnable()
    {
        // Sub to events
        GameManager.OnGameOver += HandleGameOverState;
        GameManager.OnPlayerWin += HandleWinState;
    }

    private void OnDisable()
    {
        // Unsub from events
        GameManager.OnGameOver -= UpdateGameStateDisplay;
        GameManager.OnPlayerWin -= UpdateGameStateDisplay;
    }

    // Event handling methods
    private void HandleWinState(string message) => UpdateGameStateDisplay(message); // This will handle incoming win 
    private void HandleGameOverState(string message) => UpdateGameStateDisplay(message); // This will handle incoming lose
        
    // Display handling methods
    private void UpdateGameStateDisplay(string displayMessage)
    {
        if (_displayCanvas != null) _displayCanvas.enabled = true;
        gameStateDisplayText.SetText(displayMessage);
        
    }
}
