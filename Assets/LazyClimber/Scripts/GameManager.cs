using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LazyClimber
{
    public class GameManager : MonoBehaviour
    {
        // Singleton 
        public static GameManager Instance { get; private set; }
        
        // Create a scene enum
        private enum SceneIndex
        {
            MenuScene = 0,
            Level01 = 1,
            Level02 = 2,
            Level03 = 4,
        }

        private void Awake()
        {
            // Check singleton instance
            if (Instance != null && Instance != this) Destroy(this);
            else Instance = this;
        }

        // Load scene methods. Just for ease of calling
        public void LoadMenuScene() => SceneManager.LoadSceneAsync((int)SceneIndex.MenuScene);
        public void LoadLevel01() => SceneManager.LoadSceneAsync((int)SceneIndex.Level01);
        public void LoadLevel02() => SceneManager.LoadSceneAsync((int)SceneIndex.Level02);
        public void LoadLevel03() => SceneManager.LoadSceneAsync((int)SceneIndex.Level03);
        
    }
}
