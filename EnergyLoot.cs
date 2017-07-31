using UnityEngine;
using System.Collections;

public class EnergyLoot : MonoBehaviour {

    public float powerFill = 50;
    public ParticleSystem effect;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if (other.transform.GetComponent<PlayerController>() != null)
            {
                other.transform.GetComponent<PlayerController>().CurrentPower += powerFill;
            }
            if (effect != null)
            {
               
                ParticleSystem go = (ParticleSystem)Instantiate(effect, transform.position, transform.rotation);
                Destroy(go.gameObject, 0.3f);
            }
            FindObjectOfType<SoundManager>().PlayPowerUpSound();
            Destroy(gameObject);
        }
    }
}
