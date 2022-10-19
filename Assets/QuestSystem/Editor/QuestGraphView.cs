using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace QuestSystem.Editor
{
  public class QuestGraphView : GraphView
  {
    private readonly Vector2 _defaultNodeSize = new Vector2(150, 200);
    public QuestGraphView()
    {
      styleSheets.Add(Resources.Load<StyleSheet>("QuestGraph"));
      SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
      
      this.AddManipulator(new ContentDragger());
      this.AddManipulator(new SelectionDragger());
      this.AddManipulator(new RectangleSelector());

      GridBackground grid = new GridBackground();
      Insert(0,grid);
      grid.StretchToParentSize();
      
      GenerateEntryPointNode();
      AddElement(GenerateEntryPointNode());
    }

    private Port GeneratePort(QuestNode node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single)
    {
      return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
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

      var generatedPort = GeneratePort(node, Direction.Output);
      generatedPort.name = "Next";
      node.outputContainer.Add(generatedPort);
      node.RefreshPorts();
      
      node.SetPosition(new Rect(100, 200 , 100, 150));
      return node;
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
      List<Port> compatiblePorts = new List<Port>();
      ports.ForEach(port =>
      {
        if (startPort != port && startPort.node != port.node)
          compatiblePorts.Add(port);
      });
      return compatiblePorts;
    }

    public void GenerateNode(string nodeName)
    {
      AddElement(CreateQuestNode(nodeName));
    }

    public QuestNode CreateQuestNode(string nodeName)
    {
      var questNode = new QuestNode
      {
        title = nodeName,
        DialogueText = nodeName,
        GUID = Guid.NewGuid().ToString()
      };

      var inputPort = GeneratePort(questNode, Direction.Input, Port.Capacity.Multi);
      inputPort.portName = "Input";
      questNode.inputContainer.Add(inputPort);

      var button = new Button(() => AddChoicePort(questNode));
      questNode.titleContainer.Add(button);
      button.text = "New choice";
      
      questNode.RefreshExpandedState();
      questNode.RefreshPorts();
      questNode.SetPosition(new Rect(Vector2.zero, _defaultNodeSize));
      return questNode;
    }

    private void AddChoicePort(QuestNode questNode)
    {
      var generatedPort = GeneratePort(questNode, Direction.Output);

      var outputPortCount = questNode.outputContainer.Query("connector").ToList().Count;
      generatedPort.portName = $"Choice {outputPortCount}";
      
      questNode.outputContainer.Add(generatedPort);
      questNode.RefreshPorts();
      questNode.RefreshExpandedState();
    }
  }
}