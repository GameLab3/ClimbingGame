using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class SoundDatabaseManagerWindow : EditorWindow
{
    private SoundDatabase _soundDatabase;
    private SerializedObject serializedLibrary;
    private ReorderableList sfxReorderableList;
    private ReorderableList musicReorderableList;
    private Vector2 scrollPosition;

    [MenuItem("Tools/Sound Categorizer")]
    public static void ShowWindow()
    {
        GetWindow<SoundDatabaseManagerWindow>("Sound Categorizer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Target Asset", EditorStyles.boldLabel);

        EditorGUI.BeginChangeCheck();
        _soundDatabase = (SoundDatabase)EditorGUILayout.ObjectField("Library Asset", _soundDatabase, typeof(SoundDatabase), false);

        // Re-initialize lists when target asset changes
        if (EditorGUI.EndChangeCheck() || (_soundDatabase != null && (serializedLibrary == null || serializedLibrary.targetObject != _soundDatabase)))
        {
            InitializeLists();
        }

        if (_soundDatabase == null || serializedLibrary == null)
        {
            EditorGUILayout.HelpBox("Assign a Sound Library asset to edit its contents.", MessageType.Info);
            return;
        }
        
        if (GUILayout.Button("Generate SoundID Enum", GUILayout.Height(30)))
        {
            SoundEnumGenerator.GenerateSoundIDEnum(_soundDatabase);
        }

        // Fetch latest asset values
        serializedLibrary.Update();

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        EditorGUILayout.Space();
        sfxReorderableList?.DoLayoutList();

        EditorGUILayout.Space();
        musicReorderableList?.DoLayoutList();

        EditorGUILayout.EndScrollView();

        // Save modifications and auto-register Undo
        serializedLibrary.ApplyModifiedProperties();
    }

    private void InitializeLists()
    {
        if (_soundDatabase == null) return;

        serializedLibrary = new SerializedObject(_soundDatabase);

        SerializedProperty sfxProp = serializedLibrary.FindProperty("sfxList");
        SerializedProperty musicProp = serializedLibrary.FindProperty("musicList");

        sfxReorderableList = BuildList(serializedLibrary, sfxProp, "SFX Items");
        musicReorderableList = BuildList(serializedLibrary, musicProp, "Music Items");

        // Connect CreateSound to SFX list + button
        sfxReorderableList.onAddCallback = (ReorderableList list) =>
        {
            Undo.RecordObject(_soundDatabase, "Add SFX");
            _soundDatabase.sfxList.Add(Sound.CreateSound("New SFX"));
            EditorUtility.SetDirty(_soundDatabase);
        };

        // Connect CreateMusic to Music list + button
        musicReorderableList.onAddCallback = (ReorderableList list) =>
        {
            Undo.RecordObject(_soundDatabase, "Add Music");
            _soundDatabase.musicList.Add(Sound.CreateMusic("New Music"));
            EditorUtility.SetDirty(_soundDatabase);
        };
    }

    private ReorderableList BuildList(SerializedObject so, SerializedProperty property, string headerName)
    {
        ReorderableList list = new ReorderableList(so, property, true, true, true, true);

        list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, headerName, EditorStyles.boldLabel);
        };

        // Dynamically height-adjust based on foldout/child fields inside each Sound item
        list.elementHeightCallback = (int index) =>
        {
            SerializedProperty element = property.GetArrayElementAtIndex(index);
            return EditorGUI.GetPropertyHeight(element, true) + 6f;
        };

        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            SerializedProperty element = property.GetArrayElementAtIndex(index);
            rect.y += 3f;
            
            // Draw default full inspector view for the Sound object, including nested arrays
            EditorGUI.PropertyField(
                new Rect(rect.x, rect.y, rect.width, EditorGUI.GetPropertyHeight(element, true)),
                element,
                new GUIContent(element.FindPropertyRelative("name").stringValue != "" ? element.FindPropertyRelative("name").stringValue : $"Sound {index}"),
                true
            );
        };

        return list;
    }
}
