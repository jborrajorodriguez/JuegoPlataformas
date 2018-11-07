using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControler : MonoBehaviour {

    public float maxSpeed = 5f;
    public float speed = 2f;
    private Rigidbody2D rb2d;
    private Animator anim;
    public bool grounded;
    public float jumPower = 6.5f;
    private bool jump;
    private bool doublejump;
    private bool movement = true;
    private SpriteRenderer spr;
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        anim.SetBool("Grounded", grounded);
        if (grounded)
        {
            
            doublejump = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded)
            {
                jump = true;
                doublejump = true;
            }else if (doublejump) { jump = true;doublejump = false; }
        }
		
	}
    void FixedUpdate()
    {
        Vector3 fixedVelocity = rb2d.velocity;
        fixedVelocity.x *= 0.75f;
        if (grounded)
        {
            rb2d.velocity = fixedVelocity;
        }
        float h = Input.GetAxis("Horizontal");
        if (!movement)
        {
            h = 0;
        }
        rb2d.AddForce(Vector2.right * speed * h);
        if (rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }
        if (rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }
        if (h > 0.1f)
        {
            transform.localScale = new Vector3(0.39f, 0.4f, 1f);
        }
        if (h < -0.1f)
        {
            transform.localScale = new Vector3(-0.39f, 0.4f, 1f);
        }
        if (jump)
        {   
            rb2d.AddForce(Vector2.up * jumPower, ForceMode2D.Impulse);
            jump = false;
        }
    }
    private void OnBecameInvisible()
    {
        //respawn
        transform.position = new Vector3(-7, 0, 0);
    }
    public void enemyjump()
    {
        jump = true;
    }
    public void enemyknockback(float enemyPosX)
    {
        jump = true;
        float side = Mathf.Sign(enemyPosX - transform.position.x);
        rb2d.AddForce(Vector2.left * jumPower, ForceMode2D.Impulse);
        movement = false;
        Invoke("EnableMovement", 0.7f);
        spr.color = Color.red;
    }
    void EnableMovement()
    {
        movement = true;
        spr.color = Color.white;
    }
}
