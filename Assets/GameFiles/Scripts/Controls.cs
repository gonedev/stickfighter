using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

	[System.Serializable]
	public class KeyCodes {
		// Controls
		public KeyCode up = KeyCode.W;
		public KeyCode down = KeyCode.S;
		public KeyCode left = KeyCode.A;
		public KeyCode right = KeyCode.D;

		public KeyCode handHit = KeyCode.G;
		public KeyCode legHit = KeyCode.H;
		public KeyCode fireBall = KeyCode.J;

		// Controls constructor
		public KeyCodes (KeyCode up, KeyCode down, KeyCode left, KeyCode right, KeyCode handHit, KeyCode legHit, KeyCode fireBall) {
			// WASD
			this.up = up;
			this.down = down;
			this.left = left;
			this.right = right;

			// Punches
			this.handHit = handHit;
			this.legHit = legHit;
			this.fireBall = fireBall;
		}
	}

	[System.Serializable]
	public class Player {
		public GameObject go;				// GameObjects for each player
		public Animator anim;				// GameObjects animation

		public bool direction;				// Mirrored to face each other
		public bool isGrounded;				// Check if player is grounded
		public bool isCrouching;			// Check if player is crouching

		public float jumpVelocityMax = 10;	// Jumping multiplier of the object
		public float moveVelocityMax = 5;	// Moving multiplier of the object
		public Vector3 velocity;			// Current 3D velocity of the Object

		public KeyCodes keyCodes;			// Controls of players
	}

	public Player[] player;

	void Start () {
		int playerAmount = gameObject.GetComponent<Creator>().playerAmount;
		player = new Player[playerAmount];
		for(int i = 0; i < player.Length; i++){
			player[i] = new Player();
			player[i].go = GameObject.Find("Player " + (i + 1));
			player[i].anim = player[i].go.GetComponent<Animator>();
		}

		SetControls();								// Setting controls
	}
	
	void SetControls () {
		// Setting key controls for player 1 (up, down, left, right)
		player[0].keyCodes = new KeyCodes(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D,
								 KeyCode.G, KeyCode.H, KeyCode.J);

		// Setting key controls for player 2 (up, down, left, right)								
		player[1].keyCodes = new KeyCodes(KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow,
								 KeyCode.Keypad1, KeyCode.Keypad2, KeyCode.Keypad3);	
	}

	void Update () {
		//DebugCheckControls();
		SetDirections();	// Setting players facing each other
		KeyControls();		// Player movement
		CrouchingNJumping();
	}

	void CrouchingNJumping() {
		for(int i = 0; i < player.Length; i++){
			if(player[i].isCrouching) {
				player[i].anim.Play("Crouch");
			} else if (!player[i].isGrounded) {
				player[i].anim.Play("Jump");
			}
		}
	}

	void KeyControls () {
		for(int i = 0; i < player.Length; i++) {
			player[i].velocity = player[i].go.GetComponent<Rigidbody>().velocity;		// Saving velocity in variable
			player[i].isGrounded = player[i].go.GetComponent<Grounded>().isGrounded;	// Saving IsGrounded boolean in variable

			// UP
			if(Input.GetKeyDown(player[i].keyCodes.up) && player[i].isGrounded){
				player[i].go.GetComponent<Rigidbody>().velocity = 
					new Vector3(player[i].velocity.x, player[i].jumpVelocityMax, 0f);
				player[i].anim.Play("Jump");		
			}
	
			// CROUCH
			if(Input.GetKey(player[i].keyCodes.down)){
				player[i].isCrouching = true;
				player[i].anim.Play("Crouch");
			} else {
				player[i].isCrouching = false;
			}
	
			// LEFT
			if(Input.GetKey(player[i].keyCodes.left)){
				player[i].go.GetComponent<Rigidbody>().velocity = 
					new Vector3(-player[i].moveVelocityMax, player[i].velocity.y, 0f);
				player[i].anim.Play("Run");
			}
	
			// RIGHT
			if(Input.GetKey(player[i].keyCodes.right)){
				player[i].go.GetComponent<Rigidbody>().velocity = 
					new Vector3(player[i].moveVelocityMax, player[i].velocity.y, 0f);
				player[i].anim.Play("Run");
			}
	
			// UP LEFT
			if(Input.GetKey(player[i].keyCodes.left) && Input.GetKeyDown(player[i].keyCodes.up) && player[i].isGrounded){
				player[i].go.GetComponent<Rigidbody>().velocity = 
					new Vector3(-player[i].moveVelocityMax, player[i].jumpVelocityMax, 0f);
				player[i].anim.Play("Jump");
			}
	
			// UP RIGHT
			if(Input.GetKey(player[i].keyCodes.right) && Input.GetKeyDown(player[i].keyCodes.up) && player[i].isGrounded){
				player[i].go.GetComponent<Rigidbody>().velocity = 
					new Vector3(player[i].moveVelocityMax, player[i].jumpVelocityMax, 0f);
				player[i].anim.Play("Jump");
			}
	
			// HAND HIT
			if(Input.GetKeyDown(player[i].keyCodes.handHit) && player[i].isGrounded){
				// Hand hit
				player[i].anim.Play("HandHit");
			}

			// LEG HIT
			if(Input.GetKeyDown(player[i].keyCodes.legHit) && player[i].isGrounded){
				// Leg hit
				player[i].anim.Play("LegHit");
			}

			// FIRE BALL
			if(Input.GetKeyDown(player[i].keyCodes.fireBall) && player[i].isGrounded){
				// Fire Ball
				player[i].go.GetComponent<FireBall>().Shoot(player[i].direction);
				player[i].anim.Play("Fireball");
			}

			// UPPERCUT
			if(Input.GetKeyDown(player[i].keyCodes.handHit) && player[i].isCrouching){
				// Uppercut
				player[i].anim.Play("Uppercut");
			}

			// TRIP
			if(Input.GetKeyDown(player[i].keyCodes.legHit) && player[i].isCrouching){
				// Trip
				player[i].anim.Play("Trip");
			}
		}
	}

	void DebugCheckControls () {
		for(int i = 0; i < player.Length; i++){
			if(Input.GetKeyDown(player[i].keyCodes.up)){
				Debug.Log("Button pressed = " + player[i].keyCodes.up + ", User " + (i + 1) + ", Up");
			}
			if(Input.GetKeyDown(player[i].keyCodes.down)){
				Debug.Log("Button pressed = " + player[i].keyCodes.down + ", User " + (i + 1) + ", Down");
			}
			if(Input.GetKeyDown(player[i].keyCodes.left)){
				Debug.Log("Button pressed = " + player[i].keyCodes.left + ", User " + (i + 1) + ", Left");
			}
			if(Input.GetKeyDown(player[i].keyCodes.right)){
				Debug.Log("Button pressed = " + player[i].keyCodes.right + ", User " + (i + 1) + ", Right");
			}
			if(Input.GetKeyDown(player[i].keyCodes.handHit)){
				Debug.Log("Button pressed = " + player[i].keyCodes.handHit + ", User " + (i + 1) + ", Hand Hit");
			}
			if(Input.GetKeyDown(player[i].keyCodes.legHit)){
				Debug.Log("Button pressed = " + player[i].keyCodes.legHit + ", User " + (i + 1) + ", Leg Hit");
			}
			if(Input.GetKeyDown(player[i].keyCodes.fireBall)){
				Debug.Log("Button pressed = " + player[i].keyCodes.fireBall + ", User " + (i + 1) + ", Fire Ball");
			}	
		}
	}

	void SetDirections () {
		Vector3 player1pos = player[0].go.transform.position;
		Vector3 player2pos = player[1].go.transform.position;
		if(player1pos.x > player2pos.x) {
			player[0].direction = false;
			player[1].direction = true;
			player[0].go.transform.rotation = Quaternion.Euler(0,0,0);
			player[1].go.transform.rotation = Quaternion.Euler(0,180,0);
		}
		if(player1pos.x < player2pos.x) {
			player[0].direction = true;
			player[1].direction = false;
			player[0].go.transform.rotation = Quaternion.Euler(0,180,0);
			player[1].go.transform.rotation = Quaternion.Euler(0,0,0);
		}
	}
}
