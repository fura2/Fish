using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolMoving : MonoBehaviour
{
	public float separationCoefficient = 1.0f;
	public float alignmentCoefficient = 0.01f;
	public float cohensionCoefficient = 0.001f;
	public float separationDistance = 5.0f;
	public float visibleDistance = 10.0f;
	public float minimumVelocity = 10.0f;
	public float maximumVelocity = 20.0f;

	public GameObject tankObject;

	private int numberOfFish;
	private GameObject[] fishObject;
	private FishMoving[] fishScript;

	void Start()
	{
		numberOfFish = transform.childCount;
		fishObject = new GameObject[numberOfFish];
		fishScript = new FishMoving[numberOfFish];
		for (int i = 0; i < numberOfFish; i++)
		{
			fishObject[i] = transform.GetChild(i).gameObject;
			fishScript[i] = fishObject[i].GetComponent<FishMoving>();
		}
	}

	void Update()
	{
		Vector3[] nextVelocity = new Vector3[numberOfFish];
		for (int i = 0; i < numberOfFish; i++)
		{
			nextVelocity[i] = fishScript[i].velocity;
		}

		// Boids
		//   http://www.red3d.com/cwr/boids/
		//   https://github.com/beneater/boids
		Separation(nextVelocity);
		Alignment(nextVelocity);
		Cohension(nextVelocity);

		for (int i = 0; i < numberOfFish; i++)
		{
			float magnitude = nextVelocity[i].magnitude;
			if (magnitude < minimumVelocity)
			{
				nextVelocity[i] *= minimumVelocity / magnitude;
			}
			else if (magnitude > maximumVelocity)
			{
				nextVelocity[i] *= maximumVelocity / magnitude;
			}
		}

		for (int i = 0; i < numberOfFish; i++)
		{
			fishScript[i].velocity = nextVelocity[i];
		}
	}

	void Separation(Vector3[] nextVelocity)
	{
		for (int i = 0; i < numberOfFish; i++)
		{
			for (int j = 0; j < numberOfFish; j++)
			{
				if (i == j)
				{
					continue;
				}

				Vector3 vij = tankObject.GetComponent<TankUtility>().GetDirection(
					fishObject[i].transform.position,
					fishObject[j].transform.position
				);
				float dij = tankObject.GetComponent<TankUtility>().GetDistance(
					fishObject[i].transform.position,
					fishObject[j].transform.position
				);
				float c = (separationDistance * separationDistance) / (dij * dij * dij);
				nextVelocity[i] -= separationCoefficient * c * vij / Time.deltaTime;
			}
		}
	}

	void Alignment(Vector3[] nextVelocity)
	{
		for (int i = 0; i < numberOfFish; i++)
		{
			int numberOfNeighbors = 0;
			Vector3 averageVelocity = Vector3.zero;

			for (int j = 0; j < numberOfFish; j++)
			{
				if (i == j)
				{
					continue;
				}

				float dij = tankObject.GetComponent<TankUtility>().GetDistance(
					fishObject[i].transform.position,
					fishObject[j].transform.position
				);
				if (dij < visibleDistance)
				{
					numberOfNeighbors++;
					averageVelocity += fishScript[j].velocity;
				}
			}
			if (numberOfNeighbors == 0)
			{
				continue;
			}
			averageVelocity /= numberOfNeighbors;
			nextVelocity[i] += alignmentCoefficient * (averageVelocity - fishScript[i].velocity);
		}
	}

	void Cohension(Vector3[] nextVelocity)
	{
		for (int i = 0; i < numberOfFish; i++)
		{
			int numberOfNeighbors = 0;
			Vector3 gap = Vector3.zero;

			for (int j = 0; j < numberOfFish; j++)
			{
				if (i == j)
				{
					continue;
				}

				Vector3 vij = tankObject.GetComponent<TankUtility>().GetDirection(
					fishObject[i].transform.position,
					fishObject[j].transform.position
				);
				float dij = tankObject.GetComponent<TankUtility>().GetDistance(
					fishObject[i].transform.position,
					fishObject[j].transform.position
				);
				if (dij < visibleDistance)
				{
					numberOfNeighbors++;
					gap += vij;
				}
			}
			if (numberOfNeighbors == 0)
			{
				continue;
			}
			gap /= numberOfNeighbors;
			nextVelocity[i] += cohensionCoefficient * gap / Time.deltaTime;
		}
	}
}
