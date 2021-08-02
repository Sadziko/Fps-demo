using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Damageable))]
[CanEditMultipleObjects]
public class DamageableEditor : Editor
{
    SerializedProperty _targetTypeProp;
    private string[] _choices = new [] { "Pistol", "MachineGun", "Granade" };
    private int _choiceIndex = 0;

    void OnEnable()
    {
        // Setup the SerializedProperties.
        _targetTypeProp = serializedObject.FindProperty("TargetType");
        // Set the choice index to the previously selected index
        _choiceIndex = Array.IndexOf(_choices, _targetTypeProp.stringValue);
    }
    
    public override void OnInspectorGUI ()
    {
        serializedObject.Update();
        
        DrawDefaultInspector();
        EditorGUILayout.Space(10);
        
        var GUIStyleLabel = new GUIStyle();
        GUIStyleLabel.fontStyle = FontStyle.Bold;
        
        EditorGUILayout.LabelField("Target type", GUIStyleLabel);
        _choiceIndex = EditorGUILayout.Popup(_choiceIndex, _choices);
        
        if (_choiceIndex < 0)
            _choiceIndex = 0;
        _targetTypeProp.stringValue = _choices[_choiceIndex];
        

        serializedObject.ApplyModifiedProperties();
    }
}


    

