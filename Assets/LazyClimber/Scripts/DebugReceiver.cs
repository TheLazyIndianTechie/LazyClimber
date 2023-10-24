using UnityEngine;
using TMPro;

namespace LazyClimber
{
    public class DebugReceiver : MonoBehaviour
    {
        //Variables 
        [SerializeField] private TMP_Text debugDisplayText;
        
        //Add listeners for messages
        private void OnEnable()
        {
            ButtonTester.OnButtonClicked += HandleButtonTest;
            MeshCreationManager.OnBeginDraw += HandleBeginDraw;
            MeshCreationManager.OnEndDraw += HandleEndDraw;
        }

        //Remove listeners
        private void OnDisable()
        {
            ButtonTester.OnButtonClicked -= HandleButtonTest;
            MeshCreationManager.OnBeginDraw -= HandleBeginDraw;
            MeshCreationManager.OnEndDraw -= HandleEndDraw;
        }

        // Event handling methods
        private void HandleButtonTest(string message) => UpdateDebugDisplay(message);
        private void HandleBeginDraw(string message) => UpdateDebugDisplay(message);
        private void HandleEndDraw(string message) => UpdateDebugDisplay(message);
        
        // Display handling methods
        private void UpdateDebugDisplay(string displayMessage) => debugDisplayText.SetText(displayMessage);
        
        
    }
}
