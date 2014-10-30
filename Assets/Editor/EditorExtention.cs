using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class EditorExtention : Editor
{
    public static string GetGameObjectPath(GameObject obj)
    {
        string path = "/" + obj.name;
        while (obj.transform.parent != null)
        {
            obj = obj.transform.parent.gameObject;
            path = "/" + obj.name + path;
        }
        return path;
    }

    [MenuItem("Assets/Extention/Check Selected Prefab Usage In All Scenes", false, 1024)]
    private static void OnSearchForPrefabReferences()
    {
        //确保鼠标右键选择的是一个Prefab
        if (Selection.gameObjects.Length != 1)
        {
            return;
        }

        //遍历所有游戏场景
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                //打开场景
                EditorApplication.OpenScene(scene.path);
                //获取场景中的所有游戏对象
                GameObject[] gos = (GameObject[])FindObjectsOfType(typeof(GameObject));
                foreach (GameObject go in gos)
                {
                    //判断GameObject是否为一个Prefab的引用
                    if (PrefabUtility.GetPrefabType(go) == PrefabType.PrefabInstance)
                    {
                        UnityEngine.Object parentObject = PrefabUtility.GetPrefabParent(go);
                        string path = AssetDatabase.GetAssetPath(parentObject);
                        //判断GameObject的Prefab是否和右键选择的Prefab是同一路径。
                        if (path == AssetDatabase.GetAssetPath(Selection.activeGameObject))
                        {
                            //输出场景名，以及Prefab引用的路径
                            Debug.Log(scene.path + GetGameObjectPath(go));
                        }
                    }
                }
            }
        }
    }
    //[MenuItem("Assets/Extention/Check Selected Script Usage In All Scenes", false, 1024)]
    private static void OnSearchForScriptReference()
    {
        //当前有选择物品
        if (Selection.objects.Length != 1)
        {
            return;
        }

        //遍历所有游戏场景
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                //打开场景
                EditorApplication.OpenScene(scene.path);
                //获取场景中的所有游戏对象
                GameObject[] gos = (GameObject[])FindObjectsOfType(typeof(GameObject));
                foreach (GameObject go in gos)
                {
                    if (go.name == "Coin Camera")
                    {
                         Debug.Log("Object name is " + go.name);
                    }
                    //获取gameObject的所有Component
                    Component[] components = go.GetComponents<Component>();
                    foreach (Component component in components)
                    {
                        //获取对象的路径
                        string path = AssetDatabase.GetAssetPath(component);
                        //判断路径是否相同
                        if (path == AssetDatabase.GetAssetPath(Selection.activeObject))
                        {
                            //输出场景名，以及Prefab引用的路径
                            Debug.Log(scene.path + GetGameObjectPath(go));
                        }
                    }
                }
            }
        }
    }
}