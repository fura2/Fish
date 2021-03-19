using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankUtility : MonoBehaviour
{
	public Vector3 Size { get; private set; }

	void Awake()
	{
		Size = transform.localScale;
	}

	void Update()
	{
	}

	// get the direction vector from pos1 to pos2 under the periodic boundary condition
	public Vector3 GetDirection(Vector3 pos1, Vector3 pos2)
	{
		float dx, dy, dz;

		// calculate dx
		if (Math.Abs(pos1.x - pos2.x) < Size.x - Math.Abs(pos1.x - pos2.x))
		{
			dx = pos2.x - pos1.x;
		}
		else if (pos1.x > pos2.x)
		{
			dx = pos2.x - pos1.x + Size.x;
		}
		else
		{
			dx = pos2.x - pos1.x - Size.x;
		}

		// calculate dy
		if (Math.Abs(pos1.y - pos2.y) < Size.y - Math.Abs(pos1.y - pos2.y))
		{
			dy = pos2.y - pos1.y;
		}
		else if (pos1.y > pos2.y)
		{
			dy = pos2.y - pos1.y + Size.y;
		}
		else
		{
			dy = pos2.y - pos1.y - Size.y;
		}

		// calculate dz
		if (Math.Abs(pos1.z - pos2.z) < Size.z - Math.Abs(pos1.z - pos2.z))
		{
			dz = pos2.z - pos1.z;
		}
		else if (pos1.z > pos2.z)
		{
			dz = pos2.z - pos1.z + Size.z;
		}
		else
		{
			dz = pos2.z - pos1.z - Size.z;
		}

		return new Vector3(dx, dy, dz);
	}

	// get the distance from pos1 to pos2 under the periodic boundary condition
	public float GetDistance(Vector3 pos1, Vector3 pos2)
	{
		return GetDirection(pos1, pos2).magnitude;
	}
}
