using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

    public float Speed;
    public float LifeTime;
    public float Damage;
    public string type;
    public string Origin;
    // Use this for initialization
    void Start()
    {
        if (Origin != "Enemy")
        {
            transform.localScale = new Vector3(Damage / 200, Damage / 200, Speed / 35);
        }
        else
        {
            transform.localScale = new Vector3(Damage / 100, Damage / 100, Speed / 35);
        }
        
        Destroy(gameObject, LifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy" && Origin != "Enemy")
        {
            EnemyHealth e = other.GetComponent<EnemyHealth>();

            if (e == null)
            {
                e = other.transform.parent.GetComponent<EnemyHealth>();
            }
            switch (type)
            {
                case "Pistol":
                    {
                        e.TakeDamage(Damage);
                        Destroy(gameObject);
                        break;
                    }
                case "Shotgun":
                    {
                        e.TakeDamage(Damage);
                        Destroy(gameObject);
                        break;
                    }
                case "Rifle":
                    {
                        if (Damage <= 4)
                        {
                            e.TakeDamage(Damage);
                            Destroy(gameObject);
                        }
                        else
                        {
                            e.TakeDamage(Damage);
                            Damage /= 4;
                        }
                        break;
                    }
                case "Sniper":
                    {
                        if (Damage <= 4)
                        {
                            e.TakeDamage(Damage);
                            Destroy(gameObject);
                        }
                        else
                        {
                            e.TakeDamage(Damage);
                            Damage /= 2;
                        }
                        break;
                    }
            }
        }
        else if (other.transform.tag == "Ignore")
        {

        }
        else if (other.transform.tag == "RandomLoot")
        {
            other.GetComponent<RandomDropFromBox>().TakeDamage(Damage);
            Destroy(gameObject);
        }
        else if (other.transform.tag == "Player" && Origin == "Enemy")
        {
            other.transform.root.GetComponent<PlayerController>().TakeDamage(Damage);
            Destroy(gameObject);
        }
        else if (other.transform.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
    void OnDestroy()
    {

    }
}
