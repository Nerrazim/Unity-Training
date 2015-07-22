using UnityEngine;
using System.Collections;
using SimpleJSON;

public class BuildingScript : MonoBehaviour {

	public SpriteRenderer parentRenderer;

	private string type;
	private string id;
	private string keyString;
	private Vector2 position;
	private SpriteRenderer spriteRenderer;

	private JSONNode node;

	// Use this for initialization
	public void Init (JSONNode nodeData)
	{
		node = nodeData;

		spriteRenderer.sprite = Resources.Load<Sprite>("Images/Buildings/" + node["buildingImage"]);
		keyString = node["keyString"];
		type = node["type"];
		float buildingPosX = node["x"].AsFloat;
		float buildingPosY = node["y"].AsFloat;

		Vector3 buildingTransform = transform.position;
		buildingTransform.x = buildingPosX;
		buildingTransform.y = -buildingPosY;

		transform.position = buildingTransform;
		spriteRenderer.sortingOrder = 2;

		if (type.Equals ("MOA")) {
			spriteRenderer.sortingOrder = 1;
		} else if (type.Equals ("GAT")) {
			spriteRenderer.sortingOrder = 3;
		} else {
			spriteRenderer.sortingOrder = 2;
		}
	}

	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}


	
	void Start()
	{

	}
	
	// Update is called once per frame
	void Update () {

	}
}
