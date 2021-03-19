using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolMoving : MonoBehaviour
{
	[SerializeField] private float separationCoefficient;
	[SerializeField] private float alignmentCoefficient;
	[SerializeField] private float cohensionCoefficient;
	[SerializeField] private float separationDistance;
	[SerializeField] private float visibleDistance;
	[SerializeField] private float minimumVelocity;
	[SerializeField] private float maximumVelocity;

	[SerializeField] private GameObject tankObject;
	private TankUtility tankScript;

	private int numberOfFish;
	private GameObject[] fishObject;
	private FishMoving[] fishScript;

	private Vector3[] nextVelocity;

	void Start()
	{
		tankScript = tankObject.GetComponent<TankUtility>();

		numberOfFish = transform.childCount;
		fishObject = new GameObject[numberOfFish];
		fishScript = new FishMoving[numberOfFish];
		for (int i = 0; i < numberOfFish; i++)
		{
			fishObject[i] = transform.GetChild(i).gameObject;
			fishScript[i] = fishObject[i].GetComponent<FishMoving>();
		}

		nextVelocity = new Vector3[numberOfFish];
	}

	void Update()
	{
		for (int i = 0; i < numberOfFish; i++)
		{
			nextVelocity[i] = fishScript[i].Velocity;
		}

		// Boids algorithm
		//   http://www.red3d.com/cwr/boids/
		//   https://github.com/beneater/boids
		Separation();
		Alignment();
		Cohension();

		Clamp();

		for (int i = 0; i < numberOfFish; i++)
		{
			fishScript[i].Velocity = nextVelocity[i];

		}
	}

	void Separation()
	{
		for (int i = 0; i < numberOfFish; i++)
		{
			for (int j = 0; j < numberOfFish; j++)
			{
				if (i == j)
				{
					continue;
				}

				Vector3 vij = tankScript.GetDirection(
					fishObject[i].transform.position,
					fishObject[j].transform.position
				);
				float dij = tankScript.GetDistance(
					fishObject[i].transform.position,
					fishObject[j].transform.position
				);
				float c = (separationDistance * separationDistance) / (dij * dij * dij);
				nextVelocity[i] -= separationCoefficient * c * vij / Time.deltaTime;
			}
		}
	}

	void Alignment()
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

				float dij = tankScript.GetDistance(
					fishObject[i].transform.position,
					fishObject[j].transform.position
				);
				if (dij < visibleDistance)
				{
					numberOfNeighbors++;
					averageVelocity += fishScript[j].Velocity;
				}
			}
			if (numberOfNeighbors == 0)
			{
				continue;
			}
			averageVelocity /= numberOfNeighbors;
			nextVelocity[i] += alignmentCoefficient * (averageVelocity - fishScript[i].Velocity);
		}
	}

	void Cohension()
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

				Vector3 vij = tankScript.GetDirection(
					fishObject[i].transform.position,
					fishObject[j].transform.position
				);
				float dij = tankScript.GetDistance(
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

	void Clamp()
	{
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
	}
}
