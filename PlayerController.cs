using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    bool Paused = false;

    public float PlayerSpeed;
    public float MaxHealth = 100;
    public float CurrentHealth;

    public float MaxPower = 1000;
    public float CurrentPower;
    //int sceneID = 0;

    public int Keys = 0;
    public int Granades = 1;
    public int Coins = 0;
    public int CoinsAll = 0;
    Rigidbody P_Rigidbody;

    public Image HealthFill;
    public Image PowerFill;

    Vector3 input;
    Vector3 velocity;
    Vector3 lookPoint;

    Camera cam;


    public GameObject Granade;
    public GunController gunController;
    public SoundManager soundManager;
    public LayerMask ignoreBullet = new LayerMask();

    public bool inInteractable;
    public Interactable interactable;

    public bool inShop;
    public ShopController Shop;

    public Text Center;
    public Canvas canvas;
    public Canvas pause;

    public Text Keys_T;
    public Text Granades_T;
    public Text Coins_T;

    public bool inDoorInteract = false;
    public DoorInteract door;

    public GameObject Tutorial;

    public GameObject DeathScreen;
    public Text info;
    SpawnerController s;

    public AudioSource music;
    void Start()
    {
        Keys = 0;
        Granades = 1;
        Coins = 0;
        CoinsAll = 0;
        Time.timeScale = 1;
        s = FindObjectOfType<SpawnerController>();
        P_Rigidbody = GetComponent<Rigidbody>();
        cam = Camera.main;
        CurrentHealth = MaxHealth;
        CurrentPower = MaxPower;
        soundManager = GetComponentInChildren<SoundManager>();
        canvas.gameObject.SetActive(true);
        Destroy(Tutorial, 20);
        DeathScreen.SetActive(false);
    }

    void Update()
    {


        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        velocity = input * PlayerSpeed;
        if (input.magnitude > 0)
        {
            CurrentPower -= PlayerSpeed * 2 * Time.deltaTime;
        }
        Ray CamRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(CamRay, out hit, ignoreBullet))
        {
            lookPoint = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        }

        if (Input.GetMouseButton(0))
        {
            gunController.isShooting = true;
        }
        else
        {
            gunController.isShooting = false;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {

            if (Granades > 0)
            {
                GameObject granade = (GameObject)Instantiate(Granade, gunController.c_end.position, transform.rotation);
                granade.GetComponent<Rigidbody>().AddForce(transform.forward * Mathf.Clamp(Vector3.Distance(lookPoint, transform.position), 0, 20), ForceMode.Impulse);
                Destroy(granade, 5f);
                Granades--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused)
            {
                pause.gameObject.SetActive(!Paused);
                Time.timeScale = 1;
            }
            else
            {
                pause.gameObject.SetActive(!Paused);
                Time.timeScale = 0;
            }
            Paused = !Paused;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            music.enabled = !music.enabled;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            gunController.Reload();
        }
        if (Input.GetKeyDown(KeyCode.F) && inInteractable)
        {
            BaseGun g = gunController.currentGun;
            gunController.currentGun = interactable.gun;
            gunController.currentGun.controller = gunController;
            soundManager.PlayWeaponPickUpSound();
            interactable.gun = g;
            interactable.text = "Damage: " + Mathf.RoundToInt(g.damage) + "\n" + "Fire rate: " + Mathf.RoundToInt(g.fireRate) + "\n" + "Range: " + Mathf.RoundToInt(g.range);
        }
        if (Input.GetKeyDown(KeyCode.F) && inDoorInteract)
        {
            if (Keys > door.Keys)
            {
                soundManager.PlayDoorOpenSound();
                Destroy(door.gameObject);
                Keys -= door.Keys;
                inDoorInteract = false;
                door = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && inShop)
        {
            if (Coins > Shop.cost && !Shop.used)
            {
                if (Shop.BuyArmor)
                {
                    CurrentHealth += Shop.value;
                    Shop.cost++;
                }
                else if (Shop.BuyPower)
                {
                    CurrentPower += Shop.value;
                    Shop.cost++;
                }
                else
                {
                    gunController.currentGun = Shop.g;
                    gunController.currentGun.controller = gunController;
                    Shop.genWeapon();
                }
                Coins -= Shop.cost;
            }
        }

        if (inInteractable)
        {
            Center.color = Color.black;
            Center.text = "Press F To Take";
        }
        else if (inDoorInteract)
        {
            Center.color = Color.black;
            Center.text = "Press F To Open Door\nCost: " + door.Keys + " key(s)";
        }
        else if (inShop)
        {
            Center.color = Color.black;
            Center.text = "Press F To Buy";
        }
        else
        {
            Center.color = new Color(0, 0, 0, 0);
        }

        CurrentPower = Mathf.Clamp(CurrentPower, 0, MaxPower);
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        if (CurrentHealth <= 0)
        {
            Die();
        }
        PowerFill.fillAmount = CurrentPower / MaxPower;
        HealthFill.fillAmount = CurrentHealth / MaxHealth;

        Keys_T.text = Keys.ToString();
        Granades_T.text = Granades.ToString();
        Coins_T.text = Coins.ToString();
    }

    void FixedUpdate()
    {
        P_Rigidbody.velocity = velocity;
        transform.LookAt(lookPoint);

        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
    }

    public void Die()
    {
        OpenDeathCanvas();

    }

    public void OpenDeathCanvas()
    {
        Time.timeScale = 0;
        info.text = string.Format("You survived {0} waves,\n\nkilled {1} enemies,\n\nfound {2} coins.", s.CurrentWave, s.enemiesKilled, CoinsAll);
        DeathScreen.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
