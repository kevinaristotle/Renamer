using UnityEngine;
using UnityEditor;

public class SearchAndReplaceHierarchyNamesWindow : EditorWindow
{
    string searchText = string.Empty;
    string replaceText = string.Empty;

    private static readonly string windowTitle = "Search and Replace Hierarchy Names";
    private static readonly string searchLabel = "Search for:";
    private static readonly string replaceLabel = "Replace with:";
    private static readonly string replaceButtonLabel = "Replace";
    private static readonly string applyButtonLabel = "Apply";
    private static readonly string closeButtonLabel = "Close";
    private static readonly string undoName = "GameObject name changed";

    int selectedReplacementOption;
    private static string[] replacementOptions = new string[] { "Selected", "All"};

    [MenuItem("Tools/Renaming/Search and Replace Hierarchy Names")]
    public static void ShowWindow()
    {
        Rect windowRect = new Rect(0, 0, 320, 90);
        GetWindowWithRect(typeof(SearchAndReplaceHierarchyNamesWindow), windowRect, true, windowTitle);
    }

    private void OnGUI()
    {
        searchText = EditorGUILayout.TextField(searchLabel, searchText);
        replaceText = EditorGUILayout.TextField(replaceLabel, replaceText);

        selectedReplacementOption = GUILayout.SelectionGrid(selectedReplacementOption, replacementOptions, replacementOptions.Length, EditorStyles.radioButton);

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        {
            ReplaceButton();
            ApplyButton();
            CloseButton();
        }
        EditorGUILayout.EndHorizontal();
    }

    private void ReplaceButton()
    {
        if (GUILayout.Button(replaceButtonLabel))
        {
            ReplaceNamesOfGameObjects(searchText, replaceText);
            Close();
        }
    }

    private void ApplyButton()
    {
        if (GUILayout.Button(applyButtonLabel))
        {
            ReplaceNamesOfGameObjects(searchText, replaceText);
        }
    }

    private void CloseButton()
    {
        if (GUILayout.Button(closeButtonLabel))
        {
            Close();
        }
    }

    private void ReplaceNamesOfGameObjects(string searchText, string replaceText)
    {
        GameObject[] gameObjects;

        if (selectedReplacementOption == 0)
        {
            gameObjects = Selection.gameObjects;
        } else
        {
            gameObjects = HierarchyUtil.GetGameObjectsActiveInHierarchy();
        }

        for (int i = 0; i < gameObjects.Length; i++)
        {
            Undo.RegisterCompleteObjectUndo(gameObjects[i], undoName);
            gameObjects[i].name = gameObjects[i].name.Replace(searchText, replaceText);
        }
    }
}
