using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemanager : MonoBehaviour
{
    void OnEnable()
    {
        // Unpause the game on load
        Time.timeScale = 1;
    }

    // Load main menu scene
    [ContextMenu("Return to Main Menu")]
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    // Load active scene
    [ContextMenu("Reload Scene")]
    public void ReLoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}