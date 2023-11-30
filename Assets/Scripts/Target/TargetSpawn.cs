using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class TargetSpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> targetPrefabs;
    [SerializeField] private List<float> chances;
    [SerializeField] private Vector2 spawnTimeRange;
    [SerializeField] private Transform[] tracks = new Transform[7];

    public float targetSpeed = 5;

    public static TargetSpawn Instance;

    private bool[] _currentTracks = new bool[7];

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        for (var i = 0; i < _currentTracks.Length; i++)
            _currentTracks[i] = true;
    }

    public IEnumerator StartTargetSpawn()
    {
        if (!GameManager.Instance.IsGame)
            yield break;

        if (_currentTracks.Count(x => x) > 0)
            SpawnTarget();

        float time = Random.Range(spawnTimeRange.x, spawnTimeRange.y);

        yield return new WaitForSeconds(time);
        StartCoroutine(StartTargetSpawn());
    }

    private void SpawnTarget()
    {
        int trackIndex;
        while (true)
        {
            trackIndex = Random.Range(0, tracks.Length);
            if (_currentTracks[trackIndex])
            {
                _currentTracks[trackIndex] = false;
                break;
            }
        }

        int randNum = Random.Range(1, 101);
        float num = 0;
        for (int i = 0; i < chances.Count; i++)
        {
            if (randNum > num && randNum <= chances[i] + num)
            {
                GameObject target = Instantiate(targetPrefabs[i], tracks[trackIndex].position, Quaternion.identity, transform);
                target.GetComponent<TargetMovement>().targetIndex = trackIndex;
                return;
            }

            num += chances[i];
        }
    }

    
    public void DeleteTarget(GameObject target, int index)
    {
        _currentTracks[index] = true;
        Destroy(target);
    }

    public static void DeleteAllTargets()
    {
        List<GameObject> targets = GameObject.FindGameObjectsWithTag("Enemy").ToList();

        foreach (GameObject target in targets)
        {
            Destroy(target);
        }
    }
}