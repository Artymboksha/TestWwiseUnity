using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public string[] scenesToUnload;
    public string[] scenesToLoad;

    public Vector3 playerSpawnPosition;

    public Image fadeImage;

    private bool isLoading = false;

    public PauseManager pauseManager;

    public void SwitchScenes()
    {
        if (isLoading) return;
        StartCoroutine(SwitchScenesRoutine());
    }

    IEnumerator SwitchScenesRoutine()
    {
        isLoading = true;

        // 🔥 FADE OUT (0 → 1)
        yield return StartCoroutine(Fade(0f, 1f, 1f));


        // 🔻 выгрузка сцен
        foreach (var scene in scenesToUnload)
        {
            if (string.IsNullOrEmpty(scene)) continue;

            Scene s = SceneManager.GetSceneByName(scene);

            if (s.isLoaded)
                yield return SceneManager.UnloadSceneAsync(scene);
        }

        // 🔺 загрузка сцен (без дублей)
        HashSet<string> loadedScenes = new HashSet<string>();
        Scene lastLoadedScene = default;

        foreach (var scene in scenesToLoad)
        {
            if (string.IsNullOrEmpty(scene)) continue;

            if (loadedScenes.Contains(scene)) continue;
            loadedScenes.Add(scene);

            Scene s = SceneManager.GetSceneByName(scene);
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

        pauseManager.ForceUnpause();

        // 🔥 FADE IN (1 → 0)
        yield return StartCoroutine(Fade(1f, 0f, 2f));

        isLoading = false;
    }

    IEnumerator Fade(float from, float to, float duration)
    {
        float t = 0f;

        Color c = fadeImage.color;

        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            float a = Mathf.Lerp(from, to, t / duration);

            fadeImage.color = new Color(c.r, c.g, c.b, a);

            yield return null;
        }

        fadeImage.color = new Color(c.r, c.g, c.b, to);
    }
}