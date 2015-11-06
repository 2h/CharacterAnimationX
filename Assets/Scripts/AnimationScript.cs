using UnityEngine;
using System.Collections;

public class AnimationScript: MonoBehaviour
{
   public float maxSpeed = 10f;
   bool facingRight = false;
   
   Animator anim; //calls the animator
   
   bool grounded = false; //creates a boolean for whether char is on ground. Starts as false
   public Transform groundCheck; //public to check where groundcheck is
   float groundRadius = 0.2f; //size of circle used to check where ground is
   public LayerMask whatIsGround; //allows you to set what is or is not ground
   public float jumpForce = 700f;  //sets public jumpForce variable
	
   void Start ()
   {
      anim = GetComponentInChildren<Animator>(); //getcomponentAnimator
   }

   void FixedUpdate () //change this to fixedupdate, better than update
   {
		
     grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround); //this checks if the circle projection for collision is hitting or not. will return true/false
	 anim.SetBool("Ground",grounded); //if grounded, set grounded.
		
	 anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D>().velocity.y); //tells how fast moving up or down
		
     float move = Input.GetAxis ("Horizontal"); //sets input axis to horizontal

     anim.SetFloat("Speed", Mathf.Abs(move)); //sets as an absolute value, so even negative values are taken in, and outputs the value as speed o the animator

     GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y); //this takes current y velocity and moves character based on what key is pressed*maxspeed.

     if (move > 0 &&!facingRight)
        Flip ();
     else if (move < 0 && facingRight) //this ifelse statement says if movement speed is above 0, is facing right, if below 0, flip to face left.
        Flip ();

   }
	
   void Update() //more accurate than fixedupdate, use to check for spacebar keypress
   {
		if(grounded && Input.GetKeyDown(KeyCode.Space)) //if player is grounded and spacebar is place
		{
			anim.SetBool("Ground", false); //tells player that if spacebar is pressed, not grounded anymore
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce)); //adds jumpForce upwards to player
		}
	
   }

   void Flip ()
   {
     facingRight = !facingRight; //when facing opposite direction
     Vector3 theScale = transform.localScale; //flip the scale
     theScale.x *= -1; //flip the world the other way around so will flip animation
     transform.localScale = theScale;
   }

}