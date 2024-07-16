using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : Singleton<LoadingManager>
{
    int sceneIndex;
    int lastScenceIndex;

    private void Awake()
    {
        lastScenceIndex = SceneManager.sceneCountInBuildSettings - 1;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void GoToNextScene()
    {
        if (sceneIndex <= lastScenceIndex)
        {
            if (sceneIndex != 0 && sceneIndex + 1 > PlayerPrefs.GetInt("best_level"))
            {         
                PlayerPrefs.SetInt("best_level", sceneIndex+1);
            }
            SceneManager.LoadScene(++sceneIndex);    
        }
    }
}
