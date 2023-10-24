using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;
using UnityEngine.Serialization;

namespace LazyClimber
{
    public class MeshCreationManager : MonoBehaviour
    {
        //Public events for emitting messages to listeners 
        public static event Action<string> OnBeginDraw, OnEndDraw; 
        
        // Variables
        [SerializeField] private Camera mainCamera;
        
        // Lifecycle methods
        
        // Grabs the main camera from assigned active player input.
        // TODO: Need a better way of handling the nulls if Camera is not assigned. Maybe do a camera.main but might be expensive. Fix after assignment
        private void Start() => mainCamera = GetComponent<PlayerInput>().camera;

        // Methods to detect user Input
        
        public void BeginDraw(InputAction.CallbackContext ctx)
        {
            //Return if context is not performed. Avoid multiples
            if (!ctx.performed) return;
            
            // Calling DrawMesh coroutine
            StartCoroutine(DrawMesh());
            
            // Start drawing from input
            var message = "Begin Draw: " + ctx;
            Debug.Log(message);
            OnBeginDraw?.Invoke(message);
        }
    
        public void EndDraw(InputAction.CallbackContext ctx)
        {
            //Return if context is not performed. Avoid multiples
            if (!ctx.performed) return;
            
            // Killing off all coroutines
            StopAllCoroutines();
            
            // Stop drawing when user releases input
            var message = "End Draw: " + ctx;
            Debug.Log(message);
            OnEndDraw?.Invoke(message);
        }

        private IEnumerator DrawMesh()
        {
            // Create a GameObject and assign it
            var drawing = new GameObject("DrawnMesh");
            
            // Adding a MeshFilter and a MeshRenderer to the drawnMesh go.
            drawing.AddComponent<MeshFilter>(); // Defines the 3d geometry and shape of the object (verts, etc)
            drawing.AddComponent<MeshRenderer>(); // Handles the rendering of the object (Renders the object with mat, etc)
            
            // Create the mesh
            
            var mesh = new Mesh(); // Initialize an actual mesh
            var vertices = new Vector3[8]; // Define the vertices array (8 points form the cube array)
            
            Vector3 startPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)); // Set Z as 10 because camera is -10 from origin.
            Vector3 temp = new Vector3(startPosition.x, startPosition.y, 0.5f); // Hav all drawings start from a single point.
            
            // Construction of triangles array
            // Cube is 6 faces and each face is 2 triangles. so 12 triangles! 
            // One triangle is 3 verts so total of 36 verts.
            // TODO: Research more on Winding - Unity handles winding in clock-wise for front-facing and anti-clockwise for back facing?

            for (var i = 0; i < vertices.Length; i++) vertices[i] = temp; // For loop instantiates all 8 points at the temp pos.

            var triangles = new int[36]; // creating a triangle array of size 36 as needed
            
            // Construct the faces
            //TODO: Vinay to Read more Reference: https://ilkinulas.github.io/development/unity/2016/04/30/cube-mesh-in-unity3d.html
            
            // Front face
            triangles[0] = 0;
            triangles[0] = 0;
            triangles[0] = 0;
            triangles[0] = 0;
            triangles[0] = 0;
            triangles[0] = 0;
            
            // Top face
            triangles[0] = 0;
            triangles[0] = 0;
            triangles[0] = 0;
            triangles[0] = 0;
            triangles[0] = 0;
            triangles[0] = 0;
            
            // Right face
            triangles[0] = 0;
            triangles[0] = 0;
            triangles[0] = 0;
            triangles[0] = 0;
            triangles[0] = 0;
            triangles[0] = 0;
            
            // Left face
            triangles[0] = 0;
            triangles[0] = 0;
            triangles[0] = 0;
            triangles[0] = 0;
            triangles[0] = 0;
            triangles[0] = 0;
            
            // Back face
            triangles[0] = 0;
            triangles[0] = 0;
            triangles[0] = 0;
            triangles[0] = 0;
            triangles[0] = 0;
            triangles[0] = 0;
            
            // Bottom face
            triangles[0] = 0;
            triangles[0] = 0;
            triangles[0] = 0;
            triangles[0] = 0;
            triangles[0] = 0;
            triangles[0] = 0;
            
            





            
            
            yield return null;
        }
    }
}
