using UnityEngine;
using System;

public class PauseManager : MonoBehaviour
{
    public GameObject uiElement;

    public static event Action<bool> OnPauseChanged; // глобальное событие

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool isActive = !uiElement.activeSelf;
            uiElement.SetActive(isActive);

            OnPauseChanged?.Invoke(isActive);

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