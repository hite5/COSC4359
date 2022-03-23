using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSelf : MonoBehaviour
{
    public float CountDown = 1f;

    // Update is called once per frame
    void Update()
    {
        if (CountDown > 0)
        {
            CountDown -= Time.deltaTime;
        }
        else
            Destroy(this.gameObject);
    }
}
