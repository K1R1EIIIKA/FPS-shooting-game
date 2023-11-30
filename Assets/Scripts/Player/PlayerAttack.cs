using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Camera _mainCamera;
    private float _attackCooldown = 0.5f;
    private bool _isCooldown;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !_isCooldown)
            HitLogic();
    }

    private void HitLogic()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                GameObject target = hit.transform.gameObject;
                TargetMovement targetInfo = target.GetComponent<TargetMovement>();
                GameManager.Instance.points += targetInfo.pointsGain;
                
                TargetSpawn.Instance.DeleteTarget(target, targetInfo.targetIndex);
            }
        }
    }
}
