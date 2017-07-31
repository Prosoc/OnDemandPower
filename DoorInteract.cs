using UnityEngine;
using System.Collections;

public class DoorInteract : MonoBehaviour {

    PlayerController player;
    public int Keys = 1;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            player.inDoorInteract = true;
            player.door = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            player.inDoorInteract = false;
            player.door = null;
        }
    }
}
