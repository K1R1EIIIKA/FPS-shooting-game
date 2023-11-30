using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameEndCanvas;
    [SerializeField] private GameObject startTimerCanvas;
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI endPointsText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI startTimerText;
    [SerializeField] private float gameTimer = 30;
    [SerializeField] private float startTimer = 3;
    
    public int points;
    [NonSerialized] public bool IsGame;

    private float _secs;
    private float _startSecs;

    public static GameManager Instance;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else 
            Destroy(gameObject);
    }

    private void Start()
    {
        _secs = gameTimer;
        _startSecs = startTimer;
        startTimerCanvas.SetActive(true);
        StartCoroutine(StartTimer());
    }

    private void Update()
    {
        pointsText.text = "Points: " + points;
        timerText.text = "Time: " + _secs;
    }

    private IEnumerator StartTimer()
    {
        startTimerText.text = _startSecs.ToString();
        yield return new WaitForSeconds(1);
        _startSecs--;
        
        if (_startSecs <= 0)
        {
            StartGame();
            StartCoroutine(Timer());
            StartCoroutine(TargetSpawn.Instance.StartTargetSpawn());
            startTimerCanvas.SetActive(false);
            
            yield break;
        }

        StartCoroutine(StartTimer());
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);
        _secs--;
        
        if (_secs <= 0)
        {
            EndGame();
            yield break;
        }
        
        StartCoroutine(Timer());
    }

    private void StartGame()
    {
        IsGame = true;
    }

    private void EndGame()
    {
        IsGame = false;
        endPointsText.text = "Points: " + points;
        gameEndCanvas.SetActive(true);
        TargetSpawn.DeleteAllTargets();
        CameraMovement.Instance.UnlockCursor();
    }
}
