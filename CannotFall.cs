using UnityEngine;
using System.Collections;

public class CannotFall : MonoBehaviour {


    public float yLevel = 0.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y <= yLevel)
        {
            transform.position = new Vector3(transform.position.x, yLevel, transform.position.z);
        }
	}
}
