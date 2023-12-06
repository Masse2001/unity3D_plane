using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class menuChangeScene : MonoBehaviour
{

    void Update()
    {
        // Vérifiez si la touche "Échap" est enfoncée.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("menu"); 
        }
    }
}
