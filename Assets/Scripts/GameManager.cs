using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool PlayerDied { get; private set; }

    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private UnityEvent _gameOverEvent;

    private void Awake()
    {
        if (Instance) Debug.LogError("Why you need multiple managers?");
        Instance = this;
    }

    void Start()
    {
        _gameOverScreen.SetActive(false);
        PlayerDied = false;
    }

    public void GameOver()
    {
        if (PlayerDied) return;

        _gameOverScreen.SetActive(true);
        _scoreText.text = "Final Score: " + ScoreManager.Instance.Score.ToString();
        _gameOverEvent.Invoke();
        PlayerDied = true;

        foreach(GameObject mail in GameObject.FindGameObjectsWithTag("Mail")) Destroy(mail);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnApplicationQuit()
    {
        foreach (GameObject mail in GameObject.FindGameObjectsWithTag("Mail")) DestroyImmediate(mail);
    }
}
