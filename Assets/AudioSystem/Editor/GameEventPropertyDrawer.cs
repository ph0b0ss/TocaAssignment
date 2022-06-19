using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace TocaAssignment
{
    //[CustomPropertyDrawer(typeof(GameEvent))]
    public class GameEventPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement container = new VisualElement();
            Label name = new Label("asdads");
            container.Add(name);
            return container;
        }
    }
}