using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class PlayerDataEditor
{

    public string Name;

    [MenuItem("Assets/Create/PlayerData")]
    public static void CreateData()
    {
        PlayerDataAsset asset = ScriptableObject.CreateInstance<PlayerDataAsset>();

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (path == "")
        {
            path = "Assets";
        }
        else if (Path.GetExtension(path) != "")
        {
            path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
        }

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + typeof(PlayerDataAsset).ToString() + ".asset");

        AssetDatabase.CreateAsset(asset, assetPathAndName);

        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}
