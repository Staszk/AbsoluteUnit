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
    static public List<TestMethod> methodList = new List<TestMethod>();

    [MenuItem("Absolute Unit/Test Runner")]
    static void Init()
    {
        EditorWindow.GetWindow<TestRunner>("Test Runner");
        Refresh();
    }

    private void OnGUI()
    {
        GUILayout.Label("Test Methods", EditorStyles.boldLabel);

        EditorGUILayout.BeginVertical();

        for (int i = 0; i < methodList.Count; i++)
        {
            if (methodList[i] != null)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(methodList[i].methodName);
                GUILayout.Space(10);
                EditorGUILayout.LabelField(methodList[i].GetTestResult().ToString(), methodList[i].GetTestResultTextColor());
                EditorGUILayout.EndHorizontal();
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
        Type[] types = Assembly.GetExecutingAssembly().GetTypes();
        Type[] type2 = (from Type t in types where t.IsSubclassOf(typeof(TestObject)) select t).ToArray();

        foreach (Type t2 in type2)
        {
            foreach (var method in t2.GetMethods())
            {
                var attributes = method.GetCustomAttributes(typeof(Test), true);

                if (attributes.Length > 0)
                {
                    methodList.Add(new TestMethod(method.Name, method));
                }
            }

        }
    }

    private void TestAllMethods()
    {
        foreach(TestMethod t in methodList)
        {
            t.Test();
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
