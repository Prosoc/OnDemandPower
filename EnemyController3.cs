using UnityEngine;
using System.Collections.Generic;

public class EnemyController3 : MonoBehaviour {

    NavMeshAgent agent;
    public GameObject target;
    public float Damage = 20;
    public Vector3 lastPos;
    public float EstimatedMoveTime;
    public float EstimatedMoveTimeElapsed;
    public ParticleSystem boom;
    bool fusing = false;
    float fuseTime_C = 1;
    // Use this for initialization
    void Start()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }
        agent = GetComponent<NavMeshAgent>();
        RandomPoint(transform.position, 10, out lastPos);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && agent != null)
        {

            if (fusing)
            {
                fuseTime_C -= Time.deltaTime;
                if (fuseTime_C <= 0)
                {
                    RaycastHit[] hits = Physics.SphereCastAll(transform.position, 5, Vector3.up);
                    foreach (RaycastHit hit in hits)
                    {
                        if (hit.transform.tag == "RandomLoot" || hit.transform.tag == "Enemy" || hit.transform.tag == "Player")
                        {
                            hit.transform.GetComponent<Rigidbody>().AddForce((hit.transform.position - transform.position).normalized * 50, ForceMode.Impulse);
                            try
                            {
                                hit.transform.GetComponent<EnemyHealth>().TakeDamage(Damage * (1 / Vector3.Distance(hit.transform.position, transform.position)));
                                continue;
                            }
                            catch (System.Exception)
                            {
                            }

                            try
                            {
                                hit.transform.GetComponent<RandomDropFromBox>().TakeDamage(Damage * (1 / Vector3.Distance(hit.transform.position, transform.position)));
                                continue;
                            }
                            catch (System.Exception)
                            {
                            }

                            try
                            {
                                hit.transform.GetComponent<PlayerController>().TakeDamage(Damage * (1 / Vector3.Distance(hit.transform.position, transform.position)));
                                continue;
                            }
                            catch (System.Exception)
                            {
                            }
                        }
                    }
                    ParticleSystem go = (ParticleSystem)Instantiate(boom, transform.position, transform.rotation);
                    Destroy(go.gameObject, 0.3f);                    
                    Destroy(gameObject, 0.3f);
                }
            }
            else
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, target.transform.position - transform.position, out hit))
                {
                    if (hit.transform.tag == "Player" && Vector3.Distance(target.transform.position, transform.position) <= 20)
                    {
                        agent.SetDestination(target.transform.position);
                        transform.LookAt(target.transform.position);
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            fusing = true;
            
        }
    }

}
