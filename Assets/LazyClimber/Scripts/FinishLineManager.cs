using UnityEngine;

namespace LazyClimber
{
    public class FinishLineManager : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            GameManager.Instance.Win();
        }
    }
}
