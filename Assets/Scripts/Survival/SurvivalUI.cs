using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalUI : MonoBehaviour
{
    int currentWave = 0;
    public Text SurviveTimer;
    public Text GraceTimer;
    public Text WaveText;
    public RectTransform GraceTimerBox;
    public Text RemainingCount;

    public GameObject Survive;
    public GameObject GracePeriod;
    public GameObject EnemyRemaining;
    
    public float blinkerTime = 0;
    private int counting = 0;
    private bool colorChangeGate = false;

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
                {
                    WaveText.text = "";
                    GraceTimer.text = "WAVE " + (currentWave + 1).ToString() + " BEGINS IN: " + SurvivalManager.instance.timerStart.ToString("F2");
                    if (SurvivalManager.instance.timerStart < 10)
                    {
                        blinkerTime += Time.deltaTime;
                        if (blinkerTime <= 0.5)
                            GraceTimer.color = Color.red;
                        else if (blinkerTime < 1.0)
                            GraceTimer.color = Color.white;
                        else
                            blinkerTime = 0;
                        
                    }
                }
                else
                {
                    GraceTimer.text = "0";
                }
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
                colorChangeGate = false;
                GraceTimer.color = Color.white;
                GracePeriod.SetActive(false);
                turnOnOrOffDuringGrace(false);
            }
            if (!Survive.activeSelf)
                Survive.SetActive(true);

            if (currentWave != SurvivalManager.instance.currentWave)
            {
                currentWave = SurvivalManager.instance.currentWave;
                WaveText.text = "WAVE: " + currentWave.ToString();

            }

            if (SurvivalManager.instance.bossRound == false)
            {
                if (SurvivalManager.instance.survivalTimer > 0)
                    SurviveTimer.text = "SURVIVE: " + SurvivalManager.instance.survivalTimer.ToString("F2");
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
