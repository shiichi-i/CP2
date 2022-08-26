using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MoveCam : MonoBehaviour
{

	/*public float turnSpeed = 3.0f;      // Speed of camera turning when mouse moves in along an axis
	public float rotationX;             // rotation on X axis value
	public float rotationY;             // rotation on Y axis value
	public float panSpeed = 0.5f;       // Speed of the camera when being panned
	private Vector3 mouseOrigin;    // Position of cursor when mouse dragging starts
	*/
	public float zoomSpeed = 1.0f;      // Speed of the camera going back and forth

	
	private bool isPanning;     // Is the camera being panned?
	private bool isRotating;    // Is the camera being rotated?



	void Update()
	{
		/*// Get the right mouse button
		if (Input.GetMouseButtonDown(1))
		{
			// Get mouse origin
			mouseOrigin = Input.mousePosition;
			isRotating = true;
		}

		// Get the middle mouse button
		if (Input.GetMouseButtonDown(2))
		{
			// Get mouse origin
			mouseOrigin = Input.mousePosition;
			isPanning = true;
		}


		// Disable movements on button release
		if (!Input.GetMouseButton(1)) isRotating = false;
		if (!Input.GetMouseButton(2)) isPanning = false;

		// Rotate camera along X and Y axis
		if (isRotating)
		{
			float mouseX = Input.GetAxis("Mouse X") * turnSpeed;
			float mouseY = Input.GetAxis("Mouse Y") * turnSpeed;

			rotationY += mouseX;
			rotationX += -mouseY;

			rotationX = Mathf.Clamp(rotationX, -90, 90);

			transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
		}

		// Move the camera on it's XY plane
		if (isPanning)
		{
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

			Vector3 move = new Vector3(-pos.x * panSpeed, -pos.y * panSpeed, 0);
			transform.Translate(move, Space.Self);
		}*/

		// Move the camera linearly along Z axis
		if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			GetComponentInChildren<Camera>().fieldOfView--;
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			GetComponentInChildren<Camera>().fieldOfView++;
		}
	}
}
