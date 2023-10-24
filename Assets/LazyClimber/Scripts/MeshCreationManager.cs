using UnityEngine;
using UnityEngine.InputSystem;
using System;

namespace LazyClimber
{
    public class MeshCreationManager : MonoBehaviour
    {

        public static event Action<string> OnBeginDraw, OnEndDraw; 
        
        // Methods to detect user Input
        
        public void BeginDraw(InputAction.CallbackContext ctx)
        {
            //Return if context is not performed. Avoid multiples
            if (!ctx.performed) return;
            
            // Start drawing from input
            var message = "Begin Draw: " + ctx;
            Debug.Log(message);
            OnBeginDraw?.Invoke(message);
        }
    
        public void EndDraw(InputAction.CallbackContext ctx)
        {
            //Return if context is not performed. Avoid multiples
            if (!ctx.performed) return;
            
            // Stop drawing when user releases input
            var message = "End Draw: " + ctx;
            Debug.Log(message);
            OnEndDraw?.Invoke(message);
        }
    }
}
