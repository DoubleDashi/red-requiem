using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class SceneController : MonoBehaviour
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}