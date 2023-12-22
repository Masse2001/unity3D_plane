using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private bool isCockpitActivated = false;

    // Ajoutez une propriété pour accéder à l'instance depuis d'autres scripts
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameManagerObject = new GameObject("GameManager");
                instance = gameManagerObject.AddComponent<GameManager>();
                DontDestroyOnLoad(gameManagerObject);
            }
            return instance;
        }
    }

    void Awake()
{
    if (instance == null)
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    else
    {
        Debug.Log("GameManager déjà existant. Détruire le GameManager nouvellement créé.");
        Destroy(gameObject);
    }
}


    public void LoadFlightDemoScene()
    {
        SceneManager.LoadScene("Flight Demo");
    }

    public bool IsCockpitActivated()
    {
        return isCockpitActivated;
    }

    public void ToggleCockpit()
    {
        isCockpitActivated = !isCockpitActivated;
    }
}
