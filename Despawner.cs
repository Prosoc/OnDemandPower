using UnityEngine;
using System.Collections.Generic;

public class Despawner : MonoBehaviour {

    public float DespawnTime = 30;
    public float CurrentDespawnTime = 0;
    Color baseColor;
    Color otherColor;
    MeshRenderer r;
    List<MeshRenderer> rs;
    
	// Use this for initialization
	void Start () {
        r = transform.GetComponent<MeshRenderer>();
        if (r == null)
        {
            rs = new List<MeshRenderer>();
            rs.AddRange(transform.GetComponentsInChildren<MeshRenderer>());
            baseColor = rs[0].material.color;
        }
        else
        {
            baseColor = r.material.color;            
        }
        if (baseColor == Color.white)
        {
            otherColor = Color.blue;
        }
        else
        {
            otherColor = Color.white;
        }
    }
	
	// Update is called once per frame
	void Update () {
        CurrentDespawnTime += Time.deltaTime;
        if (CurrentDespawnTime >= DespawnTime)
        {
            Destroy(gameObject);
        }
        else if (CurrentDespawnTime >= DespawnTime * 0.5f)
        {
            float num = Mathf.Sin(CurrentDespawnTime);
            if (num > 0)
            {
                if (r != null)
                {
                    r.material.color = Color.Lerp(otherColor, baseColor, num);
                }
                else
                {
                    foreach (Renderer rc in rs)
                    {
                        rc.material.color = Color.Lerp(otherColor, baseColor, num);
                    }
                }
            }
            else
            {
                if (r != null)
                {
                    r.material.color = Color.Lerp(baseColor, otherColor, Mathf.Abs(num));
                }
                else
                {
                    foreach (Renderer rc in rs)
                    {
                        rc.material.color = Color.Lerp(baseColor, otherColor, Mathf.Abs(num));
                    }
                }
                
            }
        }
	}



}
