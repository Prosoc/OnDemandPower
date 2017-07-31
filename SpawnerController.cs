using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class SpawnerController : MonoBehaviour {

    public Text waveText;
    public Text enemiesLeftText;
    public float spawnDelay = 1;
    public bool spawning = true;
    public int CurrentWave = 1;
    public GameObject enemyNormal;
    public GameObject enemyBoss;
    public GameObject enemyShooter;
    public GameObject enemyBomber;
    public GameObject enemyBombLobber;
    public List<Wave> waves = new List<Wave>();
    public List<GameObject> waveEnemies = new List<GameObject>();
    public GameObject player;
    public int maxEnemies = 20;
    float nextWaveTime = 5;
    float nextWaveDelay = 5;

    public int enemiesKilled = 0;
    public List<Transform> spawnPoints;

    public Vector3 center = new Vector3();

    public Text WaveTimer;

    public AudioSource source;
    public bool waveSound = true;
    // Use this for initialization
    void Start()
    {
        waveEnemies.Clear();
        spawnDelay = 1;
        CurrentWave = 1;
        nextWaveTime = 5;
        nextWaveDelay = 5;
        enemiesKilled = 0;
        StartCoroutine(SpawnEnemy());
        waves.Add(GetWave(CurrentWave));

    }

    // Update is called once per frame
    void Update()
    {
        waveText.text = (CurrentWave).ToString();
        enemiesLeftText.text = (waveEnemies.Count).ToString();
        if (!spawning)
        {
            WaveTimer.transform.parent.gameObject.SetActive(true);
            WaveTimer.text = Mathf.Clamp((float)Math.Round(nextWaveTime - Time.timeSinceLevelLoad, 2), 0, 123123).ToString();
            if (CurrentWave % 5 == 0)
            {
                WaveTimer.text += "\nBOSS WAVE";
            }
        }
        else
        {
            WaveTimer.transform.parent.gameObject.SetActive(false);
        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        waveEnemies.Remove(enemy);
        enemiesKilled++;
    }

    public IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if (nextWaveTime > Time.timeSinceLevelLoad && waveEnemies.Count == 0)
            {
                spawning = false;
            }
            else
            {
                spawning = true;
                if (spawning && waveEnemies.Count == 0)
                {
                    if (waveSound)
                    {
                        source.Play();
                    }
                    waveSound = !waveSound;
                }
            }
            if (spawning && waves.Count > 0)
            {                
                if (!waves[CurrentWave - 1].WaveFinished)
                {
                    for (int i = 0; i < waves[CurrentWave - 1].enemies.Count; i++)
                    {
                        if (!waves[CurrentWave - 1].enemies[i].finishedSpawning)
                        {
                            if (waves[CurrentWave - 1].enemies[i].number > 0)
                            {
                                GameObject prefab = GetEnemyPrefab(waves[CurrentWave - 1].enemies[i].enemy, CurrentWave);
                                if (prefab != null)
                                {
                                    Vector3 point;                                 

                                    for (int y = 0; y < 4; y++)
                                    {
                                        Transform t = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count - 1)];
                                        if (Vector3.Distance(t.position, player.transform.position) > 20)
                                        {
                                            RandomPoint(t.position, 5, out point);
                                            GameObject enemy = (GameObject)Instantiate(prefab, point, Quaternion.identity);
                                            enemy.GetComponent<EnemyHealth>().MaxHealth = Linear(enemy.GetComponent<EnemyHealth>().MaxHealth, enemy.GetComponent<EnemyHealth>().MaxHealth / 100, CurrentWave);
                                            enemy.GetComponent<NavMeshAgent>().speed = Linear(enemy.GetComponent<NavMeshAgent>().speed, enemy.GetComponent<NavMeshAgent>().speed / 100, CurrentWave);
                                            enemy.GetComponent<NavMeshAgent>().angularSpeed = Linear(enemy.GetComponent<NavMeshAgent>().angularSpeed, enemy.GetComponent<NavMeshAgent>().angularSpeed / 100, CurrentWave);

                                            try
                                            {
                                                enemy.GetComponent<EnemyController>().target = player;
                                            }
                                            catch (System.Exception)
                                            {                                                    
                                            }
                                            try
                                            {
                                                enemy.GetComponent<EnemyController1>().target = player;
                                            }
                                            catch (System.Exception)
                                            {
                                            }
                                            try
                                            {
                                                enemy.GetComponent<EnemyController2>().target = player;
                                            }
                                            catch (System.Exception)
                                            {
                                            }
                                            try
                                            {
                                                enemy.GetComponent<EnemyController3>().target = player;
                                            }
                                            catch (System.Exception)
                                            {
                                            }
                                            try
                                            {
                                                enemy.GetComponent<EnemyController4>().target = player;
                                            }
                                            catch (System.Exception)
                                            {
                                            }

                                            waveEnemies.Add(enemy);
                                            waves[CurrentWave - 1].enemies[i].number--;
                                            break;
                                        }
                                    }
                                }
                                
                            }
                            else
                            {
                                waves[CurrentWave - 1].enemies[i].finishedSpawning = true;
                            }
                        }
                        else
                        {                            
                            if (waves[CurrentWave - 1].enemies[waves[CurrentWave - 1].enemies.Count - 1].finishedSpawning)
                            {
                                waves[CurrentWave - 1].WaveFinished = true;
                            }
                            continue;
                        }
                    }
                }
                else
                {
                    if (waveEnemies.Count == 0)
                    {
                        player.GetComponent<PlayerController>().Coins += CurrentWave;
                        player.GetComponent<PlayerController>().CoinsAll += CurrentWave;
                        CurrentWave++;                        
                        waves.Add(GetWave(CurrentWave));
                        nextWaveTime = Time.timeSinceLevelLoad + nextWaveDelay;
                        nextWaveDelay += CurrentWave % 5 == 0 && CurrentWave != 0 ? 5 : 0;
                    }
                }
            }
            yield return new WaitForSeconds(spawnDelay);
        }
    }
    public GameObject GetEnemyPrefab(EnemyType enemy, int num)
    {
        switch (enemy)
        {
            case EnemyType.Normal:
                {
                    return enemyNormal;
                }
            case EnemyType.Shooter:
                {
                    return enemyShooter;
                }
            case EnemyType.Bomber:
                {
                    return enemyBomber;
                }
            case EnemyType.BombLobber:
                {
                    return enemyBombLobber;
                }
            case EnemyType.Boss:
                {
                    return enemyBoss;
                }
            default:
                {
                    return null;
                }
        }
    }


    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }


    Wave GetWave(int waveNum)
    {
        List<Wave_Enemy> enemies = new List<Wave_Enemy>();
        
        enemies.Add(new Wave_Enemy(EnemyType.Normal, Mathf.CeilToInt(waveNum)));

        if (waveNum >= 3)
        {
            enemies.Add(new Wave_Enemy(EnemyType.Shooter, Mathf.RoundToInt(waveNum / 2f)));
        }
        if (waveNum >= 7)
        {
            enemies.Add(new Wave_Enemy(EnemyType.Bomber, Mathf.RoundToInt(waveNum / 2.5f)));
        }
        if (waveNum >= 10)
        {
            enemies.Add(new Wave_Enemy(EnemyType.BombLobber, Mathf.RoundToInt(waveNum / 3f) - 1));
        }
        if (CurrentWave % 5 == 0)
        {
            enemies.Add(new Wave_Enemy(EnemyType.Boss, Mathf.RoundToInt(waveNum / 5)));
        }

        return new Wave(enemies);
    }

    float Linear(float a, float b , float c)
    {
        return a + b * c;
    }

    public void ClearEnemies()
    {
        spawning = false;
        foreach (GameObject e in waveEnemies)
        {
            Destroy(e);
        }
        waveEnemies.Clear();
    }

}



[System.Serializable]
public class Wave
{    
    public List<Wave_Enemy> enemies;
    public bool WaveFinished;

    public Wave(List<Wave_Enemy> enemies)
    {
        this.enemies = enemies;
    }
}

[System.Serializable]
public class Wave_Enemy
{
    public EnemyType enemy;
    public int number;
    public bool finishedSpawning;

    public Wave_Enemy(EnemyType enemy, int number)
    {
        this.enemy = enemy;
        this.number = number;
    }
}

public enum EnemyType
{
    Normal = 0,
    Shooter = 1,
    Bomber = 2,
    Boss = 3,
    BombLobber = 4
}




