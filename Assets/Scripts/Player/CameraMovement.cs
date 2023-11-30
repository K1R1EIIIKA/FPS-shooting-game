using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    public static CameraMovement Instance;

    private float _xRotation;
    private float _yRotation;
    private bool _isLocked;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else 
            Destroy(gameObject);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _isLocked = true;
    }

    private void Update()
    {
        LockLogic();
        if (!_isLocked) return;
        
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        _yRotation += mouseX;
        
        _xRotation -= mouseY;
        _xRotation = Math.Clamp(_xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
    }

    private void LockLogic()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.isGame)
        {
            if (_isLocked)
                UnlockCursor();
            else
                LockCursor();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && !_isLocked && GameManager.Instance.isGame)
            LockCursor();
    }

    public void LockCursor()
    {
        _isLocked = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    public void UnlockCursor()
    {
        _isLocked = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
