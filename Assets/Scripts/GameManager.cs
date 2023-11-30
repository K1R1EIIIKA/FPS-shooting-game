using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameEndCanvas;
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float gameTimer = 30;
    
    public int points;
    [HideInInspector] public bool isGame = true;

    private float _secs;

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
        StartCoroutine(StartTimer());
    }

    private void Update()
    {
        pointsText.text = "Points: " + points;
    }

    private IEnumerator StartTimer()
    {
        timerText.text = "Time: " + _secs;
        yield return new WaitForSeconds(1);
        _secs--;
        
        if (_secs <= 0)
        {
            EndGame();
            yield break;
        }
        
        StartCoroutine(StartTimer());
    }

    private void EndGame()
    {
        isGame = false;
        gameEndCanvas.SetActive(true);
        TargetSpawn.DeleteAllTargets();
        CameraMovement.Instance.UnlockCursor();
    }
}
