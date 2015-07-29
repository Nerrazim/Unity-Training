using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour 
{
	public float speed = 20f;

	private Rigidbody rigBody;

	void Awake () {
		rigBody = GetComponent<Rigidbody> ();
	}
	
	void Start () {
		//Moves the object forward with speed
		rigBody.velocity = transform.forward * speed;
	}

	void Update () {
	
	}
}
