using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frog : Enemy
{


    [SerializeField] private float leftcap;
    [SerializeField] private float rightcap;

    [SerializeField] private float jumpl = 10f;
    [SerializeField] private float jumph = 10f;
    [SerializeField] private LayerMask ground;



    private Collider2D coll;
    
    

    private bool facingleft = true;

    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();


    }




    private void Update()
    {
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < .1)
            {
                anim.SetBool("falling", true);
                anim.SetBool("jumping", false);

            }
        }
        if (coll.IsTouchingLayers(ground) && anim.GetBool("falling"))
        {
            anim.SetBool("falling", false);
        }
    }

    private void Move()
    {
        if (facingleft)
        {
            if (transform.position.x > leftcap)
            {
               
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }


                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-jumpl, jumph);
                    anim.SetBool("jumping", true);
                }
            }
            else
            {
                facingleft = false;
            }
        }

        else
        {
            if (transform.position.x < rightcap)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }


                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(jumpl, jumph);
                    anim.SetBool("jumping", true);
                }
            }
            else
            {
                facingleft = true;
            }
        }
    }

    
   

}
