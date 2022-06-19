using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace TocaAssignment
{
    [CustomPropertyDrawer(typeof(AudioEventListener))]
    public class AudioListenerPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();
            container.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/AudioSystem/Editor/AudioListener.uss"));
            ObjectField audiolistenerObj = new ObjectField();
            audiolistenerObj.objectType = typeof(AudioEventListener);
            audiolistenerObj.value = property.objectReferenceValue;
            SerializedObject so = new SerializedObject(property.objectReferenceValue);
            GroupBox containerBox = new GroupBox();
            containerBox.RegisterCallback<MouseDownEvent>(evt => EditorGUIUtility.PingObject(property.objectReferenceValue));

            Label listenerName = new Label(property.objectReferenceValue.name);
            listenerName.AddToClassList("Name");
            containerBox.Add(listenerName);
            
            
            SerializedProperty gameEventSP = so.FindProperty("_gameEvent");
            GameEventDropDown dropDown = new GameEventDropDown();
            dropDown.bindingPath = "_gameEvent";
            dropDown.Bind(so);
            containerBox.Add(dropDown);
            
            SerializedProperty audioPlayableSP = so.FindProperty("audioPlayable");
            ObjectField audioPlayableOF = new ObjectField();
            audioPlayableOF.objectType = typeof(AudioPlayable);
            audioPlayableOF.bindingPath = "audioPlayable";
            audioPlayableOF.Bind(so);
            containerBox.Add(audioPlayableOF);
            container.Add(containerBox);
            
            return container;
        }
    }
}
