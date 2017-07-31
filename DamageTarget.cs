using UnityEngine;
using System.Collections;

public class DamageTarget : MonoBehaviour {

    public float Damage = 5;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().TakeDamage(Damage * Time.deltaTime);
        }
    }
}
