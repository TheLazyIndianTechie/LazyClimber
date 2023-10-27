using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour
{
    //Variables 
    [SerializeField] private TMP_Text debugDisplayText;
    [SerializeField] private Button playerCallToAction;
    

    private void Awake() => GetComponent<Canvas>().enabled = false;

    //Add listeners for messages
  

    // Event handling methods
    private void HandleWinState(string message) => UpdateDebugDisplay(message); // This will handle incoming win or lose methods
    private void HandleGameOverState(string message) => UpdateDebugDisplay(message); // This will handle incoming win or lose methods
        
    // Display handling methods
    private void UpdateDebugDisplay(string displayMessage)
    {
        debugDisplayText.SetText(displayMessage);
        playerCallToAction.GetComponent<TextMeshProUGUI>().SetText(displayMessage);
    }
}
