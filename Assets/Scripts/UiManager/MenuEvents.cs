using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuEvents : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void LoadLevel()
    {
        SceneManager.LoadScene(gameObject.name);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
