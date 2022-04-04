using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[System.Serializable]
public class EMC1Addition1
{
    public EnemyColony EMC;
    [Header("Summon Allies")]
    public int AmountToSummonAtATime = 5;
    public float startRangeToSummon = 5f;
    public float endRangeToSummon = 15f;
    public float SummonTimer = 15f;


    public Transform target;

    public Transform EnemyTarget;

    public ParticleSystem spinRing;


    //A*
    public float nextWaypointDistance = 3f;

    public Path path;
    int currentWaypoint = 0;
    bool reachedEndofPath = false;

    public Seeker seeker;


    [Header("Line Of Sight")]
    //public bool lineofsight;
    //public LayerMask IgnoreMe;
    //public float shootdistance;
    private float distancefromplayer;
    private float distancefromtarget;



    [Header("Special Movement/Abilities")]
    //public bool retreatToBoss = false;
    //public float retreatDist;

    public bool canDash = false;
    public bool randomDash = false;
    public float dashForce;
    public float dashBackOnHit;
    public float beginningRangeToDash;
    public float endingRangeToDash;
    private float DashTimer;
    public float timebetweenmultiDash;
    private float timebtwdashtimers;
    public float dashAroundPlayerRadius;

    [Header("Chase Settings")]
    public float time2chase;
    private float chaseInProgress;

    //[Header("Reset Check Setting")]
    //public float recalcshortestDist = 3f;
    //private float timer2reset;


    public List<GameObject> alliesToSummonGroundUnits;
    public List<GameObject> alliesToSummonTanks;
    public List<GameObject> mists;

    public Transform player;
    
    public Transform playerStash;

    [Header("enemy flag")]
    public GameObject flag;
    public bool followFlag = false;


    public void setPlayerStash(Transform playerRef)
    {
        player = playerRef;
        target = playerRef;
        playerStash = playerRef;
    }

    public void Astar()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndofPath = true;
            return;
        }
        else
        {
            reachedEndofPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - EMC.ReadRigidBody()).normalized;
        Vector2 force = direction * EMC.speed * Time.deltaTime;
        EMC.AddForceToRB(force, false);


        float distance = Vector2.Distance(EMC.ReadRigidBody(), path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        //return;


    }

    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }


    public void fixUpdateMovementAStar()
    {
        if (target == null)
        {
            player = playerStash;
            chaseInProgress = 0f;
        }

        if (player != null && target != null)
            distancefromplayer = Vector2.Distance(EMC.ReadRigidBody(), player.position);
        if (target == player)
        {
            distancefromtarget = distancefromplayer;
        }
        else if (target != null)
        {
            distancefromtarget = Vector2.Distance(EMC.ReadRigidBody(), target.position);
            EMC.updateDistanceFromTarget(distancefromtarget);
        }




        if (distancefromtarget >= EMC.stoppingDistance || EMC.lineofsight == false)
        {
            Astar();
            //Debug.Log("Astar");
        }
        else //RANDOM MOVEMENT
        {
            if (EMC.reachedDestination == false)
            {
                Vector2 direction = (EMC.randPos - EMC.ReadRigidBody()).normalized;
                Vector2 force = direction * EMC.speed * Time.deltaTime;
                EMC.AddForceToRB(force, false);
                //transform.position = Vector2.MoveTowards(transform.position, randPos, speed * Time.deltaTime); //need to make it where it walks in that direction
            }
            //if (EMC.transform.position.x == EMC.randPos.x && EMC.transform.position.y == EMC.randPos.y)
            if(Vector2.Distance(EMC.ReadRigidBody(), EMC.randPos) <= 1)
            {
                EMC.reachedDestination = true;
            }
        }

    }

    void randomDashPos(bool goForPlayer)
    {
        //NextMoveCoolDown = Random.Range(0f, timeTillNextMove);

        if (target != null)
            EMC.randPos = target.position;
        else
            EMC.randPos = EMC.transform.position;

        EMC.randPos += Random.insideUnitCircle * dashAroundPlayerRadius;

        Vector3 newpos = new Vector3(EMC.randPos.x, EMC.randPos.y, 0);
        //Vector3 zfix = new Vector3(randPos.x, randPos.y, 0);
        //randPos = zfix;
        EMC.reachedDestination = false;
        Vector2 direction = Vector2.zero;
        //RaycastHit2D hit2 = Physics2D.Raycast(transform.position, newpos - transform.position, Mathf.Infinity, ~IgnoreMe);
        if (EMC.lineofsight == true && goForPlayer == false)
        {
            //dash around player
            direction = (newpos - EMC.transform.position).normalized;

        }
        else if (goForPlayer == true && EMC.lineofsight == true)
        {
            //newpos = new Vector3(randPos.x, randPos.y, 0);
            if (playerStash != null)
                direction = (target.position - EMC.transform.position).normalized;
            else
                direction = EMC.transform.position.normalized;
            //direction = (newpos - transform.position).normalized;
        }
        else if (EMC.lineofsight == false)
        {
            //lineofsight == false;
            EMC.randPos = EMC.transform.position;
            EMC.randPos += Random.insideUnitCircle * dashAroundPlayerRadius;
            newpos = new Vector3(EMC.randPos.x, EMC.randPos.y, 0);
            direction = (newpos - EMC.transform.position).normalized;

        }

        Vector2 force = direction * dashForce;
        if (goForPlayer == true)
        {
            force = direction * dashForce * 2;
        }

        EMC.AddForceToRB(force, true);
        //rb.AddForce(force, ForceMode2D.Impulse);



    }



    
    public void summonComrades()
    {
        //360 raycast to check for open spots
        //bossPosIsTheCenter
        //have to adjust mist to account for tanks
        for (int i = 0; i < AmountToSummonAtATime; i++)
        {
            GameObject enemy2Spawn = alliesToSummonGroundUnits[Random.Range(0, alliesToSummonGroundUnits.Count)];
            EnemyManager.instance.spawnMistObject(mists[0], EMC.transform, enemy2Spawn, 5f, 8f);
            //spawn mist object
        }



        //summon x amount (pick from player army?)

    }


    public void placeFlag()
    { 
        //similar 360 logic but with the player as the center
    }

    public void antiAir()
    { 
        //finally getting rid of those helos ayo
    }


    public void normalUpdate()
    {
        if (target == null)
        {
            player = playerStash;
            chaseInProgress = 0f;
        }

        if (playerStash == null)
        {
            player = EMC.transform;
        }


        //working on clearing up globin vision
        if (EMC.timer2reset <= 0)
        {
            EMC.timer2reset = EMC.recalcshortestDist;
            float closestDistanceSqr = Mathf.Infinity;
            Collider2D[] ColliderArray = Physics2D.OverlapCircleAll(EMC.transform.position, EMC.shootdistance);
            foreach (Collider2D collider2D in ColliderArray)
            {
                if (collider2D.TryGetComponent<GoodGuyMarker>(out GoodGuyMarker marked))
                {
                    if (collider2D.TryGetComponent<Transform>(out Transform enemy))
                    {
                        //Debug.Log("good guy detected");
                        //CAN THEY SEE THEM

                        //can probably optimize this later
                        RaycastHit2D hit2 = Physics2D.Raycast(EMC.transform.position, enemy.transform.position - EMC.transform.position, Mathf.Infinity, ~EMC.IgnoreMe);

                        if (hit2)
                        {
                            if (hit2.collider.gameObject.CompareTag("Player") || hit2.collider.gameObject.CompareTag("Globin"))
                            {
                                EMC.lineofsight = true;
                                Vector3 directionToTarget = enemy.position - EMC.transform.position;
                                float dSqrToTarget = directionToTarget.sqrMagnitude;
                                if (dSqrToTarget < closestDistanceSqr)
                                {

                                    closestDistanceSqr = dSqrToTarget;
                                    target = enemy;
                                    EMC.player = enemy;
                                    //EnemyTarget = enemy;
                                    //Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.black);
                                    //closest = true;
                                    //Debug.Log("Found target");

                                    //if (EnemyTarget != null && canSeeEnemy == true && closest == true && EnemyTarget == enemy)
                                }
                            }
                        }


                        //target = enemy;
                    }
                }
            }
        }


        //PUT LINE OF SIGHT LOGIC HERE

        if (player != null && player != EMC.transform && target != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(EMC.transform.position, target.transform.position - EMC.transform.position, Mathf.Infinity, ~EMC.IgnoreMe);
            //var rayDirection = player.position - transform.position;
            Debug.DrawRay(EMC.transform.position, target.transform.position - EMC.transform.position, Color.green);
            if (hit.collider.gameObject.CompareTag("Player") || hit.collider.gameObject.CompareTag("Globin"))
            {
                EMC.lineofsight = true;
                //Debug.Log("Player is Visable");
                // enemy can see the player!

                //Debug.Log("Player is Visable");
            }
            else
            {
                EMC.lineofsight = false;
                //Debug.Log("Player is NOT Visable");
            }

        }

        //call Summon function
        if (SummonTimer <= 0)
        {
            SummonTimer = Random.Range(startRangeToSummon, endRangeToSummon);
            summonComrades();

        }
        else
        {
            SummonTimer -= Time.deltaTime;
        }



    }


}

