using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Debug.Log("Da trigger vao door");
            if (collision.transform.position.x < transform.position.x)
            {
                cam.MoveToNewRoom(nextRoom);
                nextRoom.GetComponent<Room>().ActiveRoom(true);
                previousRoom.GetComponent<Room>().ActiveRoom(false);

            }
            else
            {
                cam.MoveToNewRoom(previousRoom);
                nextRoom.GetComponent<Room>().ActiveRoom(false);
                previousRoom.GetComponent<Room>().ActiveRoom(true);
            }
        }
    }
}
