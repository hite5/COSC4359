using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalInteractable : MonoBehaviour
{
    public Player playerScript;
    public bool inRange = false;
    private void Update()
    {
        if (playerScript != null)
        {
            if (playerScript.Utilities.InteractButtonPressed && inRange == true && OptionSettings.GameisPaused == false)
            {
                SurvivalManager.instance.skipWait = true;
            }
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("ENTER COLLIDER");
        if (other.CompareTag("Player") == true)
        {
            if (playerScript == null)
            {
                playerScript = other.GetComponent<Player>();
            }
            inRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("LEAVING COLLIDER");
        if (other.CompareTag("Player") == true)
        {
            inRange = false;
            SurvivalManager.instance.skipWait = false;
        }
    }
}
