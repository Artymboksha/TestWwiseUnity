using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Меняет Wwise Switch по тэгу объекта, вошедшего/вышедшего в триггер.
/// Свич применяется на MainCamera.
/// Повесить на GameObject с Collider (isTrigger = true).
/// </summary>
public class WwiseSwitchByTagTrigger : MonoBehaviour
{
    [Serializable]
    public struct TagSwitchPair
    {
        public string tag;
        public AK.Wwise.Switch switchValue;
    }

    [Header("Настройки Switch Game Object")]
    [Tooltip("Если не назначено вручную — берётся Camera.main")]
    [SerializeField] private GameObject switchGameObject;

    [Header("Свичи по тэгам (OnTriggerEnter)")]
    [SerializeField] private List<TagSwitchPair> enterSwitches = new List<TagSwitchPair>();

    [Header("Свичи по тэгам (OnTriggerExit)")]
    [SerializeField] private List<TagSwitchPair> exitSwitches = new List<TagSwitchPair>();

    [Header("Отладка")]
    [SerializeField] private bool logDebug = false;

    private Dictionary<string, AK.Wwise.Switch> _enterMap;
    private Dictionary<string, AK.Wwise.Switch> _exitMap;

    private void Awake()
    {
        _enterMap = BuildMap(enterSwitches);
        _exitMap = BuildMap(exitSwitches);

        ResolveSwitchGameObject();
    }

    private void ResolveSwitchGameObject()
    {
        if (switchGameObject == null)
        {
            if (Camera.main != null)
            {
                switchGameObject = Camera.main.gameObject;

                if (logDebug)
                    Debug.Log($"[WwiseSwitchByTagTrigger] Switch будет применяться на MainCamera: {switchGameObject.name}");
            }
            else
            {
                Debug.LogWarning("[WwiseSwitchByTagTrigger] MainCamera не найдена (Camera.main == null). " +
                                  "Проверьте, что камера с тэгом 'MainCamera' есть на сцене.");
            }
        }
    }

    private Dictionary<string, AK.Wwise.Switch> BuildMap(List<TagSwitchPair> pairs)
    {
        var map = new Dictionary<string, AK.Wwise.Switch>();
        foreach (var pair in pairs)
        {
            if (string.IsNullOrEmpty(pair.tag))
                continue;

            if (!map.ContainsKey(pair.tag))
                map.Add(pair.tag, pair.switchValue);
        }
        return map;
    }

    private void OnTriggerEnter(Collider other)
    {
        TrySetSwitch(_enterMap, other.tag, "Enter");
    }

    private void OnTriggerExit(Collider other)
    {
        TrySetSwitch(_exitMap, other.tag, "Exit");
    }

    private void TrySetSwitch(Dictionary<string, AK.Wwise.Switch> map, string tag, string phase)
    {
        if (map == null || !map.TryGetValue(tag, out var switchValue))
            return;

        if (switchValue == null || switchValue.IsValid() == false)
        {
            if (logDebug)
                Debug.LogWarning($"[WwiseSwitchByTagTrigger] Switch не задан для тэга '{tag}' ({phase})");
            return;
        }

        if (switchGameObject == null)
        {
            Debug.LogWarning("[WwiseSwitchByTagTrigger] switchGameObject == null (MainCamera не найдена), свич не применён.");
            return;
        }

        switchValue.SetValue(switchGameObject);

        if (logDebug)
            Debug.Log($"[WwiseSwitchByTagTrigger] {phase}: тэг '{tag}' -> switch '{switchValue.Name}' на {switchGameObject.name}");
    }
}