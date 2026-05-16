using UnityEngine;

public class AmbienceGlobalController : MonoBehaviour
{
    public static AmbienceGlobalController Instance { get; private set; }

    private AK.Wwise.Event currentAmbient;
    private uint playingId;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetAmbient(AK.Wwise.Event newAmbient, GameObject emitter)
    {
        if (!newAmbient.IsValid())
            return;

        // Уже играет этот же эмбиенс
        if (currentAmbient != null &&
            currentAmbient.Id == newAmbient.Id)
        {
            return;
        }

        // Остановить старый
        if (playingId != 0)
        {
            AkUnitySoundEngine.StopPlayingID(playingId, 2000);
        }

        // Запустить новый
        currentAmbient = newAmbient;
        playingId = newAmbient.Post(emitter);

        Debug.Log($"Ambient changed to: {newAmbient.Name}");
    }

    private void OnDestroy()
    {
        if (playingId != 0)
        {
            AkUnitySoundEngine.StopPlayingID(playingId);
        }
    }
}