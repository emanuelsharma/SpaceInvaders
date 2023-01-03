using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text scoreField, levelField;

    void Start()
    {
        levelField.text = $"Level {GameManager.crrtLevel}";
        scoreField.text = $"Score {GameManager.crrtScore}";
    }

    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("Game");
        }
    }
}
