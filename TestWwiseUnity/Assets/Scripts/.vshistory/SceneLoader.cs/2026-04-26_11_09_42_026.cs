using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SceneLoader : MonoBehaviour
{
    public string[] scenesToUnload;
    public string[] scenesToLoad;

    public Vector3 playerSpawnPosition;

    private bool isLoading = false;

    public void SwitchScenes()
    {
        if (isLoading) return;

        StartCoroutine(SwitchScenesRoutine());
    }

    IEnumerator SwitchScenesRoutine()
    {
        isLoading = true;

        // 🔻 Выгрузка сцен (безопасно)
        foreach (var scene in scenesToUnload)
        {
            if (string.IsNullOrEmpty(scene)) continue;

            Scene s = SceneManager.GetSceneByName(scene);

            if (s.isLoaded)
            {
                yield return SceneManager.UnloadSceneAsync(scene);
            }
        }

        // 🔺 Загрузка сцен БЕЗ ДУБЛЕЙ
        HashSet<string> loadedScenes = new HashSet<string>();

        Scene lastLoadedScene = default;

        foreach (var scene in scenesToLoad)
        {
            if (string.IsNullOrEmpty(scene)) continue;

            // ❌ защита от дублей в массиве
            if (loadedScenes.Contains(scene)) continue;

            loadedScenes.Add(scene);

            Scene s = SceneManager.GetSceneByName(scene);

            // ❌ если уже загружена — не грузим повторно
            if (s.isLoaded) continue;

            yield return SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

            lastLoadedScene = SceneManager.GetSceneByName(scene);
        }

        if (lastLoadedScene.IsValid())
        {
            SceneManager.SetActiveScene(lastLoadedScene);
        }

        yield return null;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            player.transform.position = playerSpawnPosition;
        }

        isLoading = false;
    }
}