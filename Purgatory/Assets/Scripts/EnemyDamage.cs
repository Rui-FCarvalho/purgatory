using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    private GameObject player;
    public float hp;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            Destroy(this.gameObject);
            //Instantiate(drop,transform.position,Quaternion.identity);
            gameObject.GetComponent<DropManager>().Drop();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Attack")
        {
            hp -= player.GetComponent<PlayerController>().aadmg;
        }
    }
}

