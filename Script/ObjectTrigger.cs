using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTrigger : MonoBehaviour
{
    public List<GameObject> objectsToTrigger = new List<GameObject>();
    private int currentObjectIndex = -1;
    private int lastObjectIndex = -1;

    private void Start()
    {
        ResetObjectOrder();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TriggerObject();
        }
    }

    private void TriggerObject()
    {
        int newObjectIndex = currentObjectIndex;
        while (newObjectIndex == currentObjectIndex || newObjectIndex == lastObjectIndex)
        {
            newObjectIndex = Random.Range(0, objectsToTrigger.Count);
        }
        lastObjectIndex = currentObjectIndex;
        currentObjectIndex = newObjectIndex;
        Instantiate(objectsToTrigger[currentObjectIndex], transform.position, Quaternion.identity);
        if (currentObjectIndex == 0)
        {
            ResetObjectOrder();
        }
    }

    private void ResetObjectOrder()
    {
        currentObjectIndex = -1;
        lastObjectIndex = -1;
        Shuffle(objectsToTrigger);
    }

    private void Shuffle(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            GameObject temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
