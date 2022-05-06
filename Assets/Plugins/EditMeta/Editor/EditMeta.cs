using System.Collections;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEditor;

internal class EditMeta : EditorWindow
{
    private string data;
    private string path;
    private Vector2 scroll;

    private void OnEnable()
    {
        data = File.ReadAllText(AssetDatabase.GetAssetPath(Selection.activeObject)+".meta");
        path = AssetDatabase.GetAssetPath(Selection.activeObject)+".meta";
    }

    private void OnGUI()
    {
        scroll = GUILayout.BeginScrollView(scroll);

        data = GUILayout.TextArea(data);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Save"))
        {
            File.WriteAllText(path,data);
            AssetDatabase.Refresh();
        }

        if(GUILayout.Button("Save and Close"))
        {
            File.WriteAllText(path, data);
            AssetDatabase.Refresh();
            GetWindow<EditMeta>().Close();
        }
        GUILayout.EndHorizontal();
        GUILayout.EndScrollView();
    }

    [MenuItem("Assets/Edit Meta/Edit")]
    internal static void Edit()
    {
        if (File.Exists(AssetDatabase.GetAssetPath(Selection.activeObject) + ".meta"))
        {
            GetWindow<EditMeta>();
        }
        else
        {
            EditorUtility.DisplayDialog("Warrning", "No file selected!", "OK");
        } 
    }
    [MenuItem("Assets/Edit Meta/Delete Meta")]
    internal static void DeleteMeta()
    {
        if (File.Exists(AssetDatabase.GetAssetPath(Selection.activeObject) + ".meta"))
        {
           File.Delete(AssetDatabase.GetAssetPath(Selection.activeObject) + ".meta");
           AssetDatabase.Refresh();
        }
        else
        {
            EditorUtility.DisplayDialog("Warrning", "No file selected!", "OK");
        }
    }
    [MenuItem("Assets/Edit Meta/Open to/Notepad")]
    internal static void OpenNotepad()
    {
        if (File.Exists(AssetDatabase.GetAssetPath(Selection.activeObject)+".meta"))
        {
            Process.Start("notepad.exe", "/a " + AssetDatabase.GetAssetPath(Selection.activeObject)+".meta");
        }
        else
        {
            EditorUtility.DisplayDialog("Warrning", "No file selected!", "OK");
        }
    }
    [MenuItem("Assets/Edit Meta/Open to/Notepad++")]
    internal static void OpenNotepadPlus()
    {
        if (File.Exists(AssetDatabase.GetAssetPath(Selection.activeObject)+".meta"))
        {
            Process.Start("notepad++.exe", '"' + AssetDatabase.GetAssetPath(Selection.activeObject)+".meta" + '"');
        }
        else
        {
            EditorUtility.DisplayDialog("Warrning", "No file selected!", "OK");
        }
    }
    [MenuItem("Assets/Edit Meta/Open to/Visual Studio")]
    internal static void OpenVisualStudio()
    {
        if (File.Exists(AssetDatabase.GetAssetPath(Selection.activeObject) + ".meta"))
        {
            Process.Start("devenv.exe","/edit "+ Path.GetFullPath(AssetDatabase.GetAssetPath(Selection.activeObject) + ".meta"));
        }
        else
        {
            EditorUtility.DisplayDialog("Warrning", "No file selected!", "OK");
        }
    }
}
