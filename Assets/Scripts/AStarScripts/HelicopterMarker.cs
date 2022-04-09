using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterMarker : MonoBehaviour
{
    public void dealDamage(float damage, float speed)
    {
        transform.parent.GetComponent<Globin>().takeDamage(damage, transform, speed);
    }
}
