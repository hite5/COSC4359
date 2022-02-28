using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{

    public GameObject GoodGuyRespawn;
    public EnemyManager EManager;
    int temp = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") == true || collision.CompareTag("Globin"))
        {
            collision.TryGetComponent<GoodGuyMarker>(out GoodGuyMarker mark);
            //Debug.Log("PlayerDetected");
            if (mark)
            {
                collision.transform.position = GoodGuyRespawn.transform.position;
            }
        }
        else if (collision.CompareTag("Enemy") || collision.CompareTag("EnemyMelee") || collision.CompareTag("Colony"))
        {
            temp = Random.Range(0, EManager.spawnPoints.Count - 1);
            //Debug.Log("SPAWN POINT AYO " + temp);
            collision.transform.position = EManager.spawnPoints[temp].transform.position;
        }
    }
}
