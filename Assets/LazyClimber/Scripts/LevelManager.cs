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
            levelTimer -= Time.deltaTime; // Decrement level timer per second
            
            counterDisplay.SetText("{0:1} seconds", levelTimer); // Set a formatted counter display
            
            if (_hasPlayerFinishedLevel) return; // Return if player has finished level

            if (levelTimer <= 0)
            {
                GameManager.Instance.Lose();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            _hasPlayerFinishedLevel = true;
            GameManager.Instance.Win();
        }
        
        
    }
}
