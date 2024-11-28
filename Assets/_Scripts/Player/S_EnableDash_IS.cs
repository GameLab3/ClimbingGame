using UnityEngine;

public class S_EnableDash_IS : MonoBehaviour
{
    [SerializeField] private S_PlayerMovement_IS player;
    private bool _isActivated;
    
    private void Start()
    {
        if (player == null)
        {
            player = FindFirstObjectByType<S_PlayerMovement_IS>();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (_isActivated) return;
        _isActivated = true;
        player.DashAllowed(true);
    }
}
