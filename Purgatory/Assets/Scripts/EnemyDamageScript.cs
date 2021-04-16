using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageScript : MonoBehaviour {

    private GameObject player;
    int count;
    //public GameObject drop;

    public bool bossmob;

    public float hp;
    private float kScale = 55f;
    private bool knockBack = false;
    private float knockTimer = 0.5f;
    private bool stopState;
    private float stopTimer = 0.16f;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(GetComponent<Enemy2FireWizard>() == true)
        {
            count = 2;
        }
        else
        {
            count = 1;
        }
    }

    void Update ()
    {
        if (hp <= 0)
        {
           
            if (bossmob==false)
            {
                player.GetComponent<PlayerController>().LevelUp();
            }
            Destroy(this.gameObject);
            //Instantiate(drop,transform.position,Quaternion.identity);
            gameObject.GetComponent<DropManager>().Drop();
        }



        if (gameObject.GetComponent<Pathfinding.AIPath>().canMove == false)
        {
            knockTimer -= Time.deltaTime;
        }

        if(knockTimer <= 0)
        {
            stopState = true;
            knockTimer = 0.5f;
        }

        if (stopState == true)
        {
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            stopTimer -= Time.deltaTime;
        }

        if(stopTimer <= 0)
        {
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            gameObject.GetComponent<Pathfinding.AIPath>().canMove = true;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            stopState = false;
            stopTimer = 0.16f;
            knockBack = false;
        }

        if (knockBack == true)
        {
            if (count == 1)
            {
            gameObject.GetComponent<Enemy1Skeleton>().knockback = true;
            }
            else
            {
                gameObject.GetComponent<Enemy2FireWizard>().knockback = true;
            }
        }
        else
        {
            if(count == 1)
            {
                gameObject.GetComponent<Enemy1Skeleton>().knockback = false;
            }
            else
            {
                gameObject.GetComponent<Enemy2FireWizard>().knockback = false;
            }
          
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Attack")
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Vector2 knockbackPos = (this.transform.position - player.transform.position).normalized * kScale;
            gameObject.GetComponent<Pathfinding.AIPath>().canMove = false;
            knockTimer = 0.5f;
            stopState = false;
            stopTimer = 0.16f;
            knockBack = true;
            hp -= player.GetComponent<PlayerController>().aadmg;
            gameObject.GetComponent<Rigidbody2D>().AddForce(knockbackPos, ForceMode2D.Impulse);

        }       
    }
}
