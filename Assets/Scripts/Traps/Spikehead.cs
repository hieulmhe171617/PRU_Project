using UnityEngine;

public class Spikehead : EnemyDamage
{
    [Header("SpikeHead Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;

    private float checkTimer;

    private Vector3 destination;
    private bool attacking;
    private Vector3[] directions = new Vector3[4];

    private void OnEnable()
    {
        Stop();
    }
    private void Update()
    {
        //move spikehead to destination only if attacking
        if (attacking)
            transform.Translate(destination * Time.deltaTime * speed);
        else
        {
            checkTimer += Time.time;
            if (checkTimer > checkDelay)
                CheckForPlayer();
        }
    }

    private void CheckForPlayer()
    {
        CalculateDirections();
        //check if spikehead sees player in 4 directions
        for (int i = 0; i < directions.Length; i++)
        {
            //Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }

    private void CalculateDirections()
    {
        directions[0] = transform.right * range; //right direction
        directions[1] = -transform.right * range; //left
        directions[2] = transform.up * range; //up
        directions[3] = -transform.up * range; //down
    }

    private void Stop()
    {
        destination = transform.position; //set destination as current position so it doesn't move
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        //stop spikehead once he hits something
        Stop();
    }
}
