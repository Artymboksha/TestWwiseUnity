using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public string[] scenesToUnload; // сцены для выгрузки
    public string[] scenesToLoad;   // сцены для загрузки

    public void SwitchScenes()
    {
        StartCoroutine(SwitchScenesRoutine());
    }

    IEnumerator SwitchScenesRoutine()
    {
        // 🔻 Выгрузка сцен
        foreach (var scene in scenesToUnload)
        {
            if (!string.IsNullOrEmpty(scene) && SceneManager.GetSceneByName(scene).isLoaded)
            {
                yield return SceneManager.UnloadSceneAsync(scene);
            }
        }

        // 🔺 Загрузка сцен
        Scene lastLoadedScene = default;

        foreach (var scene in scenesToLoad)
        {
            if (!string.IsNullOrEmpty(scene))
            {
                yield return SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
                lastLoadedScene = SceneManager.GetSceneByName(scene);
            }
        }

        // 🎯 Делаем последнюю загруженную сцену активной
        if (lastLoadedScene.IsValid())
        {
            SceneManager.SetActiveScene(lastLoadedScene);
        }
    }
}