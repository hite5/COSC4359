using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3AddOn : MonoBehaviour
{
    public EnemyColony Commander;
    public Enemy3 enemy3;

    private void Awake()
    {
        enemy3 = this.GetComponent<Enemy3>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Commander != null)
        { 
            
        }
    }
}
