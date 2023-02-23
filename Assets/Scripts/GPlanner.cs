using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.Rendering;
using System.Collections;

public class Node
{
    public Node parent;
    public float cost;
    public Dictionary<string, int> state;
    public GAction action;

    public Node(Node parent, float cost, Dictionary<string, int> allStates, GAction action)
    {
        this.parent = parent;
        this.cost = cost;
        this.state = new Dictionary<string, int>(allStates);
        this.action = action;
    }
}
public class GPlanner : MonoBehaviour
{

    public Queue<GAction> Plan(List<GAction> actions, Dictionary<string, int> goal, WorldState states)
    {
        List<GAction> usableActions = new List<GAction>();
        foreach (GAction action in actions) {
            if (action.IsAchievable())
            {
                usableActions.Add(action);
            }
        }
        List<Node> leaves = new List<Node>();
        Node start = new Node(null,0, GWorld.Instance.GetWorld().GetStates(), null);
        string tag = gameObject.tag;
        Debug.LogError(gameObject.tag);
        if (start.state.Count != 0 && gameObject.CompareTag("Nurse"))
        {
            Debug.LogError("xD");
        }
        bool success = BuildGraph(start, leaves, usableActions, goal);
        if (!success)
        {
            Debug.LogError("No PLAN");
            return  null;
        }
        Node cheapest = GetCheapestLeaf(leaves);
        Node n = cheapest;
        List<GAction> result = GetCheapestLeafPath(n);
        //After Creating the list of actions, we rebuild it as a queue
        Queue<GAction> queue = new Queue<GAction>();
        foreach (GAction action in result)
        {
            queue.Enqueue(action);
        }
        PrintPlan(queue);
        return queue;
    }
    private void PrintPlan(Queue<GAction> queue)
    {
        Debug.LogError("The plan is: ");
        foreach (GAction item in queue)
        {
            Debug.LogError("Q: " + item.actionName);
        }
    }
    private Node GetCheapestLeaf(List<Node> leaves)
    {
        Node cheapest = null;
        foreach (Node leaf in leaves)
        {
            if (cheapest == null)
            {
                cheapest = leaf;
            }
            else
            {
                if (leaf.cost < cheapest.cost)
                {
                    cheapest = leaf;
                }
            }
        }
        return cheapest;
    }
    private List<GAction> GetCheapestLeafPath(Node cheapest)
    {
        List<GAction> result = new List<GAction>();
        while (cheapest != null)
        {
            if (cheapest.action != null)
            {
                result.Insert(0, cheapest.action);
            }
            cheapest = cheapest.parent;
        }
        return result;
    }
    private bool BuildGraph( Node parent, List<Node> leaves, List<GAction> usableActions, Dictionary<string, int> goal)
    {
        bool foundPath = false;
        
        foreach (GAction action in usableActions)
        {
            if (action.IsAchievableGiven(parent.state))
            {
                Dictionary<string, int> currentState = new Dictionary<string, int>(parent.state);
                foreach (KeyValuePair<string, int> afterEffect in action.aftereffects)
                {
                    if (!currentState.ContainsKey(afterEffect.Key))
                    {
                        currentState.Add(afterEffect.Key, afterEffect.Value);
                    }
                }
                Node node = new Node(parent, parent.cost + action.cost, currentState, action);
                if (GoalAchieved(goal, currentState))
                {
                    leaves.Add(node);
                    foundPath = true;
                }
                else
                {
                    List<GAction> subset = ActionSubset(usableActions, action);
                    bool found = BuildGraph(node, leaves, subset, goal);
                    if (found)
                    {
                        foundPath = true;
                    }
                }
            }
        }
        return foundPath;
    }
    private bool GoalAchieved(Dictionary<string, int> goal, Dictionary<string, int> state)
    {
        foreach (KeyValuePair<string, int> g in goal)
        {
            if (!state.ContainsKey(g.Key))
            {
                return false;
            }
        }
         return true;
    }
    private List<GAction> ActionSubset(List<GAction> actions, GAction removeMe)
    {
        List<GAction> subset = new List<GAction>();
        foreach (GAction action in actions)
        {
            if (!action.Equals(removeMe))
            {
                subset.Add(action);
            }
        }
        return subset;
    }
}
