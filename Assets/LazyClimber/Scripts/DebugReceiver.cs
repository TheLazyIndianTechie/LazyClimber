using UnityEngine;
using TMPro;

namespace LazyClimber
{
    public class DebugReceiver : MonoBehaviour
    {
        //Variables 
        [SerializeField] private TMP_Text debugDisplayText;
        
        //Add listener for messages
        private void OnEnable() => ButtonTester.OnButtonClicked += HandleButtonTest;
        
        //Remove listener
        private void OnDisable() => ButtonTester.OnButtonClicked -= HandleButtonTest;
        
        // Debug display and event handling methods
        private void HandleButtonTest(string message) => UpdateDebugDisplay(message);
        private void UpdateDebugDisplay(string displayMessage) => debugDisplayText.SetText(displayMessage);
        
    }
}
