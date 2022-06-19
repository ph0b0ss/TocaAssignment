using System.Text;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace TocaAssignment
{
    [CustomEditor(typeof(AudioSystem))]
    public class AudioSystemEditor : Editor
    {
        public VisualTreeAsset m_visualTreeAsset;
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement myInspector = new VisualElement();
            
            m_visualTreeAsset.CloneTree(myInspector);
            
            GameEventDropDown dropDown = new GameEventDropDown();

            dropDown.value = dropDown.choices[0];
            Label gameEventContainer = myInspector.Q<Label>("GameEventDropdown");
            gameEventContainer.Add(dropDown);

            ObjectField audioPlayableOF = new ObjectField();
            audioPlayableOF.objectType = typeof(AudioPlayable);
            Label audioPlayableContainer = myInspector.Q<Label>("AudioPlayable");
            audioPlayableContainer.Add(audioPlayableOF);

            TextField listenerName = myInspector.Q<TextField>("ListenerName");
            Button createBTN = myInspector.Q<Button>(className: "CreateButton");
            createBTN.clicked += () => CreateAudioListener(listenerName,(GameEvent)dropDown.value,(AudioPlayable)audioPlayableOF.value);
            
            return myInspector;
        }

        private void CreateAudioListener(TextField nameField,GameEvent gameEvent,AudioPlayable audioPlayable)
        {
            AudioEventListener eventListener = ScriptableObject.CreateInstance<AudioEventListener>();
            eventListener.name = nameField.value;
            eventListener.gameEvent = gameEvent;
            eventListener.audioPlayable = audioPlayable;
            StringBuilder pathBuilder = new StringBuilder();
            pathBuilder.Append("Assets/AudioSystem/EventListener/").Append(eventListener.name).Append(".asset");
            AssetDatabase.CreateAsset(eventListener,pathBuilder.ToString());
            AssetDatabase.SaveAssets();
            (target as AudioSystem).GameEventListeners.Add(eventListener);
            EditorUtility.SetDirty(target);
        }
    }
}