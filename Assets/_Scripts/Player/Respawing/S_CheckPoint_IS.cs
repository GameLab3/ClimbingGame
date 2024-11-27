using UnityEngine;

public class S_CheckPoint_IS : MonoBehaviour
{
    private bool _isActivated;
    
    private void OnTriggerEnter(Collider other)
    {
        if (_isActivated) return;
        _isActivated = true;
        other.GetComponent<S_PlayerMovement_IS>().SetCheckPoint(this);
    }
}
