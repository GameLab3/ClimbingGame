using UnityEngine;

public class S_CheckPoint_IS : MonoBehaviour
{
    private S_PlayerMovement_IS _player;
    private bool _isActivated;
    
    private void Start()
    {
        if (_player == null)
        {
            _player = FindFirstObjectByType<S_PlayerMovement_IS>();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (_isActivated) return;
        _isActivated = true;
        _player.SetCheckPoint(this);
    }
}
