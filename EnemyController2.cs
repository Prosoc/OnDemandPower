using UnityEngine;
using System.Collections.Generic;

public class EnemyController2 : MonoBehaviour {

    NavMeshAgent agent;
    public GameObject target;
    public Vector3 lastPos;
    public float EstimatedMoveTime;
    public float EstimatedMoveTimeElapsed;
    public GameObject Bullet;
    public float velocity = 10, damage = 5, fireRate = 60, range = 10;
    public Transform c_end;
    public ParticleSystem muzleFlash;
    float currentShootDelay;
    float shootDelay;
    public AudioSource shoot;
    // Use this for initialization
    void Start()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }
        agent = GetComponent<NavMeshAgent>();
        RandomPoint(transform.position, 10, out lastPos);
        shootDelay = 60 / fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && agent != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, target.transform.position - transform.position, out hit))
            {
                if (hit.transform.tag == "Player" && Vector3.Distance(target.transform.position, transform.position) <= 20)
                {
                    if (Vector3.Distance(target.transform.position, transform.position) <= range * 1.1f && Vector3.Distance(target.transform.position, transform.position) > range)
                    {
                        agent.destination = transform.position;
                        AimAtPlayer();
                        currentShootDelay = Mathf.Clamp(currentShootDelay - Time.deltaTime, 0, shootDelay);
                        if (currentShootDelay <= 0)
                        {
                            Shoot();
                            currentShootDelay = shootDelay;
                        }
                    }
                    else if(Vector3.Distance(target.transform.position, transform.position) <= range)
                    {
                        AimAtPlayer();
                        agent.destination = transform.position + (transform.position - target.transform.position);
                        AimAtPlayer();
                        currentShootDelay = Mathf.Clamp(currentShootDelay - Time.deltaTime, 0, shootDelay);
                        if (currentShootDelay <= 0)
                        {
                            Shoot();
                            currentShootDelay = shootDelay;
                        }
                    }
                    else
                    {
                        agent.SetDestination(target.transform.position);
                        currentShootDelay = shootDelay;
                    }
                    lastPos = target.transform.position;
                }
                else
                {
                    EstimatedMoveTimeElapsed += Time.deltaTime;
                    if (Vector3.Distance(lastPos, transform.position) <= 3 || EstimatedMoveTimeElapsed >= EstimatedMoveTime * 1.2f)
                    {
                        RandomPoint(transform.position, 10, out lastPos);
                        EstimatedMoveTime = MoveTime(transform.position, lastPos / Random.Range(1f, 2f), agent.speed);
                        EstimatedMoveTimeElapsed = 0;
                    }
                    agent.SetDestination(lastPos / Random.Range(1f, 2f));
                }
            }
        }
    }

    float MoveTime(Vector3 pos1, Vector3 pos2, float speed)
    {
        return Vector3.Distance(pos1, pos2) / speed;
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
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
    

    void Shoot()
    {
        Vector3 rotation = transform.rotation.eulerAngles;
        GameObject newBullet = (GameObject)Instantiate(Bullet, c_end.position, Quaternion.Euler(rotation));
        BulletController bull = newBullet.GetComponent<BulletController>();
        bull.Speed = velocity;
        bull.LifeTime = range / velocity;
        bull.Damage = damage;
        bull.type = "Pistol";
        bull.Origin = "Enemy";
        muzleFlash.Play();
        shoot.Play();
    }

    void AimAtPlayer()
    {
        transform.LookAt(target.transform.position + target.GetComponent<Rigidbody>().velocity / 10);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }

}
