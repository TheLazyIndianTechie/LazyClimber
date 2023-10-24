using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;
using System.Collections.Generic;
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
            List<Vector3> vertices = new List<Vector3>(new Vector3[8]); // Define a list of vertices (8 points form the cube array)
            
            Vector3 startPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)); // Set Z as 10 because camera is -10 from origin.
            Vector3 temp = new Vector3(startPosition.x, startPosition.y, 0.5f); // Have all drawings start from a single point.
            
            // Construction of triangles array
            // Cube is 6 faces and each face is 2 triangles. so 12 triangles! 
            // One triangle is 3 verts so total of 36 verts.
            // TODO: Research more on Winding - Unity handles winding in clock-wise for front-facing and anti-clockwise for back facing?

            for (int i = 0; i < vertices.Count; i++) vertices[i] = temp; // For loop instantiates all 8 points at the temp pos.

            List<int> triangles = new List<int>(new int[36]); // creating a triangle list of collection size 36 as needed
            
            // Construct the faces
            //TODO: Vinay to Read more Reference: https://ilkinulas.github.io/development/unity/2016/04/30/cube-mesh-in-unity3d.html
            
            // Front face
            triangles[0] = 0;
            triangles[1] = 2;
            triangles[2] = 1;
            triangles[3] = 0;
            triangles[4] = 3;
            triangles[5] = 2;
            
            // Top face
            triangles[6] = 2;
            triangles[7] = 3;
            triangles[8] = 4;
            triangles[9] = 2;
            triangles[10] = 4;
            triangles[11] = 5;
            
            // Right face
            triangles[12] = 1;
            triangles[13] = 2;
            triangles[14] = 5;
            triangles[15] = 1;
            triangles[16] = 5;
            triangles[17] = 6;
            
            // Left face
            triangles[18] = 0;
            triangles[19] = 7;
            triangles[20] = 4;
            triangles[21] = 0;
            triangles[22] = 4;
            triangles[23] = 3;
            
            // Back face
            triangles[24] = 5;
            triangles[25] = 4;
            triangles[26] = 7;
            triangles[27] = 5;
            triangles[28] = 7;
            triangles[29] = 6;
            
            // Bottom face
            triangles[30] = 0;
            triangles[31] = 6;
            triangles[32] = 7;
            triangles[33] = 0;
            triangles[34] = 1;
            triangles[35] = 6;
            
            // Assign triangles and vertices to meshes
            // Convert from list to arrays
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();

            // Assign the mesh to the drawing gameobject
            drawing.GetComponent<MeshFilter>().mesh = mesh;

            Vector3 lastMousePosition = startPosition; // Calculate vertices after storing mouse position

            while (true) // bad practice but fine for now. while loop to add more verts and triangles
            {
                vertices.AddRange(new Vector3[4]);
                triangles.AddRange(new int[30]);

                // Calculate new vertices
                int vIndex = vertices.Count - 8;
                int vIndex0 = vIndex + 0;
                int vIndex1 = vIndex + 1;
                int vIndex2 = vIndex + 2;
                int vIndex3 = vIndex + 3;
                int vIndex4 = vIndex + 4;
                int vIndex5 = vIndex + 5;
                int vIndex6 = vIndex + 6;
                int vIndex7 = vIndex + 7;
                
                // Calculate direction by getting difference between last mouse position and current mouse position
                Vector3 currentMousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
                Vector3 mouseForwardVector = (lastMousePosition - currentMousePosition).normalized;

            }
            
            yield return null;
        }
    }
}
