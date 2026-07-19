using UnityEngine;

public class S_KillZone_IS : MonoBehaviour
{
    [SerializeField] private S_PlayerMovementNew_IS playerMovementScript;
    private void Start()
    {
        if (playerMovementScript == null)
        {
            playerMovementScript = FindFirstObjectByType<S_PlayerMovementNew_IS>();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        playerMovementScript.Respawn();
    }
}
