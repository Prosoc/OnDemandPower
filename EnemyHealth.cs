using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

    public float MaxHealth = 100;
    public float Health = 100;
    public List<GameObject> loots;
    bool isDead;
    public Image HealthFill;
    public int DropNum = 1;
    // Use this for initialization
    void Start () {
        Health = MaxHealth;

    }
	
	// Update is called once per frame
	void Update () {
        HealthFill.fillAmount = Health / MaxHealth;
	}

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            FindObjectOfType<SpawnerController>().RemoveEnemy(gameObject);
            Die();
        }
    }

    public void Die()
    {
        if (loots.Count > 0 && !isDead)
        {
            for (int i = 0; i < DropNum; i++)
            {
                GameObject loot = (GameObject)Instantiate(loots[Random.Range(0, loots.Count - 1)], transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f)), Quaternion.identity);
                loot.AddComponent<Despawner>();
            }            
            isDead = true;
        }
        Destroy(gameObject);
    }
}
