using UnityEngine;
using System.Collections;

public class ShopController : MonoBehaviour {

    public bool used;

    public bool BuyArmor;

    public bool BuyPower;

    public bool BuyWeapon;
    public BaseGun g;
    public float value;
    public int cost;

    public string text;

    SpawnerController s;

	// Use this for initialization
	void Start () {
        s = FindObjectOfType<SpawnerController>();
        if (BuyArmor)
        {
            text = value + " Armor for " + cost + " coins.";
        }
        else if(BuyPower)
        {
            text = value + " Energy for " + cost + " coins.";
        }
        else
        {
            genWeapon();
        }
	
	}

    public void genWeapon()
    {

        float weaponRand = Random.Range(0f, 1f);
        BaseGun gun = new BaseGun();
        if (weaponRand <= 0.25f)
        {
            gun = BaseGun.GetPistol(Mathf.Clamp01(value), s.CurrentWave);
        }
        else if (weaponRand <= 0.5f)
        {
            gun = BaseGun.GetRifle(Mathf.Clamp01(value), s.CurrentWave);
        }
        else if (weaponRand <= 0.75f)
        {
            gun = BaseGun.GetShotgun(Mathf.Clamp01(value), s.CurrentWave);
        }
        else if (weaponRand <= 1f)
        {
            gun = BaseGun.GetSniper(Mathf.Clamp01(value), s.CurrentWave);
        }
        g = gun;
        text = "Weapon for " + cost + " coins.\n" + "Damage: " + Mathf.RoundToInt(gun.damage) + "\n" + "Fire rate: " + Mathf.RoundToInt(gun.fireRate) + "\n" + "Range: " + Mathf.RoundToInt(gun.range);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            other.GetComponent<PlayerController>().inShop = true;
            other.GetComponent<PlayerController>().Shop = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            other.GetComponent<PlayerController>().inShop = false;
            other.GetComponent<PlayerController>().Shop = null;
        }
    }
}


