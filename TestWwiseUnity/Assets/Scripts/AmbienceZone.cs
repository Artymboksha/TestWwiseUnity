using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AkEnvironment))]
public class AmbienceZone : MonoBehaviour
{
    public AK.Wwise.Event ambienceEvent;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void Start()
    {
        StartCoroutine(CheckIfPlayerAlreadyInside());
    }

    private IEnumerator CheckIfPlayerAlreadyInside()
    {
        // даём физике и сцене "устаканиться"
        yield return null;

        Collider trigger = GetComponent<Collider>();
        Collider[] hits = Physics.OverlapBox(
            trigger.bounds.center,
            trigger.bounds.extents,
            trigger.transform.rotation
        );

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                Activate(hit);
                yield break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        Activate(other);
    }

    private void Activate(Collider player)
    {
        AmbienceGlobalController.Instance?.SetAmbient(
            ambienceEvent,
            player.gameObject
        );
    }
}