using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("ComponentReferences")]
    [SerializeField]
    Alien[] aliens;
    [SerializeField]
    TMP_Text scoreField, levelField;
    [SerializeField, HideInInspector]
    Transform alienSwarm;

    [Header("Balance")]
    [SerializeField]
    float movePeriod;
    [SerializeField]
    int gridWidth, gridHeight;
    [SerializeField]
    int swarmWidth, swarmHeight;
    [SerializeField, HideInInspector]
    float timeNextMove;
    [SerializeField, HideInInspector]
    public int swarmXPos, swarmYPos;
    [SerializeField, HideInInspector]
    public int swarmDirection;

    [Header("Visual")]
    [SerializeField]
    float cellSize;

    [Header("Audio")]
    [SerializeField]
    AudioClip victoryClip;

    [Header("GameState")]
    [SerializeField, HideInInspector]
    Alien[,] crrtAliens;
    [SerializeField, HideInInspector]
    int leftMostAlien, rightMostAlien, bottomMostAlien;
    [field: SerializeField, HideInInspector]
    public static int crrtLevel { get; private set; }
    [field: SerializeField, HideInInspector]
    public static int crrtScore { get; private set; }

    void Start()
    {
        alienSwarm = (new GameObject()).transform;
        alienSwarm.name = "AlienSwarm";
        alienSwarm.parent = transform;

        StartLevel(1);
        SetScore(0);
    }

    void Update()
    {
        if (timeNextMove < Time.time)
        {
            swarmXPos += swarmDirection;
            if (swarmXPos + leftMostAlien + swarmDirection < 0 || swarmXPos + rightMostAlien + swarmDirection > gridWidth)
            {
                swarmDirection *= -1;
                swarmYPos -= 1;
            }
            if (swarmYPos == 0)
            {
                EndGame();
            }

            StopAllCoroutines();
            StartCoroutine(AnimateMoveSwarm());

            timeNextMove = Time.time + movePeriod;
        }
    }

    IEnumerator AnimateMoveSwarm()
    {
        Vector3 finalPosition = new Vector3(swarmXPos * cellSize, 0f, swarmYPos * cellSize);
        float t = 0f;
        while (t < 1f)
        {
            alienSwarm.localPosition = EaseInOutBack(alienSwarm.localPosition, finalPosition, t);
            t += Time.deltaTime;
            yield return null;
        }
        alienSwarm.localPosition = finalPosition;
    }

    public static Vector3 EaseInOutBack(Vector3 start, Vector3 end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1) return end * 0.5f * value * value * value * value + start;
        value -= 2;
        return -end * 0.5f * (value * value * value * value - 2) + start;
    }

    void SetScore(int score)
    {
        crrtScore = score;
        scoreField.text = $"Score {crrtScore}";
    }

    void StartLevel(int level)
    {
        crrtLevel = level;
        levelField.text = $"Level {crrtLevel}";

        leftMostAlien = 0;
        rightMostAlien = swarmWidth - 1;
        bottomMostAlien = 0;
        swarmDirection = 1;
        swarmXPos = 0;
        swarmYPos = gridHeight - swarmHeight;
        crrtAliens = new Alien[swarmWidth, swarmHeight];
        StopAllCoroutines();
        alienSwarm.localPosition = new Vector3(0f, 0f, swarmYPos * cellSize);
        timeNextMove = Time.time + movePeriod;

        for (int y = 0; y < swarmHeight; y++)
        {
            for (int x = 0; x < swarmWidth; x++)
            {
                Alien crrtAlien = GameObject.Instantiate(aliens[y/2], alienSwarm);
                crrtAlien.transform.localPosition = new Vector3(x * cellSize, 0f, y * cellSize);
                crrtAliens[x, y] = crrtAlien;

                crrtAlien.x = x;
                crrtAlien.y = y;
                crrtAlien.gameManager = this;
            }
        }
    }

    void EndGame()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void OnAlienDead(Alien alien)
    {
        // Increase score
        int alienValue = 10 * (1 + alien.y / 2);
        SetScore(crrtScore + alienValue);

        // Update bounds
        crrtAliens[alien.x, alien.y] = null;
        bottomMostAlien = GetBottommostAlien();
        leftMostAlien = GetLeftmostAlien();
        rightMostAlien = GetRightmostAlien();

        // End level if necessary
        if (GetAreAllAliensDead())
        {
            AudioSource.PlayClipAtPoint(victoryClip, transform.position);
            StartLevel(crrtLevel + 1);
        }
    }

    int GetLeftmostAlien()
    {
        for (int x = 0; x < swarmWidth; x++)
        {
            for (int y = 0; y < swarmHeight; y++)
            {
                if (crrtAliens[x, y])
                {
                    return x;
                }
            }
        }
        return -1;
    }

    int GetRightmostAlien()
    {
        for (int x = swarmWidth - 1; x >= 0; x--)
        {
            for (int y = 0; y < swarmHeight; y++)
            {
                if (crrtAliens[x, y])
                {
                    return x;
                }
            }
        }
        return -1;
    }

    int GetBottommostAlien()
    {
        for (int y = 0; y < swarmHeight; y++)
        {
            for (int x = 0; x < swarmWidth; x++)
            {
                if (crrtAliens[x, y])
                {
                    return y;
                }
            }
        }
        return -1;
    }

    bool GetAreAllAliensDead()
    {
        for (int x = 0; x < swarmWidth; x++)
        {
            for (int y = 0; y < swarmHeight; y++)
            {
                if (crrtAliens[x, y])
                {
                    return false;
                }
            }
        }
        return true;
    }
}
