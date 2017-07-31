using UnityEngine;
using System.Collections;

[System.Serializable]
public class GunController : MonoBehaviour
{

    public bool isShooting;

    public Transform c_end;
    public BaseGun currentGun;
    public GameObject Bullet;
    public PlayerController player;


    public GameObject DropBox;

    public GameObject Rifle_GO;
    public GameObject Shotgun_GO;
    public GameObject Sniper_GO;
    public GameObject Pistol_GO;

    public GameObject Rifle_End;
    public GameObject Shotgun_End;
    public GameObject Sniper_End;
    public GameObject Pistol_End;

    public ParticleSystem Rifle_MuzleFlash;
    public ParticleSystem Shotgun_MuzleFlash;
    public ParticleSystem Sniper_MuzleFlash;
    public ParticleSystem Pistol_MuzleFlash;
    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        currentGun = BaseGun.GetPistol(0.4f, 1);
        currentGun.controller = this;
    }

    // Update is called once per frame
    void Update()
    {
        currentGun.shootDelay = 60 / currentGun.fireRate;

        switch (currentGun.Name.Split(' ')[1])
        {
            case "Pistol":
                {
                    c_end = Pistol_End.transform;
                    Pistol_GO.SetActive(true);
                    Shotgun_GO.SetActive(false);
                    Rifle_GO.SetActive(false);
                    Sniper_GO.SetActive(false);
                    break;
                }
            case "Shotgun":
                {
                    c_end = Shotgun_End.transform;
                    Pistol_GO.SetActive(false);
                    Shotgun_GO.SetActive(true);
                    Rifle_GO.SetActive(false);
                    Sniper_GO.SetActive(false);
                    break;
                }
            case "Rifle":
                {
                    c_end = Rifle_End.transform;
                    Pistol_GO.SetActive(false);
                    Shotgun_GO.SetActive(false);
                    Rifle_GO.SetActive(true);
                    Sniper_GO.SetActive(false);
                    break;
                }
            case "Sniper":
                {
                    c_end = Sniper_End.transform;
                    Pistol_GO.SetActive(false);
                    Shotgun_GO.SetActive(false);
                    Rifle_GO.SetActive(false);
                    Sniper_GO.SetActive(true);
                    break;
                }
        }

        if (isShooting)
        {
            currentGun.currentShootDelay = Mathf.Clamp(currentGun.currentShootDelay - Time.deltaTime, 0, currentGun.shootDelay);
            if (currentGun.currentShootDelay <= 0)
            {
                Shoot(currentGun.spreadCount);
                currentGun.currentShootDelay = currentGun.shootDelay;
            }
        }
        else
        {
            currentGun.currentShootDelay = Mathf.Clamp(currentGun.currentShootDelay - Time.deltaTime, 0, currentGun.shootDelay);
        }


    }

    public void Shoot(int spreadCount)
    {

        if (c_end != null && Bullet != null)
        {
            if (currentGun.currentMagazine > 0 && !currentGun.isRealoading)
            {
                currentGun.currentMagazine--;
                currentGun.isRealoading = false;
                switch (spreadCount)
                {
                    case 1:
                        {
                            Vector3 rotation = transform.rotation.eulerAngles;
                            GameObject newBullet = (GameObject)Instantiate(Bullet, c_end.position, Quaternion.Euler(rotation));
                            BulletController bull = newBullet.GetComponent<BulletController>();
                            bull.Speed = currentGun.velocity;
                            bull.LifeTime = currentGun.range / currentGun.velocity;
                            bull.Damage = currentGun.damage;
                            bull.type = currentGun.Name.Split(' ')[1];
                            break;
                        }
                    case 2:
                        {
                            Vector3 rotation = transform.rotation.eulerAngles;
                            rotation.y += -1 * 5;
                            GameObject newBullet = (GameObject)Instantiate(Bullet, c_end.position, Quaternion.Euler(rotation));
                            BulletController bull = newBullet.GetComponent<BulletController>();
                            bull.Speed = currentGun.velocity;
                            bull.LifeTime = currentGun.range / currentGun.velocity;
                            bull.Damage = currentGun.damage / 2;
                            bull.type = currentGun.Name.Split(' ')[1];

                            rotation = transform.rotation.eulerAngles;
                            rotation.y += 1 * 5;
                            newBullet = (GameObject)Instantiate(Bullet, c_end.position, Quaternion.Euler(rotation));
                            bull = newBullet.GetComponent<BulletController>();
                            bull.Speed = currentGun.velocity;
                            bull.LifeTime = currentGun.range / currentGun.velocity;
                            bull.Damage = currentGun.damage / 2;
                            bull.type = currentGun.Name.Split(' ')[1];
                            break;
                        }
                    case 3:
                        {
                            Vector3 rotation = transform.rotation.eulerAngles;
                            rotation.y += -1 * 10;
                            GameObject newBullet = (GameObject)Instantiate(Bullet, c_end.position, Quaternion.Euler(rotation));
                            BulletController bull = newBullet.GetComponent<BulletController>();
                            bull.Speed = currentGun.velocity;
                            bull.LifeTime = currentGun.range / currentGun.velocity;
                            bull.Damage = currentGun.damage / 3;
                            bull.type = currentGun.Name.Split(' ')[1];

                            rotation = transform.rotation.eulerAngles;
                            newBullet = (GameObject)Instantiate(Bullet, c_end.position, Quaternion.Euler(rotation));
                            bull = newBullet.GetComponent<BulletController>();
                            bull.Speed = currentGun.velocity;
                            bull.LifeTime = currentGun.range / currentGun.velocity;
                            bull.Damage = currentGun.damage / 3;
                            bull.type = currentGun.Name.Split(' ')[1];

                            rotation = transform.rotation.eulerAngles;
                            rotation.y += 1 * 10;
                            newBullet = (GameObject)Instantiate(Bullet, c_end.position, Quaternion.Euler(rotation));
                            bull = newBullet.GetComponent<BulletController>();
                            bull.Speed = currentGun.velocity;
                            bull.LifeTime = currentGun.range / currentGun.velocity;
                            bull.Damage = currentGun.damage / 3;
                            bull.type = currentGun.Name.Split(' ')[1];
                            break;
                        }
                    case 4:
                        {
                            Vector3 rotation = transform.rotation.eulerAngles;
                            rotation.y += -2 * 5;
                            GameObject newBullet = (GameObject)Instantiate(Bullet, c_end.position, Quaternion.Euler(rotation));
                            BulletController bull = newBullet.GetComponent<BulletController>();
                            bull.Speed = currentGun.velocity;
                            bull.LifeTime = currentGun.range / currentGun.velocity;
                            bull.Damage = currentGun.damage / 4;
                            bull.type = currentGun.Name.Split(' ')[1];

                            rotation = transform.rotation.eulerAngles;
                            rotation.y += -1 * 5;
                            newBullet = (GameObject)Instantiate(Bullet, c_end.position, Quaternion.Euler(rotation));
                            bull = newBullet.GetComponent<BulletController>();
                            bull.Speed = currentGun.velocity;
                            bull.LifeTime = currentGun.range / currentGun.velocity;
                            bull.Damage = currentGun.damage / 4;
                            bull.type = currentGun.Name.Split(' ')[1];

                            rotation = transform.rotation.eulerAngles;
                            rotation.y += 1 * 5;
                            newBullet = (GameObject)Instantiate(Bullet, c_end.position, Quaternion.Euler(rotation));
                            bull = newBullet.GetComponent<BulletController>();
                            bull.Speed = currentGun.velocity;
                            bull.LifeTime = currentGun.range / currentGun.velocity;
                            bull.Damage = currentGun.damage / 4;
                            bull.type = currentGun.Name.Split(' ')[1];

                            rotation = transform.rotation.eulerAngles;
                            rotation.y += 2 * 5;
                            newBullet = (GameObject)Instantiate(Bullet, c_end.position, Quaternion.Euler(rotation));
                            bull = newBullet.GetComponent<BulletController>();
                            bull.Speed = currentGun.velocity;
                            bull.LifeTime = currentGun.range / currentGun.velocity;
                            bull.Damage = currentGun.damage / 4;
                            bull.type = currentGun.Name.Split(' ')[1];
                            break;
                        }

                    case 5:
                        {
                            Vector3 rotation = transform.rotation.eulerAngles;
                            rotation.y += -2 * 10;
                            GameObject newBullet = (GameObject)Instantiate(Bullet, c_end.position, Quaternion.Euler(rotation));
                            BulletController bull = newBullet.GetComponent<BulletController>();
                            bull.Speed = currentGun.velocity;
                            bull.LifeTime = currentGun.range / currentGun.velocity;
                            bull.Damage = currentGun.damage / 5;
                            bull.type = currentGun.Name.Split(' ')[1];

                            rotation = transform.rotation.eulerAngles;
                            rotation.y += -1 * 10;
                            newBullet = (GameObject)Instantiate(Bullet, c_end.position, Quaternion.Euler(rotation));
                            bull = newBullet.GetComponent<BulletController>();
                            bull.Speed = currentGun.velocity;
                            bull.LifeTime = currentGun.range / currentGun.velocity;
                            bull.Damage = currentGun.damage / 5;
                            bull.type = currentGun.Name.Split(' ')[1];

                            rotation = transform.rotation.eulerAngles;
                            newBullet = (GameObject)Instantiate(Bullet, c_end.position, Quaternion.Euler(rotation));
                            bull = newBullet.GetComponent<BulletController>();
                            bull.Speed = currentGun.velocity;
                            bull.LifeTime = currentGun.range / currentGun.velocity;
                            bull.Damage = currentGun.damage / 5;
                            bull.type = currentGun.Name.Split(' ')[1];

                            rotation = transform.rotation.eulerAngles;
                            rotation.y += 1 * 10;
                            newBullet = (GameObject)Instantiate(Bullet, c_end.position, Quaternion.Euler(rotation));
                            bull = newBullet.GetComponent<BulletController>();
                            bull.Speed = currentGun.velocity;
                            bull.LifeTime = currentGun.range / currentGun.velocity;
                            bull.Damage = currentGun.damage / 5;
                            bull.type = currentGun.Name.Split(' ')[1];

                            rotation = transform.rotation.eulerAngles;
                            rotation.y += 2 * 10;
                            newBullet = (GameObject)Instantiate(Bullet, c_end.position, Quaternion.Euler(rotation));
                            bull = newBullet.GetComponent<BulletController>();
                            bull.Speed = currentGun.velocity;
                            bull.LifeTime = currentGun.range / currentGun.velocity;
                            bull.Damage = currentGun.damage / 5;
                            bull.type = currentGun.Name.Split(' ')[1];
                            break;
                        }
                    default:
                        {
                            Vector3 rotation = transform.rotation.eulerAngles;
                            GameObject newBullet = (GameObject)Instantiate(Bullet, c_end.position, Quaternion.Euler(rotation));
                            BulletController bull = newBullet.GetComponent<BulletController>();
                            bull.Speed = currentGun.velocity;
                            bull.LifeTime = currentGun.range / currentGun.velocity;
                            bull.Damage = currentGun.damage;
                            bull.type = currentGun.Name.Split(' ')[1];
                            break;
                        }

                }
                switch (currentGun.Name.Split(' ')[1])
                {
                    case "Pistol":
                        {
                            Pistol_MuzleFlash.Play();
                            break;
                        }
                    case "Shotgun":
                        {
                            Shotgun_MuzleFlash.Play();
                            break;
                        }
                    case "Rifle":
                        {
                            Rifle_MuzleFlash.Play();
                            break;
                        }
                    case "Sniper":
                        {
                            Sniper_MuzleFlash.Play();
                            break;
                        }
                }

                if (player.soundManager != null)
                {
                    player.soundManager.PlayShootSound();
                }
            }

        }
    }
    public void Reload()
    {
        StartCoroutine(currentGun.Reload());
    }
}
