using UnityEngine;

public class NextLevel : MonoBehaviour
{
    private LoadingManager loadingManager;

    private void Awake()
    {
        loadingManager = FindFirstObjectByType<LoadingManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            loadingManager.GoToNextScene();
        }
    }

}
