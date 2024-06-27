using UnityEngine;

public class StepMovement : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private int direction; //0 la di qua lai, 1 la di len xuong
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    private bool movingLeft;
    private bool movingUp;

    private float leftEdge;
    private float rightEdge;

    private float upEdge;
    private float downEdge;

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;

        upEdge = transform.position.y + movementDistance;
        downEdge = transform.position.y - movementDistance;
    }

    private void Update()
    {
        if (direction == 0)
        {
            if (movingLeft)
            {
                if (transform.position.x > leftEdge)
                {
                    transform.position = new Vector3(transform.position.x - Time.deltaTime * speed,
                            transform.position.y, transform.position.z);
                }
                else
                    movingLeft = false;
            }
            else
            {
                if (transform.position.x < rightEdge)
                {
                    transform.position = new Vector3(transform.position.x + Time.deltaTime * speed,
                            transform.position.y, transform.position.z);
                }
                else
                    movingLeft = true;
            }
        } else
        {
            if (movingUp)
            {
                if (transform.position.y < upEdge)
                {
                    transform.position = new Vector3(transform.position.x,
                            transform.position.y + Time.deltaTime * speed, transform.position.z);
                }
                else
                    movingUp = false;
            }
            else
            {
                if (transform.position.y > downEdge)
                {
                    transform.position = new Vector3(transform.position.x,
                            transform.position.y - Time.deltaTime * speed, transform.position.z);
                }
                else
                    movingUp = true;
            }
        }
    }
}
