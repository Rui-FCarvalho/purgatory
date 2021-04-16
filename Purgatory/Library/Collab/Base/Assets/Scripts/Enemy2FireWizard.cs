using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2FireWizard : MonoBehaviour
{
    private GameObject player;
    public GameObject fireBallPrefab;
    private GameObject mage;
    GameObject fireBall;
    GameObject fireBall2;
    GameObject fireBall3;
    public float speed;
    public float fbSpeed;
    private float range;
    private Vector2 rayDirection;
    private float wait = 2f;
    private bool attacking = true;
    private bool running = false;
    private float rWait = 3f;
    private bool cd = false;
    private float cdTimer = 8f;
    public bool knockback = false;
    RaycastHit2D hit;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mage = this.gameObject;
    }

    void Update()
    {

        range = Vector2.Distance(transform.position, player.transform.position);

        var direction = (player.transform.position - transform.position).normalized;

        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        rayDirection = player.transform.position - transform.position;
        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
        hit = Physics2D.Raycast(transform.position, rayDirection, Mathf.Infinity, layerMask);

        if (hit.transform == player.transform)
        {
            this.GetComponent<Pathfinding.AIPath>().endReachedDistance = 2.4f;
            if (range <= 2.4)
            {
                wait -= Time.deltaTime;
            }

            if (range <= 2.4f && wait <= 0 && attacking == true && running == false)
            {
                fireBall = Instantiate(fireBallPrefab, this.transform.position, Quaternion.identity);
                fireBall.GetComponent<Rigidbody2D>().velocity = direction * fbSpeed;
                wait = 2f;
            }
        }
        else
        {
           this.GetComponent<Pathfinding.AIPath>().endReachedDistance = 0.1f;
        }

        if (range <= 0.6f && cd == false)
        {
           running = true;
        }

        if(running == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -1 * speed * Time.deltaTime);
            rWait -= Time.deltaTime;
        }

        if(rWait <= 0)
        {
            cd = true;
            running = false;
            rWait = 4f;
        }

        if (cd == true)
        {
            cdTimer -= Time.deltaTime;
            if(cdTimer <= 0)
            {
                cd = false;
                cdTimer = 8f;
            }
        }

        if(knockback == true)
        {
            attacking = false;
        }
        else
        {
            attacking = true;
        }
      
        //this.GetComponent<Pathfinding.AIPath>().endReachedDistance = 0.30f;
    }
}
