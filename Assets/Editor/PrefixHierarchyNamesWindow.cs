using UnityEngine;
using UnityEditor;

public class PrefixHierarchyNamesWindow : EditorWindow
{
    private string prefix = "prefix_";
    private bool focusPrefixTextField = true;

    private static readonly string windowTitleText = "Prefix Hierarchy Names";
    private static readonly string prefixTextFieldName = "PrefixField";
    private static readonly string prefixLabelText = "Enter prefix:";
    private static readonly string okButtonText = "OK";
    private static readonly string cancelButtonText = "Cancel";
    private static readonly string undoName = "Prefix added to GameObject name";

    int selectedReplacementOption;
    private static string[] replacementOptions = new string[] { "Selected", "All" };

    [MenuItem("Tools/Renaming/Prefix Hierarchy Names")]
    public static void ShowWindow() 
    {
        Rect windowRect = new Rect(0, 0, 200, 90);
        GetWindowWithRect(typeof(PrefixHierarchyNamesWindow), windowRect, true, windowTitleText);
    }

    private void OnGUI()
    {
        GUILayout.Label(prefixLabelText);
        GUI.SetNextControlName(prefixTextFieldName);
        prefix = EditorGUILayout.TextField(prefix);

        if (focusPrefixTextField)
        {
            EditorGUI.FocusTextInControl(prefixTextFieldName);
            focusPrefixTextField = false;
        }

        selectedReplacementOption = GUILayout.SelectionGrid(selectedReplacementOption, replacementOptions, replacementOptions.Length, EditorStyles.radioButton);

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        {
            OKButton();
            CancelButton();
        }
        EditorGUILayout.EndHorizontal();
    }

    private void OKButton()
    {
        if (GUILayout.Button(okButtonText))
        {
            GameObject[] gameObjects;

            if (selectedReplacementOption == 0)
            {
                gameObjects = Selection.gameObjects;
            }
            else
            {
                gameObjects = HierarchyUtil.GetGameObjectsActiveInHierarchy();
            }

            for (int i = 0; i < gameObjects.Length; i++)
            {
                Undo.RegisterCompleteObjectUndo(gameObjects[i], undoName);
                gameObjects[i].name = string.Join(string.Empty, prefix, gameObjects[i].name);
            }

            Close();
        }
    }

    private void CancelButton()
    {
        if (GUILayout.Button(cancelButtonText))
        {
            Close();
        }
    }
}
