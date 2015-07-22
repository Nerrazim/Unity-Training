using UnityEngine;
using System.Collections;

public class VillageCamera : MonoBehaviour {
	public float ScrollSpeed = 10f;
	public float ScrollEdge = 0.01f;

	public GameObject parent;

	private SpriteRenderer parentRenderer;
	
	void Start()
	{
		parentRenderer = parent.GetComponent<SpriteRenderer> ();
	}

	void Update ()
	{
		if (Input.mousePosition.x >= Screen.width * (1 - ScrollEdge))
		{
			transform.Translate(Vector3.right * Time.deltaTime * ScrollSpeed, Space.World);
		}
		else if (Input.mousePosition.x <= Screen.width * ScrollEdge)
		{
			transform.Translate(Vector3.right * Time.deltaTime * -ScrollSpeed, Space.World);
		}
		
		if (Input.mousePosition.y >= Screen.height * (1 - ScrollEdge))
		{
			transform.Translate(Vector3.up * Time.deltaTime * ScrollSpeed, Space.World);
		}
		else if (Input.mousePosition.y <= Screen.height * ScrollEdge)
		{ 
			transform.Translate(Vector3.up * Time.deltaTime * -ScrollSpeed, Space.World);
		}

		Vector3 clampPosition = transform.position;
		Vector2 cameraMin = Camera.main.BoundsMin();
		Vector2 cameraMax = Camera.main.BoundsMax();

		if (cameraMin.x < parentRenderer.bounds.min.x) {
			clampPosition.x = parentRenderer.bounds.min.x + Camera.main.Extents().x;
		} else if (cameraMax.x > parentRenderer.bounds.max.x) {
			clampPosition.x = parentRenderer.bounds.max.x - Camera.main.Extents().x;
		}

		if (cameraMin.y < parentRenderer.bounds.min.y) {
			clampPosition.y = parentRenderer.bounds.min.y  + Camera.main.Extents().y;
		} else if (cameraMax.y > parentRenderer.bounds.max.y) {
			clampPosition.y = parentRenderer.bounds.max.y - Camera.main.Extents().y;
		}

		transform.position = clampPosition;
	}
}
