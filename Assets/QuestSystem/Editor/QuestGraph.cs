using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
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
            ConstructGraphView();
            GenerateToolbar();
        }

        private void ConstructGraphView()
        {
            _questGraphView = new QuestGraphView()
            {
                name = "Quest Graph"
            };

            _questGraphView.StretchToParentSize();
            rootVisualElement.Add(_questGraphView);
        }

        private void GenerateToolbar()
        {
            var toolbar = new Toolbar();

            var nodeCreateButton = new Button(() => { _questGraphView.GenerateNode("Quest node"); });
            nodeCreateButton.text = "Create node";
            toolbar.Add(nodeCreateButton);

            rootVisualElement.Add(toolbar);
        }

        private void OnDisable()
        {
            rootVisualElement.Remove(_questGraphView);
        }
    }
}
