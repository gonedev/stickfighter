using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour {

	public bool isGrounded;
 
	void OnCollisionStay (Collision collisionInfo) {
	     isGrounded = true;
	}
	
	void OnCollisionExit (Collision collisionInfo) {
	     isGrounded = false;
	}
}
