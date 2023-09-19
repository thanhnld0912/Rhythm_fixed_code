using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapYCoordinates : MonoBehaviour
{
    private void Start()
    {
        SwapYRecursive(transform);
    }

    private void SwapYRecursive(Transform parent)
    {
        foreach (Transform child in parent)
        {
            // Swap the Y coordinate of the child object
            Vector3 newPosition = new Vector3(child.position.x, -child.position.y, child.position.z);
            child.position = newPosition;

            // Recursively swap Y coordinates of child's children
            SwapYRecursive(child);
        }
    }
}
