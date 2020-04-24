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
    static public Dictionary<string, List<TestMethod>> sourceFileToMethods = new Dictionary<string, List<TestMethod>>();

    [MenuItem("Absolute Unit/Test Runner")]
    static void Init()
    {
        EditorWindow.GetWindow<TestRunner>("Test Runner");
        Refresh();
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();

        GUIStyle style = new GUIStyle();
        style.alignment = TextAnchor.MiddleCenter;
        style.fontStyle = FontStyle.Bold;
        style.normal.textColor = new Color(0.4f, 0, 0.4f);

        foreach (string file in sourceFileToMethods.Keys)
        {
            GUILayout.Label("\n" + file, style);
            foreach (TestMethod t in sourceFileToMethods[file])
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(t.methodName);
                GUILayout.Label(t.GetTestResult() + " ", t.GetTestResultTextColor());
                EditorGUILayout.EndHorizontal();

                if(t.GetResultMessage() != "" && t.GetResultMessage() != null && t.GetResultMessage() != " ")
                    EditorGUILayout.HelpBox(t.GetResultMessage(), MessageType.None);
            }
        }

        GUI.color = Color.cyan;
        if (GUILayout.Button("Refresh"))
        {
            Refresh();
        }

        GUI.color = Color.green;
        if (GUILayout.Button("Run Tests"))
        {
            TestAllMethods();
        }

        EditorGUILayout.EndVertical();
    }

    private static void Refresh()
    {
        sourceFileToMethods.Clear();

        Type[] types = Assembly.GetExecutingAssembly().GetTypes();
        Type[] testObjectTypes = (from Type type in types where type.IsSubclassOf(typeof(TestObject)) select type).ToArray();

        foreach (Type t in testObjectTypes)
        {
            foreach (var method in t.GetMethods())
            {
                var attributes = method.GetCustomAttributes(typeof(Test), true);

                if (attributes.Length > 0)
                {
                    if(!sourceFileToMethods.ContainsKey(t.Name))
                    {
                        sourceFileToMethods.Add(t.Name, new List<TestMethod>());
                    }

                    sourceFileToMethods[t.Name].Add(new TestMethod(method.Name, method));
                }
            }

        }
    }

    private void TestAllMethods()
    {
        foreach(string file in sourceFileToMethods.Keys)
        {
            foreach (TestMethod t in sourceFileToMethods[file])
            {
                t.Test();
            }
        }
    }

    public class TestMethod
    {
        public string methodName;
        public MethodInfo method;
        Result result = new Result("", TestResult.NotTested);

        public TestMethod(string _methodName, MethodInfo _method)
        {
            methodName = _methodName;
            method = _method;
        }

        public TestResult GetTestResult()
        {
            return result.testResult;
        }

        public GUIStyle GetTestResultTextColor()
        {
            Color color = Color.black;

            switch(result.testResult)
            {
                case TestResult.Fail:
                    color = Color.red;
                    break;
                case TestResult.Inconclusive:
                    color = new Color(0.6f, 0f, 0.6f);
                    break;
                case TestResult.NotTested:
                    color = Color.blue;
                    break;
                case TestResult.Pass:
                    color = new Color(0f, 0.6f, 0f);
                    break;
                case TestResult.Warning:
                    color = new Color(0.6f, 0.6f, 0f);
                    break;
            }

            GUIStyle style = new GUIStyle();
            style.normal.textColor = color;
            style.alignment = TextAnchor.UpperRight;

            return style;
        }

        public string GetResultMessage()
        {
            return result.message;
        }

        public void Test()
        {
            result = (Result)method.Invoke(null, null);
        }
    }
}
