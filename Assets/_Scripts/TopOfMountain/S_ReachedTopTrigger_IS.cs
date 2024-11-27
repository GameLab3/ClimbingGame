using System;
using UnityEngine;

public class S_ReachedTopTrigger_IS : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private S_TurnCamera_IS turnCameraScript;
    [SerializeField] private S_PlayerMovement_IS playerMovementScript;
    [SerializeField] private S_ChangeSaturation_IS changeSaturationScript;

    [Header("Settings")]
    [SerializeField] private float newSaturationMinimum;
    [SerializeField] private bool reachedTop;

    private void Start()
    {
        // If the references are not set in the inspector, find them in the scene
        if (turnCameraScript == null)
        {
            turnCameraScript = FindFirstObjectByType<S_TurnCamera_IS>();
        }
        if (changeSaturationScript == null)
        {
            changeSaturationScript = FindFirstObjectByType<S_ChangeSaturation_IS>();
        }
        if (playerMovementScript == null)
        {
            playerMovementScript = FindFirstObjectByType<S_PlayerMovement_IS>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (reachedTop) return;
        reachedTop = true;
        turnCameraScript.FlipCamera();
        playerMovementScript.ReverseAllControls(true);
        changeSaturationScript.ChangeMinSaturation(newSaturationMinimum);
    }
}
