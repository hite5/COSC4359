using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlagManager : MonoBehaviour
{
    public EnemyColony Commander;
    // Start is called before the first frame update
    void Awake()
    {
        Commander = transform.parent.GetComponent<EnemyColony>();
        transform.SetParent(null);
    }
}
