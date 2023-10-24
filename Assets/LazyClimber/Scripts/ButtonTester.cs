using System;
using UnityEngine;
using UnityEngine.UI;

namespace LazyClimber
{
    public class ButtonTester : MonoBehaviour
    {
        //Variables
        private Button _button;

        //Events 
        public static event Action<string> OnButtonClicked; 

        //Lifecycle methods
        private void Awake() => _button = GetComponent<Button>();

        private void Start()
        {
            if(_button != null) _button.onClick.AddListener(HandleButtonClick);
        }

        //Methods
        private void HandleButtonClick()
        {
            var message = name + " was clicked!";
            Debug.Log(message);
            OnButtonClicked?.Invoke(message);
        }
    }
}
