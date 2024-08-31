using UnityEngine;
using UnityEngine.SceneManagement;

public class DevIntro : MonoBehaviour
{
    //Animation Events
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
