using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	public float walkingSpeed = 2;
	public float runningSpeed = 8;
	public float turningSpeed = 60;

	private Vector3 movement;
	private Animator anim;
	private Rigidbody rb;
	private float movementSpeed;

	void Awake () {
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody> ();

		movementSpeed = walkingSpeed;
	}

	void FixedUpdate () {
		float horizontal = Input.GetAxis ("Horizontal") * Time.deltaTime;
		float vertical = Input.GetAxis ("Vertical") * Time.deltaTime;

		if (Input.GetButtonDown("Jump")) {
			anim.SetTrigger("Jump");
		}

		MovementManagement (horizontal, vertical);
	}

	void MovementManagement(float h, float v)
	{

		if (h != 0f || v != 0f) 
		{
			if(Input.GetButton("Run")){
				anim.SetBool ("isRunning", true);
				movementSpeed = runningSpeed;
			} else {
				anim.SetBool ("isRunning", false);
				anim.SetBool ("isWalking", true);
				movementSpeed = walkingSpeed;
			}
		}
		else 
		{
			anim.SetBool ("isRunning", false);
			anim.SetBool ("isWalking", false);
		}

		Moving(h, v);
	}

	void Moving(float h, float v)
	{
		rb.transform.Translate(h * movementSpeed, 0, 0);
		rb.transform.Translate(0, 0, v * movementSpeed);

		if (v < 0) {
			anim.SetFloat ("VelocityX", -h * movementSpeed  * 2);
			anim.SetFloat("VelocityZ",  v * movementSpeed * 5);
		} else {
			anim.SetFloat ("VelocityX", h * movementSpeed);
			anim.SetFloat("VelocityZ",  v * movementSpeed);
		}

	}
}
