using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

	private Image bgimg;
	private Image joystickimg;
	private Vector3 inputVector;

	// when it is a big number than the cursor will be more in the center
	// a small number - cursor will be very far from center
	public int cursorInTheCircle_k = 3;

	// Use this for initialization
	private void Start () {
		bgimg = GetComponent<Image> ();
		joystickimg = transform.GetChild (0).GetComponent<Image> ();
	}

	public virtual void OnDrag(PointerEventData ped)
	{
		Vector2 pos;
		if(RectTransformUtility.ScreenPointToLocalPointInRectangle(this.bgimg.rectTransform,
			ped.position,
			ped.pressEventCamera,
			out pos))
		{
			pos.x = (pos.x / bgimg.rectTransform.sizeDelta.x);
			pos.y = (pos.y / bgimg.rectTransform.sizeDelta.y);

			inputVector = new Vector3 (pos.x*2, 0, pos.y*2);
			inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

			joystickimg.rectTransform.anchoredPosition = new Vector3 (inputVector.x * (bgimg.rectTransform.sizeDelta.x / this.cursorInTheCircle_k), inputVector.z * (bgimg.rectTransform.sizeDelta.y / this.cursorInTheCircle_k));
		}
	}

	public virtual void OnPointerDown(PointerEventData ped)
	{
		this.OnDrag (ped);	
	}

	public virtual void OnPointerUp(PointerEventData ped)
	{
		inputVector = Vector3.zero;
		joystickimg.rectTransform.anchoredPosition = Vector3.zero;
	}

	public float GetAxis(string Axis)
	{
		if (Axis == "Horizontal" && inputVector.x != 0) 
			return inputVector.x;
		else if (Axis == "Vertical" && inputVector.z != 0)
			return inputVector.z;
		else
			return Input.GetAxis (Axis);
	}
}