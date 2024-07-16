using System.Collections;
using UnityEditor;
using UnityEngine;

public class KiwiBuff : MonoBehaviour
{
    [SerializeField] private float buffTime;
    [SerializeField] private float buffAttackCooldownTime;
    [SerializeField] private AudioClip pickupSound;

    private GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SoundManager.instance.PlaySound(pickupSound);
            player = collision.gameObject;
            float startAttackCooldown = player.GetComponent<PlayerAttack>().GetStartAttackCooldown();
            StartCoroutine(BuffAttackCooldown(startAttackCooldown, buffAttackCooldownTime, player));
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }


    private IEnumerator BuffAttackCooldown(float startCooldown, float newCooldown, GameObject player)
    {
        player.GetComponent<PlayerAttack>().NewAttackCooldown(newCooldown);
        yield return new WaitForSeconds(buffTime);

        player.GetComponent<PlayerAttack>().NewAttackCooldown(startCooldown);
    }
}
