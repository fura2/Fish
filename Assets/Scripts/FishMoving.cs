using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMoving : MonoBehaviour
{
	public Vector3 Velocity { get; set; }

	[SerializeField] private GameObject tankObject;
	private Vector3 tankSize;

	void Start()
	{
		Velocity = transform.forward;
		tankSize = tankObject.GetComponent<TankUtility>().Size;
	}

	void LateUpdate()
	{
		transform.LookAt(transform.position + Velocity);
		transform.Translate(Velocity * Time.deltaTime, Space.World);

		// periodic boundary condition
		if (transform.position.x > tankSize.x / 2)
		{
			transform.Translate(-tankSize.x, 0, 0, Space.World);
		}
		if (transform.position.y > tankSize.y / 2)
		{
			transform.Translate(0, -tankSize.y, 0, Space.World);
		}
		if (transform.position.z > tankSize.z / 2)
		{
			transform.Translate(0, 0, -tankSize.z, Space.World);
		}
		if (transform.position.x < -tankSize.x / 2)
		{
			transform.Translate(tankSize.x, 0, 0, Space.World);
		}
		if (transform.position.y < -tankSize.y / 2)
		{
			transform.Translate(0, tankSize.y, 0, Space.World);
		}
		if (transform.position.z < -tankSize.z / 2)
		{
			transform.Translate(0, 0, tankSize.z, Space.World);
		}
	}
}
