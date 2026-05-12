using UnityEngine;

public class WwiseEventTrigger : MonoBehaviour
{
    public AK.Wwise.Event wwiseEvent;

    public TriggerType triggerOn = TriggerType.Start;

    public enum TriggerType
    {
        Start,
        Destroy
    }

    private void Start()
    {
        if (triggerOn == TriggerType.Start)
        {
            PlayEvent();
        }
    }

    private void OnDestroy()
    {
        if (triggerOn == TriggerType.Destroy)
        {
            PlayEvent();
        }
    }

    private void PlayEvent()
    {
        if (wwiseEvent != null)
        {
            wwiseEvent.Post(gameObject);
        }
    }
}