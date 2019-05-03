using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

	GameObject fireBall;
	public float fBSpeed = 10f;
	GameObject fireBallHolder;

	void Start () {
		fireBall = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		fireBall.AddComponent<Rigidbody>();
		fireBall.GetComponent<Collider>().isTrigger = true;
		fireBall.GetComponent<Rigidbody>().useGravity = false;
		fireBall.SetActive(false);
		fireBallHolder = GameObject.Find("Fire Balls");
	}

	public void Shoot (bool dir) {
		int dirint = dir == true ? 1 : -1;
		GameObject go = Instantiate(fireBall, gameObject.transform.position, Quaternion.identity, fireBallHolder.transform) as GameObject;
		go.SetActive(true);
		go.GetComponent<Rigidbody>().velocity = Vector3.right * dirint * fBSpeed;
	}
}
