using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{


    private SpriteRenderer theSR;
    [SerializeField] private Sprite defaultImage;
    [SerializeField] private Sprite pressedImage;

    [SerializeField] private KeyCode keyToPress;

    [SerializeField] private GameObject n;
    [SerializeField] private bool createMode;

    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if (createMode)
        {


            if (Input.GetKeyDown(keyToPress))
            {
                theSR.sprite = pressedImage;
                Instantiate(n, transform.position, transform.rotation);
            }

            if (Input.GetKeyUp(keyToPress))
            {
                theSR.sprite = defaultImage;
            }
        }
    }

}