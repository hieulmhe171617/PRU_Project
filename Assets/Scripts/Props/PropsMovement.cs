using UnityEngine;


public class PropsMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Rigidbody2D _rigidbody;

    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] Transform player;
    [SerializeField] float speed;
    int direction = 1;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Movement()
    {
        
        if (transform.position.y >= startPoint.position.y || transform.position.y <= endPoint.position.y )
        {
            direction *= -1;
        }
        if(direction == 1)
        {
            _rigidbody.velocity = Vector2.down * speed * Time.deltaTime;
        } else
        {
            _rigidbody.velocity = Vector2.up * speed * Time.deltaTime;
        }
        
       


    }
    void Update()
    {
        Movement();
    }

 
}
