using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    private GameObject player;
    private float dmg;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dmg = player.GetComponent<PlayerController>().ardmg;
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if(!(collision.gameObject.tag == "Player"))
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyDamageScript>().hp -= dmg;
            }

            if(collision.gameObject.tag == "Boss")
            {
                collision.gameObject.GetComponent<EnemyDamage>().hp -= dmg;
            }

               Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }       
    }

    private void Update()
    {
        Destroy(this.gameObject, 1.5f);
    }
}
