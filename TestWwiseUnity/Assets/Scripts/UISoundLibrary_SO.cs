using UnityEngine;
using System;
using System.Collections.Generic;
using WwiseEvent = AK.Wwise.Event;

[CreateAssetMenu(menuName = "Audio/UI Sounds")]
public class UISoundLibrary : ScriptableObject
{
    [Serializable]
    public struct Entry
    {
        public string key;
        public WwiseEvent sound;
    }

    [SerializeField]
    private List<Entry> sounds = new List<Entry>();

    private Dictionary<string, WwiseEvent> lookup;

    private void OnEnable()
    {
        lookup = new Dictionary<string, WwiseEvent>();

        foreach (var entry in sounds)
        {
            if (!lookup.ContainsKey(entry.key))
                lookup.Add(entry.key, entry.sound);
        }
    }

    public WwiseEvent Get(string key)
    {
        if (lookup.TryGetValue(key, out var sound))
            return sound;

        Debug.LogWarning($"UI sound not found: {key}");
        return null;
    }
}