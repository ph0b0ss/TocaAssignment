using System.Collections;
using System.Collections.Generic;
using TocaAssignment;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

//[CustomEditor(typeof(AudioEventListener))]
public class AudioEventListenerEditor : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement container = new VisualElement();
        VisualElement baseVisual = base.CreateInspectorGUI();
        container.Add(baseVisual);


        return container;
    }
}
