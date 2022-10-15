using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace QuestSystem.Editor
{
    public class QuestGraph : EditorWindow
    {
        private QuestGraphView _questGraphView;
        
        [MenuItem("Graph/Quest Graph")]
        public static void OpenQuestGraphWindow()
        {
            var window = GetWindow<QuestGraph>();
            window.titleContent = new GUIContent("Quest Graph");
            
            
        }

        private void OnEnable()
        {
            _questGraphView = new QuestGraphView()
            {
                name = "Quest Graph"
            };
            
            _questGraphView.StretchToParentSize();
            rootVisualElement.Add(_questGraphView);
        }

        private void OnDisable()
        {
            rootVisualElement.Remove(_questGraphView);
        }
    }
}
