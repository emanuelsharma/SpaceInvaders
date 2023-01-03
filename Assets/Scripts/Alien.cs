using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    [field: SerializeField, HideInInspector]
    public int x { get; set; } = -1;
    [field: SerializeField, HideInInspector]
    public int y { get; set; } = -1;
    [field: SerializeField, HideInInspector]
    public GameManager gameManager { get; set; }

    public void OnDestroyed(Damageable damageable, int damageDealt)
    {
        gameManager.OnAlienDead(this);
        GameObject.Destroy(gameObject);
    }
}
