using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 0.5f;
    
    private Camera _mainCamera;
    private bool _isCooldown;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !_isCooldown && GameManager.Instance.IsGame)
            HitLogic();
    }

    private void HitLogic()
    {
        StartCoroutine(SetCooldown());
        
        FindObjectOfType<AudioManager>().Play("Pew");
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

    private IEnumerator SetCooldown()
    {
        _isCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        _isCooldown = false;
    }
}
