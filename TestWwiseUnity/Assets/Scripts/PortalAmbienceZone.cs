using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PortalAmbienceZone : MonoBehaviour
{
    [Header("Wwise Events")]
    public AK.Wwise.Event portalAmbienceEvent;
    public AK.Wwise.Event stopEvent;

    [Header("Emitters (where sound will be played)")]
    public GameObject[] emitters;

    private bool playerInside;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void Start()
    {
        StartCoroutine(InitializeZoneState());
    }

    private IEnumerator InitializeZoneState()
    {
        // даём физике и сцене полностью прогрузиться
        yield return null;

        playerInside = IsPlayerInside();

        if (!playerInside)
        {
            PlayPortalAmbience();
        }
    }

    private bool IsPlayerInside()
    {
        Collider trigger = GetComponent<Collider>();

        Vector3 center = trigger.bounds.center;
        Vector3 halfExtents = trigger.bounds.extents;
        Quaternion rotation = trigger.transform.rotation;

        Collider[] hits = Physics.OverlapBox(center, halfExtents, rotation);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
                return true;
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInside = true;
        StopPortalAmbience(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInside = false;
        PlayPortalAmbience();
    }

    private void PlayPortalAmbience()
    {
        if (!portalAmbienceEvent.IsValid())
            return;

        foreach (var emitter in emitters)
        {
            if (emitter == null) continue;
            portalAmbienceEvent.Post(emitter);
        }
    }

    private void StopPortalAmbience(GameObject player)
    {
        if (!stopEvent.IsValid())
            return;

        foreach (var emitter in emitters)
        {
            if (emitter == null) continue;
            stopEvent.Post(emitter);
        }
    }
}