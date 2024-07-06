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
            SceneManager.LoadScene(++sceneIndex);
        }

    }
}
