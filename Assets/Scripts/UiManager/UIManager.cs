using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject pausePannel;
    
    public void PauseGame()
    {
        pausePannel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePannel.SetActive(false);
    }

    public void RestartGame()
    {
       int sceneIndex = SceneManager.GetActiveScene().buildIndex;
       SceneManager.LoadScene(sceneIndex);
    }
    public void BackToHome()
    {
        SceneManager.LoadScene("MenuGame");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
