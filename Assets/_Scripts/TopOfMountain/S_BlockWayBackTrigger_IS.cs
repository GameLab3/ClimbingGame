using UnityEngine;

public class S_BlockWayBackTrigger_IS : MonoBehaviour
{
    [SerializeField] private GameObject[] blockWayBack;
    private bool _isTriggered = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (_isTriggered) return;
        _isTriggered = true;
        foreach (var block in blockWayBack)
        {
            block.SetActive(true);
        }
    }
}
