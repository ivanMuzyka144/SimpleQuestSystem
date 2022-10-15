using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace QuestSystem.Editor
{
  public class QuestGraphView : GraphView
  {
    public QuestGraphView()
    {
      this.AddManipulator(new ContentDragger());
      this.AddManipulator(new SelectionDragger());
      this.AddManipulator(new RectangleSelector());

      GenerateEntryPointNode();
      AddElement(GenerateEntryPointNode());
    }

    private QuestNode GenerateEntryPointNode()
    {
      var node = new QuestNode
      {
        title = "Start",
        GUID = Guid.NewGuid().ToString(),
        DialogueText = "EntryPoint",
        EntryPoint = true
      };
      
      node.SetPosition(new Rect(100, 200 , 100, 150));
      return node;
    }
  }
}