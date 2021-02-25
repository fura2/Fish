using UnityEngine;
using System.Collections;

[System.Serializable]
public class DataClass {
	public int myInt;
	public float myFloat;
}

public class DemoScript : MonoBehaviour {
	public Light myLight;
	public DataClass myClass;

	private Rigidbody myRigidbody;
	
	void Awake() {
		Debug.Log(77);
		myRigidbody = this.GetComponent<Rigidbody>();
	}
	
	void Update() {
		if (Input.GetKeyDown("space")) {
			myLight.enabled = !myLight.enabled;
		}
	}
}
