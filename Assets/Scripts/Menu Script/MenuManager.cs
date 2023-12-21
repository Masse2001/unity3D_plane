using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI buttonText;

    public void LoadFlightDemoScene()
    {
        // Utilisez le GameManager.Instance pour charger la scène et conserver les données du jeu
        GameManager.Instance.LoadFlightDemoScene();
    }

    public void ToggleCockpitText()
    {
        if (buttonText != null)
        {
            // Utilisez le GameManager.Instance pour gérer l'état du cockpit
            GameManager.Instance.ToggleCockpit();
            buttonText.text = GameManager.Instance.IsCockpitActivated() ? "Cockpit Activé" : "Cockpit Désactivé";
        }
        else
        {
            Debug.LogError("Le composant TextMeshProUGUI n'est pas assigné dans l'éditeur Unity.");
        }
    }

    public void QuitApplication()
    {
        Application.Quit();
        Debug.Log("Application fermée");
    }
}
