using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI buttonText; // Assurez-vous de lier ce composant dans l'éditeur Unity

    // Cette méthode doit être appelée lorsque le bouton Play est cliqué.
    public void LoadFlightDemoScene()
    {
        SceneManager.LoadScene("Flight Demo"); 
    }

    // Cette méthode change le texte du bouton
    public void ToggleCockpitText()
    {
        if (buttonText != null)
        {
            if (buttonText.text == "Cockpit Activé")
            {
                buttonText.text = "Cockpit Désactivé";
            }
            else
            {
                buttonText.text = "Cockpit Activé";
            }
        }
        else
        {
            Debug.LogError("Le composant TextMeshProUGUI n'est pas assigné dans l'éditeur Unity.");
        }
    }
    public void QuitApplication()
    {
        Application.Quit();
        Debug.Log("Application fermée"); // Cette ligne est pour le débogage dans l'éditeur Unity
    }
}
