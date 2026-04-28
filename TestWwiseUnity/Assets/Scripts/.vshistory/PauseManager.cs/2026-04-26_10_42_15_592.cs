using UnityEngine;
using UnityEngine.Events;

public class PauseManager : MonoBehaviour
{
    public GameObject uiElement;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent onPauseChanged; // событие

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool isActive = !uiElement.activeSelf;
            uiElement.SetActive(isActive);

            // вызываем событие
            onPauseChanged.Invoke(isActive);

            if (isActive)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}