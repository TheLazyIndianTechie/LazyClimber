using System;
using UnityEngine;

namespace LazyClimber
{
    public class ButtonTester : MonoBehaviour
    {
        //Events 
        public static event Action<string> OnButtonClicked; 

        //Methods
        private void HandleButtonClick()
        {
            var message = name + " was clicked!";
            OnButtonClicked?.Invoke(message);
        }
    }
}
