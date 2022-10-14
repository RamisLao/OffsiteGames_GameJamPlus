using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public class RuntimeSetWindow : EditorWindow
{
    private string _type;
    private string _uppercase;
    private string _path = "Assets/Scripts/Helpers/Custom/RuntimeSets/RuntimeSetTypes/";

    [MenuItem("Custom/RuntimeSet/Create New")]
    public static void ShowWindow()
    {
        GetWindow(typeof(RuntimeSetWindow));
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Enter a valid C# type with no spaces");
        string input = EditorGUILayout.TextField("Type", _type);
        if (input != null) _type = input.Trim();
        EditorGUILayout.LabelField("Enter path to RuntimeSet scripts or use default");
        string path = EditorGUILayout.TextField("Path", _path);
        if (path != null) _path = path.Trim();

        if(GUILayout.Button("Generate"))
        {
            _uppercase = StringFormatting.FirstLetterToUpper(_type);
            CreateRuntimeSet();
            CreateAddToRuntimeSet();
            AssetDatabase.Refresh();
        }
    }

    private void CreateRuntimeSet()
    {
        string path = $"{_path}RuntimeSet{_uppercase}.cs";
        Debug.Log($"Creating RuntimeSet: {path}");
        using (StreamWriter outfile = new StreamWriter(path))
        {
            outfile.WriteLine("using UnityEngine;");
            outfile.WriteLine("");
            outfile.WriteLine($"[CreateAssetMenu(menuName = \"RuntimeSets/{_type}\")]");
            outfile.WriteLine($"public class RuntimeSet{_uppercase} : RuntimeSet<{_type}> {{}}");
        }
    }

    private void CreateAddToRuntimeSet()
    {
        string path = $"{_path}AddToRuntimeSet{_uppercase}.cs";
        Debug.Log($"Creating AddToRuntimeSet: {path}");
        using (StreamWriter outfile = new StreamWriter(path))
        {
            outfile.WriteLine("using UnityEngine;");
            outfile.WriteLine("");
            outfile.WriteLine($"public class AddToRuntimeSet{_uppercase} : AddToRuntimeSet<{_type}> {{}}");
        }
    }
}
#endif