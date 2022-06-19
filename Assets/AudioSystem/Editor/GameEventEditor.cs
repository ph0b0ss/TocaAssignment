using System.Collections;
using System.Collections.Generic;
using TocaAssignment;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(GameEvent))]
public class GameEventEditor : Editor
{
    public VisualTreeAsset m_visualTreeAsset;
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement myInspector = new VisualElement();
        m_visualTreeAsset.CloneTree(myInspector);
        Label title = myInspector.Q<Label>();
        title.text = target.name;
        return myInspector;
    }
}
