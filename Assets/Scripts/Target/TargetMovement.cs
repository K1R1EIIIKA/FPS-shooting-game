using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    [HideInInspector] public int targetIndex;

    private float _startZPosition;

    private void Start()
    {
        _startZPosition = transform.position.z;
    }

    private void Update()
    {
        Move();
        
        if (transform.position.z - _startZPosition >= TargetSpawn.Instance.maxZRange)
        {
            TargetSpawn.Instance.DeleteTarget(gameObject, targetIndex);
            GameManager.Instance.points--;
        }
    }

    private void Move()
    {
        transform.position += Vector3.forward * (TargetSpawn.Instance.targetSpeed * Time.deltaTime);
    }
}
