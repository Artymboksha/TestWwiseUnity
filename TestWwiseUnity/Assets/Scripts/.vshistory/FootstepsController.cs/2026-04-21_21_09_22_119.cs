using UnityEngine;

public class FootstepsController : MonoBehaviour
{
    [Header("Wwise Settings")]
    [SerializeField] private AK.Wwise.Event footstepEvent; // Событие шага

    // Этот метод мы будем вызывать из анимации
    public void PlayFootstepSound()
    {
        // Проверяем, назначено ли событие в инспекторе
        if (footstepEvent != null)
        {
            footstepEvent.Post(gameObject);
        }
        else
        {
            Debug.LogWarning("Wwise Event для шагов не назначен в скрипте FootstepsController!");
        }
    }
}