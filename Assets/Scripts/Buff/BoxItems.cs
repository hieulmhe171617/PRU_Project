using System.Collections.Generic;
using UnityEngine;

public class BoxItems : MonoBehaviour
{

    Transform[] childs;
    List<GameObject> childObjects;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        childs = GetComponentsInChildren<Transform>(true);

        childObjects = new List<GameObject>();

        foreach (Transform child in childs)
        {
            // Tránh thêm GameObject cha vào danh sách
            if (child != this.transform && !child.gameObject.activeInHierarchy)
            {
                childObjects.Add(child.gameObject);
            }
        }

    }

    public void DisplayRandomItem()
    {
        if (childObjects != null && childObjects.Count > 0)
        {
            int randomIndex = Random.Range(0, childObjects.Count);
            if (randomIndex != childObjects.Count)
            {
                childObjects[randomIndex].gameObject.SetActive(true);
            }
        }
    }


}
