using UnityEditor;
using UnityEngine;

public class ProfileIdManagerWindow : EditorWindow
{
    private ProfileIdsDatabase _database;
    private Vector2 _scrollPos;
    private string _newProfileIdInput;


    [MenuItem("Tools/Profile Id Manager")]
    private static void ShowWindow()
    {
        GetWindow<ProfileIdManagerWindow>("Profile Id Manager");
    }

    private void OnEnable()
    {
        LoadProfiles();
    }

    private void LoadProfiles()
    {
        _database = Resources.Load<ProfileIdsDatabase>("ProfileIdsDatabase");
    }
    
    private void OnGUI()
    {
        if (!_database)
        {
            EditorGUILayout.HelpBox("ProfileIdsDatabase asset not found in Resources folder!", MessageType.Error);
            if (GUILayout.Button("Find/Reload Database"))
            {
                LoadProfiles();
            }
            return;
        }

        EditorGUILayout.LabelField("Profile Id Manager", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        // Add new string section
        EditorGUILayout.BeginHorizontal();
        _newProfileIdInput = EditorGUILayout.TextField("New Id:", _newProfileIdInput);
        if (GUILayout.Button("Add", GUILayout.Width(60)))
        {
            if (!string.IsNullOrEmpty(_newProfileIdInput))
            {
                Undo.RecordObject(_database, "Add Id");
                _database.profileIds.Add(_newProfileIdInput);
                _newProfileIdInput = "";
                EditorUtility.SetDirty(_database);
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Existing Ids", EditorStyles.boldLabel);

        // Scrollable list of editable strings
        _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
        for (int i = 0; i < _database.profileIds.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            
            EditorGUI.BeginChangeCheck();
            string updatedValue = EditorGUILayout.TextField($"[{i}]", _database.profileIds[i]);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(_database, "Edit Id");
                _database.profileIds[i] = updatedValue;
                EditorUtility.SetDirty(_database);
            }

            bool itemDeleted = false;
            if (GUILayout.Button("Delete", GUILayout.Width(60)))
            {
                Undo.RecordObject(_database, "Delete Id");
                _database.profileIds.RemoveAt(i);
                EditorUtility.SetDirty(_database);
                itemDeleted = true; // Mark for deletion, but don't break yet
            }

            // End the horizontal group safely before breaking the loop
            EditorGUILayout.EndHorizontal();

            if (itemDeleted)
            {
                break;
            }
        }
        EditorGUILayout.EndScrollView();
    }
    
}
