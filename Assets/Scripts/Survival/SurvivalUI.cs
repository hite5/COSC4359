using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalUI : MonoBehaviour
{
    public Text WaveCount;
    int currentWave = 0;
    public Text SurviveTimer;
    public Text GraceTimer;

    public GameObject Survive;
    public GameObject GracePeriod;

    // Update is called once per frame
    void Update()
    {
        //link this to survival manager to remove update later after testing
        if (SurvivalManager.instance.roundOver == true)
        {
            if(!GracePeriod.activeSelf)
                GracePeriod.SetActive(true);
            if (Survive.activeSelf)
                Survive.SetActive(false);
            GraceTimer.text = SurvivalManager.instance.timerStart.ToString("F2");
        }
        else
        {
            if (GracePeriod.activeSelf)
                GracePeriod.SetActive(false);
            if (!Survive.activeSelf)
                Survive.SetActive(true);

            if (currentWave != SurvivalManager.instance.currentWave)
            {
                currentWave = SurvivalManager.instance.currentWave;
                WaveCount.text = currentWave.ToString();
            }

            if (SurvivalManager.instance.bossRound == false)
            {
                SurviveTimer.text = SurvivalManager.instance.survivalTimer.ToString("F2");
            }
            else
            {
                SurviveTimer.text = "BOSS ROUND";
            }


        }
    }
}
