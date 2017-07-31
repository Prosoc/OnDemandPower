using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    PlayerController player;
    AudioSource source;

    public AudioClip Shotgun;
    public AudioClip Rifle;
    public AudioClip Pistol;
    public AudioClip Sniper;
    public AudioClip Granade;
    public AudioClip PowerUp;
    public AudioClip PickUp;
    public AudioClip PlayerHurt;
    public AudioClip WeaponPickUp;
    public AudioClip DoorOpen;

    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PlayerController>();
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayShootSound()
    {
        switch (player.gunController.currentGun.Name.Split(' ')[1])
        {
            case "Pistol":
                {
                    source.clip = Pistol;
                    break;
                }
            case "Shotgun":
                {
                    source.clip = Shotgun;
                    break;
                }
            case "Rifle":
                {
                    source.clip = Rifle;
                    break;
                }
            case "Sniper":
                {
                    source.clip = Sniper;
                    break;
                }
            default:
                break;
        }
        source.Play();
    }

    public void PlayGranadeSound(){
        if (Granade != null)
        {
            source.clip = Granade;
            source.Play();
        }
    }

    public void PlayPowerUpSound()
    {
        if (PowerUp != null)
        {
            source.clip = PowerUp;
            source.Play();
        }
    }

    public void PlayPickUpSound()
    {
        if (PickUp != null)
        {
            source.clip = PickUp;
            source.Play();
        }
    }

    public void PlayPlayerHurtSound()
    {

    }

    public void PlayWeaponPickUpSound()
    {
        if (WeaponPickUp != null)
        {
            source.clip = WeaponPickUp;
            source.Play();
        }
    }

    public void PlayDoorOpenSound()
    {
        if (DoorOpen != null)
        {
            source.clip = DoorOpen;
            source.Play();
        }
    }
}
