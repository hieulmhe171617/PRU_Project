using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;
    private Health playerHealth;

    [SerializeField] private int playerRespawnTime = 2;

    private int saveRespawnTime;
    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        saveRespawnTime = playerRespawnTime;
    }

    public void Respawn()
    {
        if (currentCheckpoint != null)
        {
            transform.position = currentCheckpoint.position;
            playerHealth.Respawn();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("appear");
        }
    }

    public int GetRespawnTime() => playerRespawnTime;

    public void DecreaseRespawnTime()
    {
        playerRespawnTime--;
    }

    public bool IsOver() => playerRespawnTime < 0 || (playerHealth.currentHealth <= 0 && currentCheckpoint == null);

    //public void ResetRespawnTime()
    //{
    //    playerRespawnTime = saveRespawnTime;
    //}
}
