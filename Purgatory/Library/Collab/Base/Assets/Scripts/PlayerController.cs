using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;

    private Rigidbody2D rb2d;

    public Animator animator;

    public int direction;

    private int maxLifes = 6;
    public int lifes;

    public bool dmgState = true;

    private float invTime = 0.8f;

    public float aadmg;
    public float ardmg;

    

    // Use this for initialization
    void Start()
    {
        lifes = maxLifes;
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_up") || animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_down") || animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_left")|| animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_right"))
        {
            rb2d.velocity = new Vector2(0, 0);
        }
        else
        {
            rb2d.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * speed, 0.8f),
                       Mathf.Lerp(0, Input.GetAxis("Vertical") * speed, 0.8f));
        }


        //rb2d.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * speed, 0.8f),
        //                                      Mathf.Lerp(0, Input.GetAxis("Vertical") * speed, 0.8f));

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {

            animator.SetBool("IsMoving", true);
        } else { animator.SetBool("IsMoving", false); }
        /*0-down
         1-up
         2-left
         3-right*/

        //int direction = animator.GetInteger("direction");

        if (Input.GetKey(KeyCode.W))
        {
            //transform.Translate(Vector2.down * speed);
            animator.SetInteger("direction", 1);
            direction = 1;
            //animator.SetBool("IsMoving", true);
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            //transform.Translate(Vector2.down * speed);
            animator.SetInteger("direction", 0);
            direction = 0;
            //animator.SetBool("IsMoving", true);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            //transform.Translate(Vector2.right * speed);
            animator.SetInteger("direction", 3);
            direction = 3;
        }

        if (Input.GetKey(KeyCode.A))
        {
            //transform.Translate(Vector2.left * speed);

            animator.SetInteger("direction", 2);
            direction = 2;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Attack");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            lifes--;
        }

        if (dmgState == false)
        {
            //Play invulnerability animation and set dmgState = false;
            invTime -= Time.deltaTime;

            if (invTime <= 0)
            {
                dmgState = true;
                invTime = 1f;
            }
        }

        if(lifes == 0)
        {
            Debug.Log("oof");
            //lifes = 3;
        }
        if(lifes > maxLifes)
        {
            lifes = maxLifes;
        }

        if(lifes < 0)
        {
            lifes = 0;
        }
    }
}
