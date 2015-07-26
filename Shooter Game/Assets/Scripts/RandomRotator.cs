using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour {

	public float tumble = 5f;

	private Rigidbody rigBody;
	void Awake () {
		rigBody = GetComponent<Rigidbody> ();
	}

	void Start () {
		rigBody.angularVelocity = Random.insideUnitSphere * tumble;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
