using UnityEngine;
using UnityEngine.UI;

public class LevelManage : MonoBehaviour
{
    public Button[] levels;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("best_level") == 0)
        {
            PlayerPrefs.SetInt("best_level", 1);
        }
        //Debug.Log(PlayerPrefs.GetInt("best_level"));
       
    }

    void Start()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (i <= PlayerPrefs.GetInt("best_level") - 1)
            {
                levels[i].transform.GetChild(1).gameObject.SetActive(false);               
            } else
            { 
                levels[i].enabled = false;
            }
            if (i < PlayerPrefs.GetInt("best_level") - 1)
            {
                levels[i].transform.GetChild(2).gameObject.SetActive(true);
            } else
            {
                levels[i].transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }

    public void CheckBestLevel()
    {

    }
    

}
