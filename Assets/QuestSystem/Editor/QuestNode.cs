using UnityEditor.Graphs;
using UnityEngine;
using Node = UnityEditor.Experimental.GraphView.Node;

namespace QuestSystem.Editor
{
  public class QuestNode : Node
  {
    public string GUID;
    public string DialogueText;
    public bool EntryPoint = false;
  }
}