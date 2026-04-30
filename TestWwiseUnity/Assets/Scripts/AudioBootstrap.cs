using UnityEngine;

public class AudioBootstrap : MonoBehaviour
{
    public static AudioBootstrap Instance { get; private set; }

    [Header("Banks")]
    [SerializeField] private BankList bankList;

    [Header("Libraries")]
    [SerializeField] private UISoundLibrary uiSounds;

    private void Awake()
    {
        // Singleton
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Загрузка банков
        foreach (var bank in bankList.banks)
        {
            AkBankManager.LoadBank(bank, false, false);
        }
    }

    //  Глобальный доступ к UI звукам
    public void PlayUI(string key, GameObject source)
    {
        uiSounds.Get(key)?.Post(source);
    }
}