using UnityEngine;
using UnityEngine.InputSystem;
using System;

namespace LazyClimber
{
    public class MeshCreationManager : MonoBehaviour
    {
        //Public events for emitting messages to listeners 
        public static event Action<string> OnBeginDraw, OnEndDraw; 
        
        // Variables
        public Camera mainCamera;
        
        // Lifecycle methods
        
        // Grabs the main camera from assigned active player input.
        private void Start() => mainCamera = GetComponent<PlayerInput>().camera;

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
