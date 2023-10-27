using UnityEngine;

namespace LazyClimber
{
    public class MaterialVariants : MonoBehaviour
    {
        // Variables
        private Material _material;
        [SerializeField] private Color color;

        private void Start()
        {
            var go = gameObject; // Retrieves current go script is attached to
            _material = go.GetComponent<MeshRenderer>().material; // Get material from renderer
            _material.color = color; // Assign chosen color
        }
    }
}
