using UnityEngine;
using System.Collections.Generic;

public class EnemyController4 : MonoBehaviour {

    NavMeshAgent agent;
    public GameObject target;
    public Vector3 lastPos;
    public float EstimatedMoveTime;
    public float EstimatedMoveTimeElapsed;
    public GameObject Granade;
    public float damage = 5, fireRate = 60;
    float currentDropDelay;
    float dropDelay;
    // Use this for initialization
    void Start()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }
        agent = GetComponent<NavMeshAgent>();
        RandomPoint(transform.position, 10, out lastPos);
        dropDelay = 60 / fireRate;
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
                    if (Vector3.Distance(target.transform.position, transform.position) <= 10)
                    {
                        agent.destination = transform.position;
                        AimAtPlayer();
                        currentDropDelay = Mathf.Clamp(currentDropDelay - Time.deltaTime, 0, dropDelay);
                        if (currentDropDelay <= 0)
                        {
                            DropGranade();
                            currentDropDelay = dropDelay;
                        }
                    }
                    else
                    {
                        agent.SetDestination(target.transform.position);                       

                    }
                    lastPos = target.transform.position;
                }
                else
                {
                    EstimatedMoveTimeElapsed += Time.deltaTime;
                    if (Vector3.Distance(lastPos, transform.position) <= 3 || EstimatedMoveTimeElapsed >= EstimatedMoveTime * 1.2f)
                    {
                        RandomPoint(transform.position, 10, out lastPos);
                        EstimatedMoveTime = MoveTime(transform.position, lastPos / Random.Range(0f, 2f), agent.speed);
                        EstimatedMoveTimeElapsed = 0;
                    }
                    agent.SetDestination(lastPos / Random.Range(0f, 2f));
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
    

    void DropGranade()
    {       
        GameObject granade = (GameObject)Instantiate(Granade, transform.position + transform.forward.normalized, transform.rotation);
        granade.GetComponent<Rigidbody>().AddForce(transform.forward * Mathf.Clamp(Vector3.Distance(target.transform.position, transform.position), 0, 105), ForceMode.Impulse);
        granade.GetComponent<GranadeController>().damage = damage;
        granade.GetComponent<GranadeController>().Origin = "Enemy";
        Destroy(granade, 5f);
    }

    void AimAtPlayer()
    {
        transform.LookAt(target.transform.position + target.GetComponent<Rigidbody>().velocity / 10);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }

}
