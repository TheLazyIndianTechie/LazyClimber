using UnityEngine;
namespace LazyClimber
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform followTarget;
        [SerializeField] private Vector3 offset = new Vector3(0f, 2f,8f);
        [SerializeField] private float smoothSpeed = 4f;
        private bool _isFollowTargetNull;

        // Avoid null checks by creating a bool check in start
        private void Start() => _isFollowTargetNull = followTarget == null;
    
        private void LateUpdate()
        {
            if (_isFollowTargetNull) return; // Null check

            Vector3 desiredPosition = followTarget.position + offset; // Set desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime); // Apply damping
            transform.position = smoothedPosition; // Set position to smooth pos
            transform.LookAt(followTarget); // Set look at 

        }
    }
}
