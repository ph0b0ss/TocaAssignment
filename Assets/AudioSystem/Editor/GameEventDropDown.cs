using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;

namespace TocaAssignment
{
    public class GameEventDropDown : PopupField<UnityEngine.Object>
    {

        public GameEventDropDown()
        {
            choices =GetGameEvents();
            formatSelectedValueCallback += FormatSelectedValueCallback;
            formatListItemCallback += FormatListItemCallback;
        }

        private string FormatListItemCallback(UnityEngine.Object arg)
        {
            return arg.name;
        }

        private string FormatSelectedValueCallback(UnityEngine.Object arg)
        {
            if(arg != null)
                return arg.name;
            return string.Empty;
        }

        public static List<UnityEngine.Object> GetGameEvents()
        {
            string[] gameEventsGUIDs = AssetDatabase.FindAssets("t:GameEvent");
            List<UnityEngine.Object> gameEvents = new List<UnityEngine.Object>();
            foreach (var assetGuid in gameEventsGUIDs)
            {
                GameEvent gameEvent = AssetDatabase.LoadAssetAtPath<GameEvent>(AssetDatabase.GUIDToAssetPath(assetGuid));
                gameEvents.Add(gameEvent);
            }

            return gameEvents;
        }
    }
}