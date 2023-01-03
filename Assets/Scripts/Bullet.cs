using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float speed = 10f;
    [SerializeField]
    float maxPosition = 20f;

    void Update()
    {
        transform.position += new Vector3(0f, 0f, Time.deltaTime * speed);
        if (transform.position.z > maxPosition)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
