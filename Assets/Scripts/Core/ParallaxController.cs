using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    Transform cam; //main camera
    //[SerializeField] Transform virtualcam;
    Vector3 camStartPos;
    float distanceX; // khoang cach giua start position va position hien tai
    float distanceY;

    GameObject[] allQuadLayers;
    Material[] mat;
    float[] layerSpeed;

    float farthestBack;


    [Range(0.01f, 0.25f)]
    [SerializeField] private float parallaxSpeed;

    void Start()
    {
        cam = Camera.main.transform;
        camStartPos = cam.position;

        int layerCount = transform.childCount; //dem so children ma tranform nay co
        //Debug.Log("child count: " + layerCount);
        mat = new Material[layerCount];
        layerSpeed = new float[layerCount];
        allQuadLayers = new GameObject[layerCount];

        for (int i = 0; i < layerCount; i++)
        {
            allQuadLayers[i] = transform.GetChild(i).gameObject;
            mat[i] = allQuadLayers[i].GetComponent<Renderer>().material;
        }

        CalculateSpeedLayer(allQuadLayers.Length);

    }

    private void CalculateSpeedLayer(int layerCount)
    {
        for (int i = 0; i < layerCount; i++) //tim cai layer nao xa camera nhat theo truc Z
        {
            if (allQuadLayers[i].transform.position.z - cam.position.z > farthestBack)
            {
                farthestBack = allQuadLayers[i].transform.position.z - cam.position.z;
            }
        }

        //tinh toan speed cho tung layer
        for (int i = 0; i < layerCount; i++)
        {
            layerSpeed[i] = 1.05f - (allQuadLayers[i].transform.position.z - cam.position.z) / farthestBack;
        }


    }

    private void LateUpdate()
    {
        distanceX = cam.position.x - camStartPos.x;
        distanceY = cam.position.y - camStartPos.y;
        transform.position = new Vector3(cam.position.x, cam.position.y, 0);
        for (int i = 0; i < allQuadLayers.Length; i++)
        {
            float speed = layerSpeed[i] * parallaxSpeed;
            mat[i].SetTextureOffset("_MainTex", new Vector2(distanceX, distanceY) * speed);
        }

    }
}
