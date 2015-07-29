using UnityEngine;
using System.Collections;

public class BackgroundScroller : MonoBehaviour {

	public float scrollSpeed;
	public float tileSizeZ;
	
	private Vector3 startPosition;
	
	void Start ()
	{
		startPosition = transform.position;
	}
	
	void Update ()
	{
		//Moves the background tiles
		float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
		transform.position = startPosition + Vector3.forward * newPosition;
	}
}
