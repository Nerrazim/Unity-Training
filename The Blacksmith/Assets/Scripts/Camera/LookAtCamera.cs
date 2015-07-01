using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {

	public GameObject target;
	public float rotateSpeed = 5;
	private Vector3 offset;
	
	void Start() {
		offset = target.transform.position - transform.position;
	}
	
	void LateUpdate() {
		Transform targetTransform = target.transform;
		float horizontal = Input.GetAxis ("Mouse X") * rotateSpeed;
		targetTransform.Rotate (0, horizontal, 0);


		float desiredAngle = targetTransform.eulerAngles.y;
		Quaternion rotation = Quaternion.Euler (0, desiredAngle, 0);
		transform.position = targetTransform.position - (rotation * offset);
		transform.LookAt (new Vector3(targetTransform.position.x, targetTransform.position.y + 0.5f, targetTransform.position.z));
	}
}
