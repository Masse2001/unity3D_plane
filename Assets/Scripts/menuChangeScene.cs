// menuChangeScene.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuChangeScene : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Rechargez la scène précédente sans réinitialiser le GameManager
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1, LoadSceneMode.Single);
        }
    }
}
