using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private bool isAttacking;

    private float attacktimer = 0f;
    private float attackcd = 0.3f;

    int direction;

    public Collider2D leftAttack;
    public Collider2D rightAttack;
    public Collider2D upAttack;
    public Collider2D downAttack;

    //public Collider2D[] colliders;

    public Animator playerAnimator;

	// Use this for initialization
	void Start () {
        playerAnimator = gameObject.GetComponent<Animator>();
        leftAttack.enabled = false;
        rightAttack.enabled = false;
        upAttack.enabled = false;
        downAttack.enabled = false;

        //for (int i = 0; i < colliders.Length; i++)
        //{
        //    colliders[i].enabled = false;
        //}
    }
	
	// Update is called once per frame
	void Update () {


        direction = playerAnimator.GetInteger("direction");

        if(Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {

            isAttacking = true;
            attacktimer = attackcd;

            //colliders[direction].enabled = true;

            if (playerAnimator.GetInteger("direction") == 0)
            {
                downAttack.enabled = true;
            }

            if (playerAnimator.GetInteger("direction") == 1)
            {
                upAttack.enabled = true;
            }

            if (playerAnimator.GetInteger("direction") == 2)
            {
                leftAttack.enabled = true;
            }

            if (playerAnimator.GetInteger("direction") == 3)
            {
                rightAttack.enabled = true;
            }
        }


        if (isAttacking)
        {
            if(attacktimer>0)
            {
                attacktimer -= Time.deltaTime;
            }
            else
            {
                isAttacking = false;
                leftAttack.enabled = false;
                rightAttack.enabled = false;
                upAttack.enabled = false;
                downAttack.enabled = false;

                //for (int i = 0; i < colliders.Length; i++)
                //{
                //    colliders[i].enabled = false;
                //}
            }
        }
		
	}
}
