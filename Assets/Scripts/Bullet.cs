using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float speed = 10f;
    [SerializeField]
    float maxPosition = 20f;
    [SerializeField]
    int damage = 1;

    void Update()
    {
        transform.position += new Vector3(0f, 0f, Time.deltaTime * speed);
        if (transform.position.z > maxPosition)
        {
            GameObject.Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Damageable damageable = collision.gameObject.GetComponent<Damageable>();
        if (damageable)
        {
            damageable.TakeDamage(damage);
        }

        GameObject.Destroy(gameObject);
    }
}
