﻿using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace traVRsal.SDK
{
    public class SetupUI : BasicEditorUI
    {
        private string levelName;

        [MenuItem("traVRsal/Setup", priority = 100)]
        public static void ShowWindow()
        {
            GetWindow<SetupUI>("traVRsal Setup");
        }

        public override void OnGUI()
        {
            base.OnGUI();

            GUILayout.Label("Levels are the basic building block. Single levels can either be shared with others on traVRsal or combined with other levels into your own game.", EditorStyles.wordWrappedLabel);

            GUILayout.Space(10);
            levelName = EditorGUILayout.TextField("Level Key: ", levelName);
            if (GUILayout.Button("Create New Level")) CreateSampleLevel();

            GUILayout.Space(10);
            GUILayout.Label("Maintenance Functions", EditorStyles.boldLabel);
            if (GUILayout.Button("Update/Restore Tiled Data")) RestoreTiled();

            OnGUIDone();
        }

        private string GetLevelPath()
        {
            return GetLevelsRoot() + "/" + levelName;
        }

        private void CreateSampleLevel()
        {
            if (string.IsNullOrEmpty(levelName))
            {
                EditorUtility.DisplayDialog("Invalid Entry", "No level key specified.", "OK");
                return;
            }
            if (!IsValidLevelName(levelName))
            {
                EditorUtility.DisplayDialog("Invalid Entry", "Level key is not valid: must be upper and lower case characters, numbers and undercore only.", "OK");
                return;
            }

            if (CreateLevelsRoot()) RestoreTiled();
            CreateSampleWorld();

            EditorUtility.FocusProjectWindow();
            Object obj = AssetDatabase.LoadAssetAtPath<Object>("Assets/Levels/" + levelName);
            Selection.activeObject = obj;
            EditorGUIUtility.PingObject(obj);

            levelName = "";
        }

        private bool IsValidLevelName(string levelName)
        {
            Regex allowed = new Regex("[^a-zA-Z0-9_]");
            return levelName != "_" && !allowed.IsMatch(levelName);
        }

        private bool CreateLevelsRoot()
        {
            if (!Directory.Exists(GetLevelsRoot()))
            {
                Directory.CreateDirectory(GetLevelsRoot());
                AssetDatabase.Refresh();

                return true;
            }
            else
            {
                return false;
            }
        }

        private void RestoreTiled()
        {
            string tiledPath = GetLevelsRoot() + "/_Tiled";

            if (Directory.Exists(tiledPath)) Directory.Delete(tiledPath, true);
            AssetDatabase.Refresh();

            string id = AssetDatabase.FindAssets("_Tiled")[0];
            string path = AssetDatabase.GUIDToAssetPath(id);
            AssetDatabase.CopyAsset(path, tiledPath);
            AssetDatabase.Refresh();
        }

        private bool CreateSampleWorld()
        {
            if (!Directory.Exists(GetLevelPath()))
            {
                string id = AssetDatabase.FindAssets("_Level")[0];
                string path = AssetDatabase.GUIDToAssetPath(id);
                AssetDatabase.CopyAsset(path, GetLevelPath());
                AssetDatabase.Refresh();

                return true;
            }
            else
            {
                Debug.LogError("Level with identical name already exists.");
                return false;
            }
        }
    }
}