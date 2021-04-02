using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerControler : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public int cherries = 0;



    


    private Collider2D coll;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed;
    [SerializeField] private float jump;
    [SerializeField] private float heartforce = 10f;
    [SerializeField] private AudioSource cherry;
    [SerializeField] private AudioSource powerry;

    private enum State { idle, runing, jumping, falling, hurt,climb }
    private State state = State.idle;

    [HideInInspector] public bool canClimb = false;
    [HideInInspector] public bool bottomLadder = false;
    [HideInInspector] public bool topLadder = false;
    [HideInInspector] public Ladder ladder;
    private float naturalGravity;
    [SerializeField] float climbSpeed = 3f;

    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        naturalGravity = rb.gravityScale;

       

    }



    private void Update()
    {





        if (state == State.climb)
        {
            Climb();
        }



        if (state != State.hurt)
        {
            Movment();
        }
        VelocityState();
        anim.SetInteger("state", (int)state);

    }

   



    private void Movment()
    {


        if(Input.GetKeyDown(KeyCode.Space))
          {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            PernamentUI.pern.Reset();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("menu");
            PernamentUI.pern.Reset();
        }











        if (canClimb && Mathf.Abs(Input.GetAxis("Vertical")) >.1f)
        {
            state = State.climb;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | 
                RigidbodyConstraints2D.FreezeRotation;
            transform.position = new Vector3(ladder.transform.position.x, rb.position.y);
            rb.gravityScale = 0f;

        }


        float velocity = Input.GetAxis("Horizontal") * speed;
        bool isJumping = Input.GetKey(KeyCode.Space);

        anim.SetFloat("Speed", Mathf.Abs(velocity));




       
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);

        }
        else
        {

        }

        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))

        {

          RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.down, 1.3f, ground);

           if (hit.collider != null)

                Jump();

        }
    }


    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jump);
        state = State.jumping;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (state == State.falling)
            {

                enemy.jumpedOn();
                Jump();
                
            }
            else
            {
                state = State.hurt;
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(-heartforce, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(heartforce, rb.velocity.y);

                }
            }
        }

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            cherry.Play();
            Destroy(collision.gameObject);
            PernamentUI.pern.cherries++;
            PernamentUI.pern.Licznik.text = PernamentUI.pern.cherries.ToString();
        }
        if(collision.tag == "PowerUp")
        {
            powerry.Play();
            Destroy(collision.gameObject);
            jump = 9f;
            speed = 10f;

            Color color1;
            Color color2;
            ColorUtility.TryParseHtmlString("#B5BAFF", out color1);
            ColorUtility.TryParseHtmlString("#FF0090", out color2);


            StartCoroutine(ResetPower());

        }

    }




    private void VelocityState()
    {


        if(state == State.climb)
        {

        }
        else if (state == State.jumping)
        {
            if (rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }

        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            state = State.runing;
        }
        else
        {
            state = State.idle;
        }

    }




    private IEnumerator ResetPower()
    {


        yield return new WaitForSeconds(10);
        jump = 7;
        speed = 5;
        GetComponent<SpriteRenderer>().color = Color.white;
    }


    private void Climb()
    {
        if (Input.GetButtonDown("Jump"))

        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            canClimb = false;
            rb.gravityScale = naturalGravity;
            anim.speed = 1f;
            Jump();
            return;
           

        }
        float vDierction = Input.GetAxis("Vertical");

        if(vDierction > .1f && !topLadder)
        {
            rb.velocity = new Vector2(0f, vDierction * climbSpeed);
            anim.speed = 1f;

        }
        else if(vDierction < -.1f && !bottomLadder)
        {
            rb.velocity = new Vector2(0f, vDierction * climbSpeed);
            anim.speed = 1f;
        }
        else
        {
            anim.speed = 0f;
            rb.velocity = Vector2.zero;
        }
    }








}








/*public function if (Input.GetKey(KeyCode.A))
{
    rb.velocity = new Vector2(-speed, rb.velocity.y);
    transform.localScale = new Vector2(-1, 1);

}
else if (Input.GetKey(KeyCode.D))
{
    rb.velocity = new Vector2(speed, rb.velocity.y);
    transform.localScale = new Vector2(1, 1);

}
else
{

}

if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
{
    rb.velocity = new Vector2(rb.velocity.x, jump);
    state = State.jumping;
} ()

    {
    //
}
}
*/