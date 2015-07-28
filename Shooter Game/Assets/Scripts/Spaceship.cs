using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary 
{
	
	public float xMin, xMax, zMin, zMax;
	
}

public class Spaceship : MonoBehaviour {

	public float speed = 10f;
	public float fireRate = 0.5f;
	public GameObject bolt;

	public Boundary boundary;

	public GameObject explosion;
	protected Rigidbody rigBody;
	protected AudioSource audioSource;
	protected float nextFire = 0.0f;
}
