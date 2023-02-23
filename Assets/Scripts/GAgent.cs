using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor.Search;
using System.Runtime.InteropServices;
using Unity.VisualScripting;

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
    bool invoked = false;
    protected virtual void Start()
    {
        GAction[] acts = GetComponents<GAction>();
        foreach (GAction act in acts)
        {
            actions.Add(act);
        }
    }
    private void LateUpdate()
    {
        CheckPlanner();
    }

    private void CheckPlanner()
    {
        if (CurrentActionRunning())
        {
            return;
        }
        SetActionQueue();
        HandleGoal();

    }
    private bool CurrentActionRunning()
    {
        if (currentAction != null && currentAction.running)
        {
            if (currentAction.agent.hasPath && currentAction.agent.remainingDistance < 1f)
            {
                if (!invoked)
                {
                    Invoke("CompleteAction", currentAction.duration);
                    invoked = true;
                }
            }
            return true;
        }
        return false;
    }
    private void SetActionQueue()
    {
        if (planner == null || actionQueue == null)
        {
            planner = gameObject.AddComponent<GPlanner>();
            var sortedGoals = from entry in goals orderby entry.Value descending select entry;
            foreach (KeyValuePair<SubGoal, int> sg in sortedGoals)
            {
                actionQueue = planner.Plan(actions, sg.Key.sgoals, null);
                if (actionQueue != null)
                {
                    currentGoal = sg.Key;
                    break;
                }
            }
        }
    }
    private void HandleGoal()
    {
        if (actionQueue != null && actionQueue.Count == 0)
        {
            if (currentGoal.remove)
            {
                goals.Remove(currentGoal);
            }
            planner = null;
        }
        if (actionQueue != null && actionQueue.Count > 0)
        {
            currentAction = actionQueue.Dequeue();
            if (currentAction.PrePerform())
            {
                if (currentAction.target == null && currentAction.targetTag != "")
                {
                    currentAction.target = GameObject.FindWithTag(currentAction.targetTag);
                }
                if (currentAction.target != null)
                {
                    currentAction.running = true;
                    currentAction.agent.SetDestination(currentAction.target.transform.position);
                }
            }
            else
            {
                actionQueue = null;
            }
        }
       
    }
    private void CompleteAction()
    {
        currentAction.running = false;
        currentAction.PostPerform();
        invoked = false;
    }
}
