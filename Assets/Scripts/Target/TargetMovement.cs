using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    [HideInInspector] public int targetIndex;
    public int pointsGain;

    [SerializeField] private float lifeSpawn;

    private void Start()
    {
        StartCoroutine(StartLifeSpawn());
    }

    private void Update()
    {
        Move();
    }

    private IEnumerator StartLifeSpawn()
    {
        yield return new WaitForSeconds(lifeSpawn);
        
        TargetSpawn.Instance.DeleteTarget(gameObject, targetIndex);
        if (GameManager.Instance.points > 0)
            GameManager.Instance.points--;
    }

    private void Move()
    {
        transform.position += Vector3.forward * (TargetSpawn.Instance.targetSpeed * Time.deltaTime);
    }
}