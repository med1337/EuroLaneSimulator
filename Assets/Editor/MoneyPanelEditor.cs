using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MoneyPanel))]
public class MoneyPanelEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        if (GUILayout.Button("$100 Cash Bonus"))
        {
            if (!EditorApplication.isPlaying)
                return;

            var t = (MoneyPanel)target;
            t.LogTransaction(100, "Debug Cash Bonus");
        }

        if (GUILayout.Button("$100 Cash Penalty"))
        {
            if (!EditorApplication.isPlaying)
                return;

            var t = (MoneyPanel)target;
            t.LogTransaction(-100, "Debug Cash Penalty");
        }
    }

}
