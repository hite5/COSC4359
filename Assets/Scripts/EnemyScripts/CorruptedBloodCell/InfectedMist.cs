using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedMist : MonoBehaviour
{
    public float transformTimer = 5f;
    public GameObject EnemyToSpawn;
    public EnemyColony commander;
    bool done = false;


    // Update is called once per frame
    void Update()
    {
        transformTimer -= Time.deltaTime;
        if (transformTimer <= 0 && !done)
        {
            done = true;
            var go = Instantiate(EnemyToSpawn, this.transform.position, Quaternion.identity);
            go.GetComponent<Enemy3AddOn>().Commander = commander;
            go.GetComponent<Enemy3>().hasCommander = true;
            this.gameObject.GetComponent<Enemy3>().Die();
        }
        
    }
}
