using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using NativeSerializableDictionary;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Variables/Dictionary Variable")]
public class GameObjectDictionaryVariable : VariableObject<Dictionary<string,GameObject>>
{
    [SerializeField] string path;
    public UnityEvent<float, bool> onChangedSigned;
    public SerializableDictionary<string,GameObject> dictionary;

    public GameObjectDictionaryVariable()
    {
        onChangedSigned = new UnityEvent<float, bool>();
    }

    public GameObjectDictionaryVariable AddKeyPair((string, GameObject) keyPair)
    {        
        if(!dictionary.TryAdd(keyPair.Item1, keyPair.Item2))
        {
            Debug.LogError("Attempted to add asset to dictionary which already exists:  Key = " + keyPair.Item1);
            return null;
        }

        return this;
    }

        public GameObjectDictionaryVariable RemoveKeyPair(string key)
    {
        if(!dictionary.Remove(key))
        {
            Debug.LogError("Attempted to Remove asset to dictionary which doesn't exists:  Key = " + key);
            return null;
        }

        return this;
    }

    public static GameObjectDictionaryVariable operator +(GameObjectDictionaryVariable dictionaryVariable, (string, GameObject) keyPair)
    {
        dictionaryVariable.AddKeyPair(keyPair);
        return dictionaryVariable;
    }

        public static GameObjectDictionaryVariable operator -(GameObjectDictionaryVariable dictionaryVariable, string key)
    {
        dictionaryVariable.RemoveKeyPair(key);
        return dictionaryVariable;
    }

    #if UNITY_EDITOR

    [ContextMenu("Populate Dictionary")]
    public void PopulateDictionary()
    {
        Debug.Log(path);
        string[] guids = AssetDatabase.FindAssets("t:GameObject", new []{path});
        Debug.Log(guids.Length);


        dictionary = new SerializableDictionary<string, GameObject>();
        foreach (string file in guids)
        {
            GameObject gameObject = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(file));
            AddKeyPair((gameObject.name, gameObject));
        }
    }
    #endif
}

#if UNITY_EDITOR

[CustomEditor(typeof(GameObjectDictionaryVariable))]
public class GameObjectDictionaryVariableEditor : VariableObjectEditor<Dictionary<string,GameObject>>
{
    GameObjectDictionaryVariableEditor()
    {
        EditorApplication.playModeStateChanged += onPlayEvent;
    }
    private void onPlayEvent(PlayModeStateChange state)
    {
        var script = (GameObjectDictionaryVariable)target;
        if(!script.resetOnPlay) return;
        script.dictionary = new SerializableDictionary<string, GameObject>();
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (GameObjectDictionaryVariable)target;
        if(GUILayout.Button("PopulateDictionary", GUILayout.Height(40)))
        {
            script.PopulateDictionary();
        }
    }
}
#endif