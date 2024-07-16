using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class BossScreen : MonoBehaviour
{

    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject door;
    [SerializeField] private CinemachineCamera Cinemachine;
    [SerializeField] private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private bool isOpenDoor = false;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
    
        if (boss.GetComponent<Boss>().isDead && !isOpenDoor)
        {
            StartCoroutine(OpenDoor(door));
            isOpenDoor = true;
            
        }
    }

    private IEnumerator OpenDoor(GameObject door)
    {
        Cinemachine.Follow = door.transform;
        Cinemachine.LookAt = door.transform;
        yield return new WaitForSeconds(1f);
        door.GetComponent<Animator>().SetTrigger("open");
        yield return new WaitForSeconds(1f);
        door.GetComponent<BoxCollider2D>().enabled = false;
        Cinemachine.Follow = player.transform;
        Cinemachine.LookAt = player.transform;

    }
}
