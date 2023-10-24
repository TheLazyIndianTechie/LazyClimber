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
            // Start drawing from input
            var message = "Begin Draw: " + ctx;
            Debug.Log(message);
            OnBeginDraw?.Invoke(message);
        }
    
        public void EndDraw(InputAction.CallbackContext ctx)
        {
            // Stop drawing when user releases input
            Debug.Log(ctx.ToString());
            var message = "End Draw: " + ctx;
            Debug.Log(message);
            OnEndDraw?.Invoke(message);
        }
    }
}
