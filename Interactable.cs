using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {

    public bool weapon;
    public bool key;

    public string text;
    public BaseGun gun;
    public PlayerController player;
    SpawnerController s;
    
	// Use this for initialization
	void Start () {
        s = FindObjectOfType<SpawnerController>();
        int rand = 0;
        if (weapon && key)
        {
            rand = Random.Range(0, 1);
        }
        else if (weapon)
        {
            rand = 0;
        }
        else
        {
            rand = 1;
        }
        switch (rand)
        {
            case 0:
                {
                    float weaponRand = Random.Range(0f, 1f);
                    if (weaponRand <= 0.5f)
                    {
                        gun = BaseGun.GetPistol(Mathf.Clamp01(0.01f * s.CurrentWave), s.CurrentWave);
                    }
                    else if (weaponRand <= 0.75f)
                    {
                        gun = BaseGun.GetShotgun(Mathf.Clamp01(0.01f * s.CurrentWave), s.CurrentWave);
                    }
                    else if (weaponRand <= 0.9f)
                    {
                        gun = BaseGun.GetRifle(Mathf.Clamp01(0.01f * s.CurrentWave), s.CurrentWave);                        
                    }
                    else if (weaponRand <= 1f)
                    {
                        gun = BaseGun.GetSniper(Mathf.Clamp01(0.01f * s.CurrentWave), s.CurrentWave);
                    }

                    text = "Damage: " + Mathf.RoundToInt(gun.damage) + "\n" + "Fire rate: " + Mathf.RoundToInt(gun.fireRate) + "\n" + "Range: " + Mathf.RoundToInt(gun.range);
                    break;
                }
            case 1:
                {
                    text = "You get a key";
                    break;
                }
            default:
                {
                    break;
                }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (player == null)
            {
                player = other.GetComponent<PlayerController>();                
            }
            player.inInteractable = true;
            player.interactable = this;
        }
    }

   

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (player != null)
            {
                player.inInteractable = false;
                player.interactable = null;

            }
        }
    }
}
