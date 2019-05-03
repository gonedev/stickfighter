using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour {

	public int playerAmount = 2;
	public GameObject playerPrefab;
	public Vector3[] positions;
	GameObject playerHolder;

	void Awake () {
		playerHolder = new GameObject("Players");	// Creating PlayerHolder in the scene
		// Creating Players and setting parent
		for(int i = 0; i < playerAmount; i++) {
			CreatePlayer("Player " + (i + 1), positions[i], playerHolder);	
		}
	}

	void CreatePlayer (string name, Vector3 position, GameObject parent) {
		GameObject go = Instantiate(playerPrefab, parent.transform) as GameObject;		// Creating Primitive Cube as Player's Body
		go.name = name;
		go.transform.position = position;
		go.AddComponent<CapsuleCollider>();
		go.GetComponent<CapsuleCollider>().center = new Vector3(0, -1.9f, 0);
		go.GetComponent<CapsuleCollider>().direction = 1;
		go.GetComponent<CapsuleCollider>().height = 7;
		go.GetComponent<CapsuleCollider>().radius = 1.3f;
		go.GetComponent<Transform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
		go.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("AnimatorController") as RuntimeAnimatorController;
		go.AddComponent<Rigidbody>();
		go.AddComponent<Grounded>();							// Adding grounded component with boolean IsGrounded
		go.AddComponent<FireBall>();
		go.GetComponent<Rigidbody>().constraints = 
			RigidbodyConstraints.FreezeRotationX | 
			RigidbodyConstraints.FreezeRotationY | 
			RigidbodyConstraints.FreezeRotationZ | 
			RigidbodyConstraints.FreezePositionZ;				// Setting FreezeRotation XYZ and FreezePositionZ
	}
}
