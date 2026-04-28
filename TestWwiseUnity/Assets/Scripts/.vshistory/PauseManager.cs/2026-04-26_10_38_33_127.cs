using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject uiElement; // Перетащи сюда свой UI объект в инспекторе

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Переключаем активность объекта
            uiElement.SetActive(!uiElement.activeSelf);
        }
    }
}
