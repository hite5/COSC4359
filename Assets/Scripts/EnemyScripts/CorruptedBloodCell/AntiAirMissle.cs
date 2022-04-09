using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiAirMissle : MonoBehaviour
{
	public float speed = 5;
	public float rotatingSpeed = 200;
	public GameObject target;
	public ParticleSystem missleTrail;
	public ParticleSystem missleExplosion;

	public float flyFree = 5f;
	public float expire = 15f;
	public float explodeDist = 1f;

	public float damageValue = 150;

	Rigidbody2D rb;
	Vector2 point2Target;
	private float value = 0;
	bool isDone = false;

	// Use this for initialization
	void Awake()
	{
		missleExplosion.Stop();

		target = GameObject.FindGameObjectWithTag("Player");
		rb = GetComponent<Rigidbody2D>();
	}


	IEnumerator explodeParticleEffect()
	{
		AudioManager.instance.PlayEffect("DistantExplosionLowPitch");
		missleTrail.Stop();
		missleExplosion.Stop();
		missleExplosion.Play();

		yield return new WaitForSeconds(3);
		Destroy(this.gameObject);
	}

	void explode()
	{
		missleExplosion.Play();
	}

    private void Update()
    {
		if (target == null)
		{
			target = this.gameObject;
			StartCoroutine(explodeParticleEffect());
		}

		if (flyFree >= 0)
		{
			flyFree -= Time.deltaTime;
		}
		expire -= Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
	{
		if (target != null)
		{
			if (flyFree < 0)
			{
				point2Target = (Vector2)transform.position - (Vector2)target.transform.position;
				point2Target.Normalize();

				value = Vector3.Cross(point2Target, transform.right).z;

				rb.angularVelocity = rotatingSpeed * value;
			}

			if (Vector2.Distance(this.transform.position, target.transform.position) <= explodeDist && isDone == false || expire < 0 && isDone == false)
			{
				//damageHelo
				speed = 0;
				isDone = true;
				target.TryGetComponent<HelicopterMarker>(out HelicopterMarker globin);
				if(globin!=null)
					globin.dealDamage(damageValue, 7);
				StartCoroutine(explodeParticleEffect());
			}

		}
		rb.velocity = transform.right * speed;


	}
}
