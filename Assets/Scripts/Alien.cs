using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public void OnDestroyed(Damageable damageable, int damageDealt)
    {
        GameObject.Destroy(gameObject);
    }
}
