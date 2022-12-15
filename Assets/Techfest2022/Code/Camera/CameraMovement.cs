using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float _panningSpeed = 3f;
    [SerializeField]
    private float _panningMousePositionThreshold = 1f;
    [SerializeField]
    private Vector3 _cameraPanMin = Vector3.zero;
    [SerializeField]
    private Vector3 _cameraPanMax = Vector3.zero;
    [SerializeField]
    private float _zoomSpeedMultiplier = 100f;

    private Vector3 _currentMousePosition = Vector3.zero;
    private Vector3 _currentCamerPosition = Vector3.zero;
    private Vector2 _currentScreenSize = Vector2.zero;
    private Transform _mainCameraTransform = null;

    private bool _allowCameraMovemet = false;
    private bool _isGameInFocus = false;


    public bool AllowPanning => _allowCameraMovemet;
    private void Awake()
    {
        ///Let's fetch the 'Screen' size here and store in _currentScreenSize.
        ///Also, cache the Main Camera's transform component.
        
        _currentCamerPosition = _mainCameraTransform.position;

        TogglePanning(true);
    }

    //private void WhatMagicMethod()
    //{
    //    PanCamera();
    //    ZoomCamera();
    //}


    private void OnApplicationFocus(bool focus)
    {
        _isGameInFocus = focus;
    }
    
    private void ZoomCamera()
    {
        if (!_allowCameraMovemet || !_isGameInFocus)
            return;

        if (Input.mouseScrollDelta.y == 0f)
            return;

        _currentCamerPosition.y -= Mathf.Sign(Input.mouseScrollDelta.y) * _panningSpeed * Time.deltaTime * _zoomSpeedMultiplier;
        ClampCameraZoom();
        _mainCameraTransform.position = _currentCamerPosition;
    }

    private void PanCamera()
    {
        if (!_allowCameraMovemet || !_isGameInFocus)
            return;

        _currentMousePosition = Input.mousePosition;
        if (_currentMousePosition.x < _panningMousePositionThreshold || _currentMousePosition.x > _currentScreenSize.x - _panningMousePositionThreshold)
        {
            _currentCamerPosition.x += Mathf.Sign(_currentMousePosition.x) * _panningSpeed * Time.deltaTime;
            _currentCamerPosition.z -= Mathf.Sign(_currentMousePosition.x) * _panningSpeed * Time.deltaTime;
            ClampCameraPanning();

            _mainCameraTransform.position = _currentCamerPosition;
        }

        if (_currentMousePosition.y < _panningMousePositionThreshold || _currentMousePosition.y > _currentScreenSize.y - _panningMousePositionThreshold)
        {
            _currentCamerPosition.x += Mathf.Sign(_currentMousePosition.y) * _panningSpeed * Time.deltaTime;
            _currentCamerPosition.z += Mathf.Sign(_currentMousePosition.y) * _panningSpeed * Time.deltaTime;

            ClampCameraPanning();

            _mainCameraTransform.position = _currentCamerPosition;
        }
    }

    private void ClampCameraPanning()
    {
        if (_currentCamerPosition.x < _cameraPanMin.x || _currentCamerPosition.x > _cameraPanMax.x
                        || _currentCamerPosition.z < _cameraPanMin.z || _currentCamerPosition.z > _cameraPanMax.z)
        {
            _currentCamerPosition.x = Mathf.Clamp(_currentCamerPosition.x, _cameraPanMin.x, _cameraPanMax.x);
            _currentCamerPosition.z = Mathf.Clamp(_currentCamerPosition.z, _cameraPanMin.z, _cameraPanMax.z);
        }
    }

    private void ClampCameraZoom()
    {
        if (_currentCamerPosition.y < _cameraPanMin.y || _currentCamerPosition.y > _cameraPanMax.y)
        {
            _currentCamerPosition.y = Mathf.Clamp(_currentCamerPosition.y, _cameraPanMin.y, _cameraPanMax.y);
        }
    }

    public void TogglePanning(bool allow)
    {
        _allowCameraMovemet = allow;
    }
}
