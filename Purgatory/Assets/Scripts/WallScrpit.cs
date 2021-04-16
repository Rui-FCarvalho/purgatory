using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScrpit : MonoBehaviour {

    private Transform other;

    public Animator animator;


    // Use this for initialization
    void Start()
    {
        other = GameObject.FindGameObjectWithTag("Player").transform;
        //Debug.Log("Player speed" + other.GetComponent<PlayerController>().speed);
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (other)
               // Debug.Log("if other works");
            {
                var dist = Vector2.Distance(other.position, this.transform.position);
                //Debug.Log("dist = " + dist);
                if (dist < 1)
                {
                    if (other.GetComponent<PlayerController>().direction == 1)
                    {
                        Debug.Log("Wsafas");
                        other.transform.Translate(Vector2.right * other.GetComponent<PlayerController>().speed * 0.05f);
                    }
                }
            }
        }
    }
}