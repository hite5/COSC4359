using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPickup : MonoBehaviour
{
    public bool PermaDuration;
    public float range;
    public float Duration;
    public float ArmorGen;
    public ParticleSystem pS;
    public Animator AnimController;
    public Transform playerTracker;
    private CircleCollider2D cirCol;
    private ParticleSystem.EmissionModule pSemission;
    private ParticleSystem.ShapeModule pSshape;
    private Player player = null;
    float initDur;
    // Start is called before the first frame update
    void Start()
    {
        cirCol = GetComponent<CircleCollider2D>();
        initDur = Duration;
        //pS = GetComponent<ParticleSystem>();
        cirCol.radius = 0;
        pSemission = pS.emission;
        pSemission.rateOverTime = 0;
        pSshape = pS.shape;
        pSshape.radius = 0;
        pS.Stop();
        if (!PermaDuration)
            Destroy(gameObject, Duration + 2);
    }

    // Update is called once per frame
    void Update()
    {
        if(!PermaDuration)
            Duration = Duration > 0 ? Duration -= Time.deltaTime : 0;
        cirCol.radius = cirCol.radius < range ? cirCol.radius += range * Time.deltaTime : range;
        float distance = 0;
        if (player != null)
        {
            distance = Vector2.Distance(player.Stats.Position, playerTracker.position);
            playerTracker.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(player.Stats.Position.y - playerTracker.position.y, player.Stats.Position.x - playerTracker.position.x) * Mathf.Rad2Deg);
        }
        pSemission.rateOverTime = distance * 50;
        pSshape.radius = distance;
        if (Duration == 0)
            AnimController.SetBool("Undeploy", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Duration <= initDur - 1 && Duration >= 1)
        {
            player = collision.GetComponent<Player>();
            pS.Play();
            //if (player.Stats.Armorz < 700)
            //    player.Stats.Armorz += 100;
            //else if (player.Stats.Armorz >= 700 && player.Stats.Armorz < 799)
            //    player.Stats.Armorz = 799;
            //Debug.Log(player.Stats.ArmorLevel + " " + player.Stats.Armorz);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Duration <= initDur - 1 && Duration >= 1 || PermaDuration)
        {
            player = null;
            pS.Stop();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Duration <= initDur - 1 && Duration >= 0.5 || PermaDuration)
        {
            player = collision.GetComponent<Player>();
            if (pS.isStopped)
                pS.Play();
            if (player.Stats.Armorz < 799 - ArmorGen)
                player.Stats.Armorz += ArmorGen;
            else if (player.Stats.Armorz >= 799 - ArmorGen && player.Stats.Armorz < 799)
                player.Stats.Armorz = 799;
            //Debug.Log(player.Stats.ArmorLevel + " " + player.Stats.Armorz);
        }
        else if(Duration >= initDur - 1 || Duration <= 0.5)
        {
            if (pS.isPlaying)
                pS.Stop();
        }
    }
}
