using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public string[] scenesToUnload;
    public string[] scenesToLoad;

    public Vector3 playerSpawnPosition; // куда телепортировать игрока

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

        // 🎯 Активная сцена
        if (lastLoadedScene.IsValid())
        {
            SceneManager.SetActiveScene(lastLoadedScene);
        }

        // ⏳ даём Unity 1 кадр, чтобы сцена точно инициализировалась
        yield return null;

        // 👤 ищем игрока и телепортируем
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            player.transform.position = playerSpawnPosition;
        }
        else
        {
            Debug.LogWarning("Player not found in scene!");
        }
    }
}