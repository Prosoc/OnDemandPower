using UnityEngine;
using System.Collections;

public class GranadeController : MonoBehaviour {

    public ParticleSystem boom;
    public string Origin;
    public float damage = 10;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    private void OnTriggerEnter(Collider other)
    {
        
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 3, Vector3.up);
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.tag == "RandomLoot" || hit.transform.tag == "Enemy" || hit.transform.tag == "Player" || hit.transform.tag == "Map") { 
                if (hit.transform.tag != "Player" && Origin != "Enemy")
                {
                    hit.transform.GetComponent<Rigidbody>().AddForce((hit.transform.position - transform.position).normalized * 50, ForceMode.Impulse);
                }
                try
                {
                    if (Origin != "Enemy")
                    {
                        hit.transform.root.GetComponent<EnemyHealth>().TakeDamage(damage);
                        if (boom != null)
                        {
                            ParticleSystem go = (ParticleSystem)Instantiate(boom, transform.position, transform.rotation);
                            Destroy(go.gameObject, 0.3f);
                        }
                        Destroy(gameObject);
                    }
                    
                }
                catch (System.Exception)
                {
                }
                try
                {
                    hit.transform.root.GetComponent<RandomDropFromBox>().TakeDamage(damage);
                    if (boom != null)
                    {
                        ParticleSystem go = (ParticleSystem)Instantiate(boom, transform.position, transform.rotation);
                        Destroy(go.gameObject, 0.3f);
                    }
                    Destroy(gameObject);
                }
                catch (System.Exception)
                {
                }

                try
                {
                    if (Origin == "Enemy")
                    {
                        hit.transform.root.GetComponent<PlayerController>().TakeDamage(damage);
                        if (boom != null)
                        {
                            ParticleSystem go = (ParticleSystem)Instantiate(boom, transform.position, transform.rotation);
                            Destroy(go.gameObject, 0.3f);
                        }
                        Destroy(gameObject);
                    }
                }
                catch (System.Exception)
                {
                }
            }
            if (boom != null)
            {
                ParticleSystem go = (ParticleSystem)Instantiate(boom, transform.position, transform.rotation);
                Destroy(go.gameObject, 0.3f);
            }
            Destroy(gameObject);
        }
        

    }
    
}
