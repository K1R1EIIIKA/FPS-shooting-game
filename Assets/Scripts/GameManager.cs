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
    [SerializeField] private float gameTimer = 30;
    
    public int points;
    [HideInInspector] public bool isGame = true;

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
        StartCoroutine(StartTimer());
    }

    private void Update()
    {
        pointsText.text = "Points: " + points;
    }

    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(gameTimer);
        EndGame();
    }

    private void EndGame()
    {
        isGame = false;
        gameEndCanvas.SetActive(true);
        TargetSpawn.DeleteAllTargets();
        CameraMovement.Instance.UnlockCursor();
    }
}
