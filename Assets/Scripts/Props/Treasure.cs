using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Treasure : MonoBehaviour
{
    [SerializeField] private GameObject PressF;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject ThankText;
    [SerializeField] private GameObject Button;
    [SerializeField] private Text ButtonText;
    [SerializeField] private GameObject Image;

    private Animator animator;
    private int pressTime = 1;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PressF.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                animator.SetTrigger("open");
                SoundManager.instance.PlaySound(audioClip);
                Time.timeScale = 0;
                PressF.SetActive(false);
                panel.SetActive(true);
                Image.SetActive(false);
                ThankText.SetActive(true);
                Button.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (PressF != null)
        {
            PressF.SetActive(false);
        }
    }



    public void ClickButton()
    {
        if (pressTime == 1)
        {
            ThankText.SetActive(false);
            Image.SetActive(true);
            ButtonText.text = "SIUUUU!";
            pressTime++;
        } else
        {
            FindFirstObjectByType<LoadingManager>().DoneLastScence();
            FindAnyObjectByType<UIManager>().BackToHome();
        }
    }
}
