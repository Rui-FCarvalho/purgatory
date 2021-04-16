using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Skeleton : MonoBehaviour {

    private GameObject player;
    public bool knockback;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerController>().dmgState == true && knockback == false)
            {
                collision.gameObject.GetComponent<PlayerController>().lifes -= 1;
                collision.gameObject.GetComponent<PlayerController>().dmgState = false;
            }
        }
    }
}
