using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using System.Reflection;
using System;
using UnityEditor;
using System.Linq;
using AbsoluteUnit;

public class TestRunner : EditorWindow
{
    [MenuItem("Absolute Unit/Test Runner")]
    static void Init()
    {
        Type[] types = Assembly.GetExecutingAssembly().GetTypes();
        Type[] type2 = (from Type t in types where t.IsSubclassOf(typeof(TestObject)) select t).ToArray();

        foreach (Type t2 in type2)
        {
            foreach (var method in t2.GetMethods())
            {
                var attributes = method.GetCustomAttributes(typeof(Test), true);

                if (attributes.Length > 0)
                {
                    Debug.Log(method.Name);
                    Result r = (Result)method.Invoke(null, null);
                    Debug.Log(r.testResult);
                }
            }

        }
    }

    public static List<TestObject> FindAssetsByType()
    {
        List<TestObject> assets = new List<TestObject>();
        string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(TestObject)));
        for (int i = 0; i < guids.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
            UnityEngine.Object asset = AssetDatabase.LoadAssetAtPath(assetPath, typeof(TestObject));
            if (asset != null)
            {
                assets.Add((TestObject)asset);
            }
        }
        return assets;
    }
}
