using UnityEngine;
using System.Collections;

public class Loot : MonoBehaviour
{
    public enum LootType
    {
        key,
        granade,
        coin,
        armor,
        random
    }
    public LootType type;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if (other.transform.GetComponent<PlayerController>() != null)
            {
                switch (type)
                {
                    case LootType.key:
                        {
                            other.transform.GetComponent<PlayerController>().Keys++;
                            break;
                        }
                    case LootType.granade:
                        {
                            other.transform.GetComponent<PlayerController>().Granades++;
                            break;
                        }
                    case LootType.coin:
                        {
                            other.transform.GetComponent<PlayerController>().Coins++;
                            break;
                        }
                    case LootType.armor:
                        {
                            other.transform.GetComponent<PlayerController>().CurrentHealth += 20;
                            break;
                        }
                    case LootType.random:
                        {
                            switch (Random.Range(1, 3))
                            {
                                case 0:
                                    {
                                        other.transform.GetComponent<PlayerController>().Keys++;
                                        break;
                                    }
                                case 1:
                                    {
                                        other.transform.GetComponent<PlayerController>().Granades++;
                                        break;
                                    }
                                case 2:
                                    {
                                        other.transform.GetComponent<PlayerController>().Coins++;
                                        other.transform.GetComponent<PlayerController>().CoinsAll++;
                                        break;
                                    }
                                case 3:
                                    {
                                        other.transform.GetComponent<PlayerController>().CurrentHealth += 20;
                                        break;
                                    }
                            }
                            break;
                        }
                    default:                       
                        break;
                }
            }
            FindObjectOfType<SoundManager>().PlayPickUpSound();
            Destroy(gameObject);
        }
    }
}
