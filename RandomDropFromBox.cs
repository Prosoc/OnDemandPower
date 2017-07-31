using UnityEngine;
using System.Collections.Generic;

public class RandomDropFromBox : MonoBehaviour {

    public float HP = 100;
    public bool weapon;
    public bool key;
    public GameObject randomBox;
    bool isDestroyed = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (HP <= 0)
        {
            Die();
        }
	}

    public void TakeDamage(float amount)
    {
        HP -= amount;
    }

    public void Die()
    {
        if (!isDestroyed)
        {
            GameObject box = (GameObject)Instantiate(randomBox, transform.position, transform.rotation);
            box.GetComponent<Interactable>().weapon = weapon;
            box.GetComponent<Interactable>().key = key;
            isDestroyed = true;
            Destroy(gameObject);
        }

    }
}
