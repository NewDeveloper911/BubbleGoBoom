using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenu : MonoBehaviour
{
    /* public GameObject menuUI;        // Reference to the UI panel for the menu
    public Button startButton;       // Reference to the Start button
    public Button quitButton;        // Reference to the Quit button (optional) */
     // Reference to the TextMeshProUGUI component (UI element)
    public TextMeshProUGUI statusText;

    public void StartGame()
    {
        // Update the text when the game starts (before transitioning to the main scene)
        if (statusText != null)
        {
            statusText.text = "Loading Game...";
        }

        // Load the main game scene
        SceneManager.LoadScene("SampleScene"); // Make sure "MainScene" matches your scene name
    }

    
}

