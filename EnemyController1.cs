using UnityEngine;
using System.Collections.Generic;

public class EnemyController1 : MonoBehaviour {

    NavMeshAgent agent;
    public GameObject target;
    public Vector3 lastPos;
    public float EstimatedMoveTime;
    public float EstimatedMoveTimeElapsed;
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
    
}
