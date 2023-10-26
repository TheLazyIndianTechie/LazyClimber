using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine.Serialization;
using GameObject = UnityEngine.GameObject;

namespace LazyClimber
{
    public class MeshCreationManager : MonoBehaviour
    {
        //Public events for emitting messages to listeners 
        public static event Action<string> OnBeginDraw, OnEndDraw; 
        
        // Variables
        [SerializeField] private Camera mainCamera;
        private GameObject _container;
        [SerializeField] private Color drawingColor = Color.yellow; // Allow an option for drawing colour to be chosen, defaults to yellow
        
        public MeshCollider drawArea;

        // Detect if player cursor is within the bounds of the drawing panel bounds
        private bool IsCursorInDrawArea => drawArea.bounds.Contains(mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 11)));

        // Lifecycle methods
        
        // Grabs the main camera from assigned active player input.
        // TODO: Need a better way of handling the nulls if Camera is not assigned. Maybe do a camera.main but might be expensive. Fix after assignment
        private void Start()
        {
            mainCamera = GetComponent<PlayerInput>().camera;
            _container = new GameObject("Drawing Board"); // To hold all runtime created drawing meshes
        }

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

            drawing.transform.localScale = new Vector3(1, 1, 0); // Makes the drawing 2D by limiting the z depth.
            
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

            
            drawing.transform.parent = _container.transform;
            // Assign the mesh to the drawing gameobject
            drawing.GetComponent<MeshFilter>().mesh = mesh;
            
            // Assign colour to render out mat. Set the material to URP unlit
            drawing.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Unlit");
            drawing.GetComponent<Renderer>().material.color = drawingColor;  
            
            // Calculate vertices after storing mouse position
            Vector3 lastMousePosition = startPosition; 

            while (IsCursorInDrawArea) // Check if cursor is in draw area -> while loop to add more verts and triangles based on drawing
            {
                var minDistance = 0.1f; // Min distance between prev vertices and new vertices to avoid spiking issue
                var distance = Vector3.Distance(mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,10)), lastMousePosition); // Calculate the difference between mouse positions

                // while distance < min distance - set to distance and return .
                while (distance < minDistance)
                {
                    distance = Vector3.Distance(mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,10)), lastMousePosition);
                    yield return null;
                }
                
                // Add maximum range for vertices and triangles.
                vertices.AddRange(new Vector3[4]);
                triangles.AddRange(new int[30]);

                // Calculate new vertices
                int vIndex = vertices.Count - 8;
                
                // Prev vertices indices (Reversed from 0,1,2,3)
                int vIndex0 = vIndex + 3;
                int vIndex1 = vIndex + 2;
                int vIndex2 = vIndex + 1;
                int vIndex3 = vIndex + 0;
                
                // New vertices Indices
                int vIndex4 = vIndex + 4;
                int vIndex5 = vIndex + 5;
                int vIndex6 = vIndex + 6;
                int vIndex7 = vIndex + 7;
                
                // Calculate direction by getting difference between last mouse position and current mouse position
                Vector3 currentMousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
                Vector3 mouseForwardVector = (lastMousePosition - currentMousePosition).normalized;

                float lineThickness = 0.25f;
                
                // Calculate vertex
                Vector3 topRightVertex = currentMousePosition + Vector3.Cross(mouseForwardVector, Vector3.back) * lineThickness; // Cross product of mouse forward vec and global back vector
                Vector3 bottomRightVertex = currentMousePosition + Vector3.Cross(mouseForwardVector, Vector3.forward) * lineThickness; // Cross product of mouse forward vec and forward vec
                Vector3 topLeftVertex = new Vector3(topRightVertex.x, topRightVertex.y, 1); // Get pos of right vertices and move z by 1
                Vector3 bottomLeftVertex = new Vector3(bottomRightVertex.x, bottomRightVertex.y, 1); // Get pos of right vertices and move z by 1
                
                // Add to vertices list
                vertices[vIndex4] = topLeftVertex;
                vertices[vIndex5] = topRightVertex;
                vertices[vIndex6] = bottomRightVertex;
                vertices[vIndex7] = bottomLeftVertex;
                
                // Get triangle index at start of new triangles
                int tIndex = triangles.Count - 30;

                // New Top face (2,3,4,2,4,5)
                triangles[tIndex + 0] = vIndex2;
                triangles[tIndex + 1] = vIndex3;
                triangles[tIndex + 2] = vIndex4;
                triangles[tIndex + 3] = vIndex2;
                triangles[tIndex + 4] = vIndex4;
                triangles[tIndex + 5] = vIndex5;
                
                // New Right face (1,2,5,1,5,6)
                triangles[tIndex + 6] = vIndex1;
                triangles[tIndex + 7] = vIndex2;
                triangles[tIndex + 8] = vIndex5;
                triangles[tIndex + 9] = vIndex1;
                triangles[tIndex + 10] = vIndex5;
                triangles[tIndex + 11] = vIndex6;

                // New Left face (0,7,4,0,4,5)
                triangles[tIndex + 12] = vIndex0;
                triangles[tIndex + 13] = vIndex7;
                triangles[tIndex + 14] = vIndex4;
                triangles[tIndex + 15] = vIndex0;
                triangles[tIndex + 16] = vIndex4;
                triangles[tIndex + 17] = vIndex3;
                
                // New back face (5,4,7,0,4,3)
                triangles[tIndex + 18] = vIndex5;
                triangles[tIndex + 19] = vIndex4;
                triangles[tIndex + 20] = vIndex7;
                triangles[tIndex + 21] = vIndex0;
                triangles[tIndex + 22] = vIndex4;
                triangles[tIndex + 23] = vIndex3;
                
                // New bottom face (0,6,7,0,1,6)
                triangles[tIndex + 24] = vIndex5;
                triangles[tIndex + 25] = vIndex4;
                triangles[tIndex + 26] = vIndex7;
                triangles[tIndex + 27] = vIndex0;
                triangles[tIndex + 28] = vIndex4;
                triangles[tIndex + 29] = vIndex3;
                
                // Apply new vertices and triangles to mesh
                mesh.vertices = vertices.ToArray();
                mesh.triangles = triangles.ToArray();
                
                // Update last mouse position
                lastMousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
                
                yield return null;
            }
            
        }
    }
}
