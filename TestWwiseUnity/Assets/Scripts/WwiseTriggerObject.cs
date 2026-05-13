using UnityEngine;

public class WwiseTriggerObject : MonoBehaviour
{
    [Header("Player Tag")]
    [SerializeField] private string playerTag = "Player";

    [Header("Wwise Event")]
    [SerializeField] private string eventName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            AkUnitySoundEngine.PostEvent(eventName, gameObject);
        }
    }
}
