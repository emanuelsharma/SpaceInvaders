using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    float movementRadius = 15f;
    [SerializeField]
    float speed = 5f;
    [SerializeField]
    float lean = 20f;

    [SerializeField, HideInInspector]
    float m_position = 0f;

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float direction = Input.GetAxis("Horizontal");

        if (direction != 0)
        {
            float velocity = direction * speed;
            m_position += Time.deltaTime * velocity;
            m_position = Mathf.Clamp(m_position, -movementRadius, movementRadius);

            transform.position = new Vector3(m_position, 0f, 0f);
            transform.rotation = Quaternion.Euler(0f, 0f, -lean * direction);
        }
    }
}
