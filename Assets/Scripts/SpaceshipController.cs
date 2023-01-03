using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField]
    AudioSource flyAudioSource;
    [SerializeField]
    AudioClip flyAudioClip, idleAudioClip, fireAudioClip;

    [Header("Movement")]
    [SerializeField]
    float movementRadius = 15f;
    [SerializeField]
    float speed = 5f;
    [SerializeField]
    float lean = 20f;

    [Header("Firing")]
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    float firePeriod = 0.5f;

    [SerializeField, HideInInspector]
    float m_position = 0f;
    [SerializeField, HideInInspector]
    float m_timeNextFire = 0f;

    void Update()
    {
        HandleMovement();
        HandleFiring();
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

            flyAudioSource.clip = flyAudioClip;
        }
        else
        {
            flyAudioSource.clip = idleAudioClip;
        }

        if (!flyAudioSource.isPlaying)
        {
            flyAudioSource.Play();
        }
    }

    void HandleFiring()
    {
        bool isFiring = Input.GetKey("space");
        if (isFiring && m_timeNextFire < Time.time)
        {
            GameObject.Instantiate(bullet, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(fireAudioClip, transform.position);
            m_timeNextFire = Time.time + firePeriod;
        }
    }
}
