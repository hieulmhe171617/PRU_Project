using System.Collections;
using UnityEditor;
using UnityEngine;

public class OrangeBuff : MonoBehaviour
{
    [SerializeField] private float buffInvulnerableTime;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private GameObject shield;

    private GameObject player;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SoundManager.instance.PlaySound(pickupSound);
            player = collision.gameObject;

            StartCoroutine(BuffInvulnerable(buffInvulnerableTime));
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }


    private IEnumerator BuffInvulnerable(float buffInvulnerableTime)
    {
        Physics2D.IgnoreLayerCollision(8, 10, true);
        shield.SetActive(true);
        yield return new WaitForSeconds(buffInvulnerableTime);
        Physics2D.IgnoreLayerCollision(8, 10, false);
        shield.SetActive(false);

    }
}
