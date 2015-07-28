using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class TouchMovement : MonoBehaviour , IPointerDownHandler, IDragHandler, IPointerUpHandler {
	public float smoothing;

	public float movementMargin = 2f;

	public Transform origin;
	private Vector2 direction;
	private Vector2 smoothDirection;
	private Vector2 lastPosition;
	private bool touched;
	private int pointerID;

	void Awake() {
		direction = Vector2.zero;
		touched = false;
	}

	public void OnPointerDown (PointerEventData data) {
		if (!touched) {
			touched = true;
			pointerID = data.pointerId;
		}
	}

	public void OnPointerUp (PointerEventData data) {
		if (data.pointerId == pointerID) {
			direction = Vector2.zero;
			touched = false;
		}
	}

	public void OnDrag (PointerEventData data) {
		if (data.pointerId == pointerID) {
			Vector2 currentPosition = data.position;
			Vector3 screenPos = Camera.main.WorldToScreenPoint(origin.position);
			Vector2 playerPosition = new Vector2(screenPos.x, screenPos.y);

			if(Vector2.Distance(lastPosition, currentPosition) > movementMargin) {
				Vector2 directionRaw = currentPosition - playerPosition;
				direction = directionRaw.normalized;
			} else {
				direction = Vector2.zero;
			}

			lastPosition = currentPosition;
		}
	}

	public Vector2 GetDirection() {
		smoothDirection = Vector2.MoveTowards (smoothDirection, direction, smoothing);
		return smoothDirection;
	}
}
