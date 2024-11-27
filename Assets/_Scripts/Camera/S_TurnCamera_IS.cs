using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class S_TurnCamera_IS : MonoBehaviour
{
    [Header("Camera Reference")]
    [SerializeField] private CinemachineFollow cameraFollow;

    [Header("Camera Settings")]
    [SerializeField] private float flipTime;
    
    private float _trueFlipTime;

    private bool _isFlipping;

    private void Start()
    {
        _trueFlipTime = flipTime/2;
    }

    public void FlipCamera()
    {
        if (_isFlipping) return;
        _isFlipping = true;
        StartCoroutine(ChangeCameraAngle());
    }

    private IEnumerator ChangeCameraAngle()
    {
        float elapsedTime = 0f;
        Vector3 startRotation = cameraFollow.FollowOffset;
        Vector3 endRotation = new Vector3(-startRotation.x, startRotation.y, -startRotation.z);
        // Flip the x axis
        while (elapsedTime < _trueFlipTime)
        {
            elapsedTime += Time.deltaTime;
            cameraFollow.FollowOffset.x = Mathf.Lerp(startRotation.x, endRotation.x, elapsedTime / _trueFlipTime);
            yield return null;
        }
        elapsedTime = 0f;
        // Flip the z axis
        while (elapsedTime < _trueFlipTime)
        {
            elapsedTime += Time.deltaTime;
            cameraFollow.FollowOffset.z = Mathf.Lerp(startRotation.z, endRotation.z, elapsedTime / _trueFlipTime);
            yield return null;
        }
        _isFlipping = false;
    }
}
