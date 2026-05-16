using UnityEngine;
using System;
using AK.Wwise;

public class PauseManager : MonoBehaviour
{
    public GameObject uiElement;

    public static event Action<bool> OnPauseChanged;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool isActive = !uiElement.activeSelf;
            uiElement.SetActive(isActive);

            Time.timeScale = isActive ? 0f : 1f;

            OnPauseChanged?.Invoke(isActive);

            if (isActive)
            {
                AkUnitySoundEngine.SetState("Pause", "On");
                AudioBootstrap.Instance.PlayUI("OpenMenu", gameObject);
            }
            else
            {
                AkUnitySoundEngine.SetState("Pause", "None");
                AudioBootstrap.Instance.PlayUI("СloseMenu", gameObject);
            }


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

    public void ForceUnpause()
    {
        uiElement.SetActive(false);
        Time.timeScale = 1f;

        AkUnitySoundEngine.SetState("Pause", "None");

        OnPauseChanged?.Invoke(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}