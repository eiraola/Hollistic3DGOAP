using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor.Search;

public class SubGoal
{
    public Dictionary<string,int> sgoals;
    public bool remove;
    public SubGoal(string s, int i, bool r)
    {
        sgoals = new Dictionary<string,int>();
        sgoals.Add(s,i);
        remove = r;
    }
}
public abstract class GAgent : MonoBehaviour
{
    public List<GAction> actions = new List<GAction>();
    public Dictionary<SubGoal,int> goals = new Dictionary<SubGoal, int>();
    GPlanner planner;
    Queue<GAction> actionQueue;
    public GAction currentAction;
    SubGoal currentGoal;
    private void Start()
    {
        GAction[] acts = GetComponents<GAction>();
        foreach (GAction act in acts)
        {
            actions.Add(act);
        }
    }
    private void LateUpdate()
    {
        
    }
}
