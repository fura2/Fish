using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankUtility : MonoBehaviour
{
	public Vector3 length { get; } = new Vector3(60, 60, 60);

	void Start()
	{

	}

	void Update()
	{

	}

	// direction vector from pos1 to pos2 under the periodic boundary condition
	public Vector3 GetDirection(Vector3 pos1, Vector3 pos2)
	{
		float vx, vy, vz;
		// x
		if (Math.Abs(pos1.x - pos2.x) < length.x - Math.Abs(pos1.x - pos2.x))
		{
			vx = pos2.x - pos1.x;
		}
		else if (pos1.x > pos2.x)
		{
			vx = pos2.x - pos1.x + length.x;
		}
		else
		{
			vx = pos2.x - pos1.x - length.x;
		}
		// y
		if (Math.Abs(pos1.y - pos2.y) < length.y - Math.Abs(pos1.y - pos2.y))
		{
			vy = pos2.y - pos1.y;
		}
		else if (pos1.y > pos2.y)
		{
			vy = pos2.y - pos1.y + length.y;
		}
		else
		{
			vy = pos2.y - pos1.y - length.y;
		}
		// z
		if (Math.Abs(pos1.z - pos2.z) < length.z - Math.Abs(pos1.z - pos2.z))
		{
			vz = pos2.z - pos1.z;
		}
		else if (pos1.z > pos2.z)
		{
			vz = pos2.z - pos1.z + length.z;
		}
		else
		{
			vz = pos2.z - pos1.z - length.z;
		}
		return new Vector3(vx, vy, vz);
	}

	public float GetDistance(Vector3 pos1, Vector3 pos2)
	{
		return GetDirection(pos1, pos2).magnitude;
	}
}
