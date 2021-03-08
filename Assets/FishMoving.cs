using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMoving : MonoBehaviour
{
	public Vector3 velocity;
	public GameObject tankObject;

	void Start()
	{
		velocity = Vector3.forward * 5.0f;
	}

	void LateUpdate()
	{
		transform.LookAt(transform.position + velocity);
		transform.Translate(Vector3.forward * velocity.magnitude * Time.deltaTime);

		// periodic boundary condition
		Vector3 length = tankObject.GetComponent<TankUtility>().length;
		if (transform.position.x > length.x / 2)
		{
			transform.Translate(-length.x, 0, 0, Space.World);
		}
		if (transform.position.y > length.y / 2)
		{
			transform.Translate(0, -length.y, 0, Space.World);
		}
		if (transform.position.z > length.z / 2)
		{
			transform.Translate(0, 0, -length.z, Space.World);
		}
		if (transform.position.x < -length.x / 2)
		{
			transform.Translate(length.x, 0, 0, Space.World);
		}
		if (transform.position.y < -length.y / 2)
		{
			transform.Translate(0, length.y, 0, Space.World);
		}
		if (transform.position.z < -length.z / 2)
		{
			transform.Translate(0, 0, length.z, Space.World);
		}
	}
}
