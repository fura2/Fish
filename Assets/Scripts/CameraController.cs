using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] private float cameraSpeed;

	void Start()
	{
	}

	void Update()
	{
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");
		float forwardInput = 0.0f;
		if (Input.GetKey(KeyCode.Q)) { forwardInput = -1.0f; }
		if (Input.GetKey(KeyCode.E)) { forwardInput = 1.0f; }
		transform.Translate(Vector3.right * Time.deltaTime * cameraSpeed * horizontalInput);
		transform.Translate(Vector3.up * Time.deltaTime * cameraSpeed * verticalInput);
		transform.Translate(Vector3.forward * Time.deltaTime * cameraSpeed * forwardInput);
	}
}
