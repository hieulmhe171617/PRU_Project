using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    
    void Update()
    {
        //just for test change scenne 
        if (Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(1);
        }
        //after inject code logic change scene here
    }
}
