using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    //Player
    private GameObject player;

    //general stuff
    private int sCount = 0;
    private int tsCount = 0;
    private bool secondP = false;
    private bool thirdP = false;

    //shooting stuff
    public GameObject fireBallPrefab;
    GameObject fireBall;
    GameObject fireBall2;
    GameObject fireBall3;
    private float angle;
    private float fbSpeed = 2f;
    private float timer = 1f;
    private float sTime;

    //summon stuff
    public GameObject skeletonPrefab;
    GameObject skeleton;
    private float rPosX;
    private float rPosY;
    private float skeleTimer = 1.0f;
    private int rSkel;

    //state switch stuff
    private BossAction state = BossAction.Start;
    private float wait = 2.0f;

    public enum BossAction
    {
        Start,
        Idle,
        TripleBlaster,
        Laser,
        SummonSkeletons,
    }

    private void Start()
    {
        angle = 0.75f;
        player = GameObject.FindGameObjectWithTag("Player");
    }


    private void Update()
    {

        Vector2 direction = (player.transform.position - transform.position).normalized;
        Vector2 per = Vector2.Perpendicular(direction);
        Vector2 dir2 = new Vector2(direction.x + per.x * angle, direction.y + per.y * angle);
        Vector2 dir3 = new Vector2(direction.x - per.x * angle, direction.y - per.y * angle);


        if(this.gameObject.GetComponent<EnemyDamage>().hp <= 40)
        {
            secondP = true;
            angle = 0.65f;
        }


        if(this.gameObject.GetComponent<EnemyDamage>().hp <= 20)
        {
            thirdP = true;
            secondP = false;
            angle = 0.60f;
        }
        //Debug.Log(state);
        //Debug.Log(rSkel);
        //Debug.Log(skeleTimer);
        //Debug.Log(wait);
        //Debug.Log(sTime);
        //Debug.Log(sCount);
        //Debug.Log(tsCount);

        switch (state)
        {
            case BossAction.Start:

                //animation or something
                state = BossAction.Idle;
                break;

            case BossAction.Idle:

                wait -= Time.deltaTime;

                if (wait <= 0)
                {
                    int rState = Random.Range(0, 2);

                    if(rState == 0)
                    {
                        if (!(tsCount == 2))
                        {
                            sTime = Random.Range(3, 5);
                            sCount = 0;
                            state = BossAction.TripleBlaster;
                        }
                        else
                        {
                            state = BossAction.Idle;
                        }
                    }
                    if(rState == 1)
                    {
                        if (sCount == 0)
                        {
                            rSkel = Random.Range(2, 4);
                            tsCount = 0;
                            state = BossAction.SummonSkeletons;
                        }
                        else
                        {
                            state = BossAction.Idle;
                        }
                    }
                    if(rState == 2)
                    {
                        if (!(tsCount == 2))
                        {
                            sTime = Random.Range(3, 5);
                            sCount = 0;
                            state = BossAction.TripleBlaster;
                        }
                        else
                        {
                            state = BossAction.Idle;
                        }
                    }

                }
                break;

            case BossAction.TripleBlaster:

                if (secondP == true)
                {
                    fbSpeed = 3f;
                }
                else if (thirdP == true)
                {
                    fbSpeed = 6f;
                }

                sTime -= Time.deltaTime;

                if(sTime >= 0)
                {
                    timer -= Time.deltaTime;
                    if (timer <= 0)
                    {
                        fireBall = Instantiate(fireBallPrefab, this.transform.position, Quaternion.identity);
                        fireBall2 = Instantiate(fireBallPrefab, this.transform.position, Quaternion.identity);
                        fireBall3 = Instantiate(fireBallPrefab, this.transform.position, Quaternion.identity);
                        fireBall.GetComponent<Rigidbody2D>().velocity = direction * fbSpeed;
                        fireBall2.GetComponent<Rigidbody2D>().velocity = dir2 * fbSpeed;
                        fireBall3.GetComponent<Rigidbody2D>().velocity = dir3 * fbSpeed;
                        if (secondP == true)
                        {
                            timer = 0.1f;
                        }
                        else if (thirdP == true)
                        {
                            timer = 0.03f;
                        }
                        else
                        {
                            timer = 1f;
                        }
                    }
                }

                if(sTime <= 0)
                {
                    wait = 2.0f;
                    tsCount += 1;
                    state = BossAction.Idle;
                }

               break;


            case BossAction.SummonSkeletons:

                for (int i = 0; i < rSkel; i++)
                {
                    rPosX = this.transform.position.x + Random.Range(-2, 2);
                    rPosY = this.transform.position.y + Random.Range(-2, 2);

                    Vector2 Pos = new Vector2(rPosX, rPosY);

                    skeleton = Instantiate(skeletonPrefab, Pos, Quaternion.identity);
                }

                
                wait = 4.0f;
                sCount += 1;
                state = BossAction.Idle;

                break;
        }
    }
}
