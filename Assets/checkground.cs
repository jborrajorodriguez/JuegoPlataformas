using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkground : MonoBehaviour {
    private playerControler player;
    private Rigidbody2D rb2d;
	
	void Start () {
        player = GetComponentInParent<playerControler>();
        rb2d = GetComponentInParent<Rigidbody2D>();
		
	}
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Platform")
        {
            rb2d.velocity = new Vector3(0f, 0f, 0f);
            player.transform.parent = col.transform;
            player.grounded = true;
        }

    }
    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            player.grounded = true;
        }
        if (col.gameObject.tag == "Platform")
        {
            
            player.transform.parent = col.transform;
            player.grounded = true;
        }

    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            player.grounded = false;
        }
        if (col.gameObject.tag == "Platform")
        {
            player.transform.parent = null;
            player.grounded = false;
        }
    }

}
