using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunker : MonoBehaviour
{
    public void OnDamaged(Damageable damageable, int damageDealt)
    {
        transform.localScale = new Vector3(1f, 1f, (float)damageable.crrtHealth / damageable.startingHealth);
    }
    public void OnDestroyed(Damageable damageable, int damageDealt)
    {
        GameObject.Destroy(gameObject);
    }
}
