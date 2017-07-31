using UnityEngine;
using System.Collections;

public class lookAtCamCanvas : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(Camera.main.transform.position);
        //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.x - 90, transform.rotation.eulerAngles.z);
	}
}
