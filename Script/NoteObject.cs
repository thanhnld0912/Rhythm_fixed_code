using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{

    [SerializeField] private bool canBePressed;
    [SerializeField] private KeyCode keyToPress;

    public GameObject hit, good, perfect, miss;

    [SerializeField] private float speed = 1f;
    Rigidbody2D rb;


    // Start is called before the first frame update

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rb.velocity = new Vector2(0, -speed);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                gameObject.SetActive(false);

                //GameManager.instance.NoteHit();

                if(Mathf.Abs(transform.position.y) > 0.25)
                {
                    Debug.Log("Hit");
                    GameManager.instance.NormalHit();

                    Instantiate(hit, transform.position, hit.transform.rotation);
                } else if(Mathf.Abs(transform.position.y) > 0.05f)
                {
                    Debug.Log("Good");
                    GameManager.instance.GoodHit();

                    Instantiate(good, transform.position, good.transform.rotation);
                }
                else
                {
                    Debug.Log("Perfect");
                    GameManager.instance.PerfectHit();

                    Instantiate(perfect, transform.position, perfect.transform.rotation);
                }
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            canBePressed = true;

            
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator" && gameObject.activeInHierarchy)
        {
            canBePressed = false;

            GameManager.instance.MissedNotes();

            Instantiate(miss, transform.position, miss.transform.rotation);

        }
    }
    
}
