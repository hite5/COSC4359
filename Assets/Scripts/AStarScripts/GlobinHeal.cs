using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobinHeal : MonoBehaviour
{
    public Globin globinScript;
    public Player playerScript;
    public ParticleSystem healerPS;
    private ParticleSystem.ShapeModule psShape;
    private ParticleSystem.EmissionModule psEmission;

    [Header("Heal Radius Settings")]
    public float healRadiusAmount = 50;
    public float rangeSpeed = 5f;
    private float range;
    public float rangeMax = 10f;
    //private float lowestHP = Mathf.Infinity;
    //private float tempHP = 0;

    [Header("Heal Pulse Settings")]
    public float AoeCoolDown = 0;
    private float AoeCoolDownTimer = 0;
    public int amountOfTimesToPulse = 5;

    public Transform healTransform;
    //private List<Collider2D> alreadyPingedColliderList;
    private HashSet<Collider2D> alreadyPingedColliderList;

    private void Awake()
    {
        healTransform = transform.Find("healCircle");
        //alreadyPingedColliderList = new List<Collider2D>();
        alreadyPingedColliderList = new HashSet<Collider2D>();
        healerPS.Stop();
        psEmission = healerPS.emission;
        psShape = healerPS.shape;
    }
    public int timesPulsed = 0;
    private void Update()
    {
        if (GlobalPlayerVariables.EnableAI)
        {
            if (AoeCoolDownTimer <= 0)
            {
                if (timesPulsed < amountOfTimesToPulse)
                {
                    range += rangeSpeed * Time.deltaTime;
                    if (range > rangeMax)
                    {
                        range = 0f;
                        alreadyPingedColliderList.Clear();
                        timesPulsed++;
                    }
                    //healTransform.localScale = new Vector3(range, range);
                    healerPS.Play();
                    psShape.radius = range;
                    psEmission.rateOverTime = range * 200;
                    //try overlap sphere
                    Collider2D[] ColliderArray = Physics2D.OverlapCircleAll(transform.position, range);
                    foreach (Collider2D ping in ColliderArray)
                    {
                        //Debug.Log("PING0");
                        if (ping != null)
                        {
                            if (ping.TryGetComponent<GoodGuyMarker>(out GoodGuyMarker goodMarker))
                            {
                                //Debug.Log("PING1");
                                if (ping.TryGetComponent<Globin>(out Globin markedG))
                                {
                                    if (!alreadyPingedColliderList.Contains(ping))
                                    {
                                        alreadyPingedColliderList.Add(ping);
                                        //Debug.Log("PING GLOBIN ADD");
                                        markedG.recieveHeal(healRadiusAmount);


                                    }
                                    //Debug.Log("PING2");
                                }
                                else if (ping.TryGetComponent<Player>(out Player markedP))
                                {
                                    if (!alreadyPingedColliderList.Contains(ping))
                                    {
                                        alreadyPingedColliderList.Add(ping);
                                        markedP.transform.GetComponent<TakeDamage>().recieveHeal(healRadiusAmount);
                                        //Debug.Log("PING PLAYER ADD");
                                        //do heal

                                    }
                                    //Debug.Log("PING2");
                                }
                            }
                        }
                    }
                }
                else
                {
                    AoeCoolDownTimer = AoeCoolDown;
                    timesPulsed = 0;
                    healerPS.Stop();
                }
            }
            else
            {
                AoeCoolDownTimer -= Time.deltaTime;
            }
        }
    }
    public float fineTune = 0.9f;
    public bool showDebugHealCircle = false;
    private void OnDrawGizmos()
    {
        if (showDebugHealCircle == true)
        {
            Gizmos.DrawWireSphere(transform.position, range * fineTune);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, rangeMax * fineTune);
        }
    }

    //public Vector3 directionToTarget = Vector3.zero;






}
