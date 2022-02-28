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
    public Text RemainingCount;

    public GameObject Survive;
    public GameObject GracePeriod;
    public GameObject EnemyRemaining;

    private int counting = 0;

    public List<GameObject> enableDuringGrace;


    void turnOnOrOffDuringGrace(bool turnOn)
    {
        if (turnOn)
        {
            for (int i = 0; i < enableDuringGrace.Count; i++)
            {
                enableDuringGrace[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < enableDuringGrace.Count; i++)
            {
                enableDuringGrace[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //link this to survival manager to remove update later after testing
        if (SurvivalManager.instance.roundOver == true)
        {
            if (GlobalPlayerVariables.TotalEnemiesAlive <= 0)
            {
                if (!GracePeriod.activeSelf)
                {
                    GracePeriod.SetActive(true);
                    turnOnOrOffDuringGrace(true);
                    counting = 0;
                }
                if (Survive.activeSelf)
                    Survive.SetActive(false);
                if (EnemyRemaining.activeSelf)
                    EnemyRemaining.SetActive(false);
                if (SurvivalManager.instance.timerStart > 0)
                    GraceTimer.text = SurvivalManager.instance.timerStart.ToString("F2");
                else
                    GraceTimer.text = "0";

            }
            else
            {
                if (counting != GlobalPlayerVariables.TotalEnemiesAlive && GlobalPlayerVariables.TotalEnemiesAlive <= 10)
                {
                    if (!EnemyRemaining.activeSelf)
                        EnemyRemaining.SetActive(true);
                    RemainingCount.text = "ENEMIES REMAINING " + GlobalPlayerVariables.TotalEnemiesAlive.ToString();
                    counting = GlobalPlayerVariables.TotalEnemiesAlive;
                }
                Debug.Log("COUNTING " + counting);
            }
        }
        else
        {
            if (GracePeriod.activeSelf)
            {
                GracePeriod.SetActive(false);
                turnOnOrOffDuringGrace(false);
            }
            if (!Survive.activeSelf)
                Survive.SetActive(true);

            if (currentWave != SurvivalManager.instance.currentWave)
            {
                currentWave = SurvivalManager.instance.currentWave;
                WaveCount.text = currentWave.ToString();
            }

            if (SurvivalManager.instance.bossRound == false)
            {
                if (SurvivalManager.instance.survivalTimer > 0)
                    SurviveTimer.text = SurvivalManager.instance.survivalTimer.ToString("F2");
                else
                    SurviveTimer.text = "0";
            }
            else
            {
                SurviveTimer.text = "BOSS ROUND";
            }


        }
    }
}
