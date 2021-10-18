using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class LevelSelect : MonoBehaviour
    {
        public void LoadLevel(string levelName)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
        }
    }
}