using UnityEngine;

public class MoveTextEffect : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject intro;
    [SerializeField] private float timeAlive = 5f;

    private float timer = 0;
    private RectTransform rect;

    void Start()
    {
        rect = intro.GetComponent<RectTransform>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        rect.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        transform.position = new Vector3(transform.position.x + speed* Time.deltaTime, transform.position.y, transform.position.z);        
        // Kiểm tra và hủy đối tượng intro sau khi timeAlive hết hạn
        if (timer > timeAlive)
        {
            gameObject.SetActive(false);
        }
    }

}
