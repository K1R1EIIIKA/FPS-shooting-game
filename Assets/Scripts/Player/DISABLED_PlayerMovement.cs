using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform orientation;
    [SerializeField] private float moveSpeed = 5;
    
    private void Update()
    {
        if (!GameManager.Instance.IsGame)
            return;

        Move();
    }

    private void Move()
    {
        Vector3 moveDirection = orientation.forward * Input.GetAxisRaw("Vertical") + orientation.right * Input.GetAxisRaw("Horizontal");

        transform.position += moveDirection * (moveSpeed * Time.deltaTime) * -1;
    }
}
