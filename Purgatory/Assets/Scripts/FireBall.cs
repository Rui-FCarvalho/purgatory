using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

    private GameObject player;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (player.GetComponent<PlayerController>().dmgState == true)
            {
                player.GetComponent<PlayerController>().lifes -= 1;
                player.GetComponent<PlayerController>().dmgState = false;
            }

            Destroy(this.gameObject);
        }

        if(collision.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        Destroy(this.gameObject, 5f);
    }
}
