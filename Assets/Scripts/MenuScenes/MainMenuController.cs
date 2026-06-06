using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera creditsCamera;
    [SerializeField] private Canvas mainMenuCanvas;
    [SerializeField] private Canvas creditsCanvas;


    private void Start()
    {
        mainCamera.enabled = true;
        creditsCamera.enabled = false;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToCredits()
    {
        mainCamera.enabled = false;
        creditsCamera.enabled = true;
        mainMenuCanvas.gameObject.SetActive(false);
        creditsCamera.gameObject.SetActive(true);
    }

    public void GoToMainMenu()
    {
        mainCamera.enabled = true;
        creditsCamera.enabled = false;
        mainMenuCanvas.gameObject.SetActive(true);
        creditsCamera.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBGL
            return;
        #else
            Application.Quit();
        #endif
    }
}
