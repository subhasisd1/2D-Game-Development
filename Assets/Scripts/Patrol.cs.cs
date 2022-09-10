using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour {

	public float speed;
	public float distance;
	// public Rigidbody2D rigidbody2D;
	//public Transform groundDetection;

	// Update is called once per frame
	void Update()
	{
		transform.Translate(Vector2.right * speed * Time.deltaTime);
	}


	private void OnTriggerEnter2D(Collider2D other)
	{
		transform.eulerAngles = new Vector3(0, -180, 0);
		if (other.tag == "wall")
		{
			Debug.Log("Touched Wall!!!");
		}
		else
		{
		}

	}

}
