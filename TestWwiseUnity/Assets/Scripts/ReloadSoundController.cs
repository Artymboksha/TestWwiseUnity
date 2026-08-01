using UnityEngine;

public class ReloadSoundController : MonoBehaviour
{
    [Header("Wwise Settings")]
    [SerializeField] private AK.Wwise.Event reloadEvent;

    public void PlayReloadSound()
    {
        reloadEvent?.Post(gameObject);

        Debug.Log("Reload sound played");
    }
}
