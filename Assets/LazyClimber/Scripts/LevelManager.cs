using TMPro;
using UnityEngine;

namespace LazyClimber
{
    public class LevelManager : MonoBehaviour
    {
        // Variables
        [SerializeField] private float levelTimer = 60f;
        private bool _hasPlayerFinishedLevel = false;
        [SerializeField] private TMP_Text counterDisplay;
        
        private void Update()
        {
            
            if (_hasPlayerFinishedLevel) return; // Return if player has finished level
            
            levelTimer -= Time.deltaTime; // Decrement level timer per second
            
            counterDisplay.SetText("{0:1} seconds", levelTimer); // Set a formatted counter display
            
            
            if (levelTimer <= 0)
            {
                levelTimer = 0.0f; // Stop the timer
                counterDisplay.SetText("Time Up"); // Set a time out message
                GameManager.Instance.Lose(); // Call lose game from Game Manager
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            _hasPlayerFinishedLevel = true;
            levelTimer = 0.0f; // Stop the timer
            counterDisplay.SetText(""); // Clear message
            GameManager.Instance.Win();
        }
        
        
    }
}
