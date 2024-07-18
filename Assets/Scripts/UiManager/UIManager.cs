using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject pausePannel;

    [SerializeField] PlayerRespawn player;

    [SerializeField] GameObject gameOverPannel;

    [SerializeField] GameObject pauseButton;


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
        Time.timeScale = 1f;
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
       SceneManager.LoadScene(sceneIndex);
    }
    public void BackToHome()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuGame");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

 

    
    public IEnumerator ShowGameOver()
    {
       
        if (player)
        {
            if(player.IsOver())
            {
                yield return new WaitForSeconds(1f);
                Time.timeScale = 0f;
                pauseButton.SetActive(false);
                gameOverPannel.SetActive(true);
            }
        }
    }
    private void Update()
    {
        if (player.IsOver())
        {
            StartCoroutine(ShowGameOver());
        } 
        
    }

}
