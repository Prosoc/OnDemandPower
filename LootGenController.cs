using UnityEngine;
using System.Collections;

public class LootGenController : MonoBehaviour {

    public float time = 5;
    public GameObject Box;
    public Transform p;
	// Use this for initialization
	void Start () {
        p = FindObjectOfType<PlayerController>().transform;
        StartCoroutine(Generate());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator Generate()
    {

        while (true)
        {
            yield return new WaitForSeconds(time);
            if (transform.childCount > 20)
            {
                continue;
            }
            Vector3 pos = new Vector3(Random.Range(-70, 70), 3, Random.Range(-70, 70));
            for (int i = 0; i < 30; i++)
            {
                
                if (Vector3.Distance(p.position, pos) >= 20)
                {
                    break;
                }
            }

            
            Instantiate(Box, pos, Quaternion.identity, transform);            
        }
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
