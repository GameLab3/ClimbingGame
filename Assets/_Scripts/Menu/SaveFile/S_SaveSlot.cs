using UnityEngine;

public class S_SaveSlot : MonoBehaviour
{
    [Header("Profile Id")]
    [SerializeField] private string profileId = "";
    [Header("Content")]
    [SerializeField] private GameObject noDataContent;
    [SerializeField] private GameObject hasDataContent;
}
