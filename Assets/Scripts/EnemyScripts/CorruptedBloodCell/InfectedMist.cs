using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedMist : MonoBehaviour
{
    public float transformTimer = 5f;
    public GameObject EnemyToSpawn;
    public EnemyColony commander;

    // Update is called once per frame
    void Update()
    {
        transformTimer -= Time.deltaTime;
        if (transformTimer <= 0)
        {
            var go = Instantiate(EnemyToSpawn, this.transform.position, Quaternion.identity);
            go.GetComponent<Enemy3AddOn>().Commander = commander;
            Destroy(this.gameObject);
        }
        
    }
}
