using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class TargetSpawn : MonoBehaviour
{
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private Vector2 timeRange;
    [SerializeField] private Transform[] tracks = new Transform[7];

    private bool[] _currentTracks = new bool[7];

    private void Start()
    {
        for (var i = 0; i < _currentTracks.Length; i++)
            _currentTracks[i] = true;

        StartCoroutine(StartTargetSpawn());
    }
    

    private void Update()
    {
        
    }

    private IEnumerator StartTargetSpawn()
    {
        if (_currentTracks.Count(x => x) > 0) 
            SpawnTarget();

        float time = Random.Range(timeRange.x, timeRange.y);
        Debug.Log(time);
        yield return new WaitForSeconds(time);
        StartCoroutine(StartTargetSpawn());
    }

    private GameObject SpawnTarget()
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
        
        return Instantiate(targetPrefab, tracks[trackIndex].position, Quaternion.identity, transform);
    }
}
