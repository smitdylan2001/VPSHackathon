using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public int Score { get; private set; }

    [SerializeField] private TMP_Text _scoreText;

    private void Awake()
    {
        if (Instance) Debug.LogError("Why you need multiple managers?");
        Instance = this;
    }

    public void IncreaseScore(int increase = 1)
    {
        Score += increase;

        _scoreText.text = "Score: " + Score.ToString();
    }
}
