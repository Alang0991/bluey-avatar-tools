using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using VRC.SDK3.Dynamics.PhysBone.Components;

public class BlueyWindow : EditorWindow
{
    GameObject avatar;
    GameObject questClone;

    bool editOriginal = true;

    Vector2 scroll;

    List<PhysBoneItem> physbones = new List<PhysBoneItem>();
    List<TextureItem> textures = new List<TextureItem>();

    [MenuItem("Tools/Bluey Avatar Tools")]
    public static void ShowWindow()
    {
        GetWindow<BlueyWindow>("Bluey");
    }

    void OnGUI()
    {
        scroll = EditorGUILayout.BeginScrollView(scroll);

        GUILayout.Space(10);
        GUILayout.Label("Bluey Avatar Tools", EditorStyles.boldLabel);

        avatar = (GameObject)EditorGUILayout.ObjectField(
            "Avatar",
            avatar,
            typeof(GameObject),
            true
        );

        GUILayout.Space(5);

        editOriginal = EditorGUILayout.Toggle("Edit Original Avatar", editOriginal);

        GUILayout.Space(10);

        if (GUILayout.Button("Scan Avatar", GUILayout.Height(30)))
        {
            ScanAvatar();
        }

        if (GUILayout.Button("Create Quest Clone", GUILayout.Height(30)))
        {
            CreateClone();
        }

        GUILayout.Space(15);

        DrawPhysbones();

        GUILayout.Space(15);

        DrawTextures();

        GUILayout.Space(15);

        if (GUILayout.Button("Save Quest Prefab", GUILayout.Height(35)))
        {
            SavePrefab();
        }

        EditorGUILayout.EndScrollView();
    }

    // ======================
    // TARGET OBJECT
    // ======================

    GameObject GetTarget()
    {
        if (!editOriginal && questClone != null)
            return questClone;

        return avatar;
    }

    // ======================
    // SCAN
    // ======================

    void ScanAvatar()
    {
        physbones.Clear();
        textures.Clear();

        GameObject target = GetTarget();

        if (target == null)
            return;

        foreach (VRCPhysBone pb in target.GetComponentsInChildren<VRCPhysBone>(true))
        {
            physbones.Add(new PhysBoneItem
            {
                physbone = pb,
                name = pb.name
            });
        }

        foreach (Renderer r in target.GetComponentsInChildren<Renderer>(true))
        {
            foreach (Material m in r.sharedMaterials)
            {
                if (m == null) continue;

                Texture2D tex = m.mainTexture as Texture2D;

                if (tex == null) continue;

                textures.Add(new TextureItem
                {
                    texture = tex,
                    name = tex.name,
                    width = tex.width,
                    height = tex.height
                });
            }
        }

        Debug.Log("Avatar scanned.");
    }

    // ======================
    // CLONE
    // ======================

    void CreateClone()
    {
        if (avatar == null)
        {
            Debug.LogWarning("No avatar selected.");
            return;
        }

        questClone = Instantiate(avatar);
        questClone.name = avatar.name + "_Quest";

        Selection.activeGameObject = questClone;

        Debug.Log("Quest clone created.");

        ScanAvatar();
    }

    // ======================
    // PHYSBONES UI
    // ======================

    void DrawPhysbones()
    {
        GUILayout.Label("PhysBones", EditorStyles.boldLabel);

        if (physbones.Count == 0)
        {
            GUILayout.Label("No PhysBones found.");
            return;
        }

        foreach (var p in physbones)
        {
            GUILayout.BeginHorizontal("box");

            p.selected = GUILayout.Toggle(p.selected, "", GUILayout.Width(20));

            GUILayout.Label(p.name);

            GUILayout.EndHorizontal();
        }

        GUILayout.Space(5);

        if (GUILayout.Button("Remove Selected PhysBones"))
        {
            RemoveSelectedPhysbones();
        }

        if (GUILayout.Button("Remove ALL PhysBones"))
        {
            RemoveAllPhysbones();
        }
    }

    // ======================
    // REMOVE PHYSBONES
    // ======================

    void RemoveSelectedPhysbones()
    {
        GameObject target = GetTarget();

        int removed = 0;

        foreach (var p in physbones)
        {
            if (!p.selected) continue;

            if (p.physbone != null)
            {
                Undo.DestroyObjectImmediate(p.physbone);
                removed++;
            }
        }

        Debug.Log("Removed PhysBones: " + removed);

        ScanAvatar();
    }

    void RemoveAllPhysbones()
    {
        GameObject target = GetTarget();

        if (target == null)
            return;

        int removed = 0;

        foreach (var pb in target.GetComponentsInChildren<VRCPhysBone>(true))
        {
            Undo.DestroyObjectImmediate(pb);
            removed++;
        }

        Debug.Log("Removed ALL PhysBones: " + removed);

        ScanAvatar();
    }

    // ======================
    // TEXTURES
    // ======================

    void DrawTextures()
    {
        GUILayout.Label("Textures", EditorStyles.boldLabel);

        if (textures.Count == 0)
        {
            GUILayout.Label("No textures found.");
            return;
        }

        foreach (var t in textures)
        {
            GUILayout.BeginHorizontal("box");

            t.selected = GUILayout.Toggle(t.selected, "", GUILayout.Width(20));

            GUILayout.Label(t.name);

            GUILayout.FlexibleSpace();

            GUILayout.Label($"{t.width}x{t.height}");

            GUILayout.EndHorizontal();
        }

        GUILayout.Space(5);

        if (GUILayout.Button("Reduce Selected Textures"))
        {
            ReduceTextures();
        }
    }

    // ======================
    // REDUCE TEXTURES
    // ======================

    void ReduceTextures()
    {
        foreach (var t in textures)
        {
            if (!t.selected)
                continue;

            string path = AssetDatabase.GetAssetPath(t.texture);

            TextureImporter importer =
                AssetImporter.GetAtPath(path) as TextureImporter;

            if (importer != null)
            {
                importer.maxTextureSize = 1024;
                importer.SaveAndReimport();
            }
        }

        Debug.Log("Selected textures reduced.");
    }

    // ======================
    // SAVE PREFAB
    // ======================

    void SavePrefab()
    {
        if (questClone == null)
        {
            Debug.LogWarning("No Quest clone exists.");
            return;
        }

        string folder = "Assets/QuestOptimized";

        if (!AssetDatabase.IsValidFolder(folder))
            AssetDatabase.CreateFolder("Assets", "QuestOptimized");

        string path = folder + "/" + questClone.name + ".prefab";

        PrefabUtility.SaveAsPrefabAsset(questClone, path);

        AssetDatabase.Refresh();

        Debug.Log("Quest prefab saved.");
    }

    // ======================
    // DATA
    // ======================

    class PhysBoneItem
    {
        public bool selected;
        public string name;
        public VRCPhysBone physbone;
    }

    class TextureItem
    {
        public bool selected;
        public Texture2D texture;
        public string name;
        public int width;
        public int height;
    }
}