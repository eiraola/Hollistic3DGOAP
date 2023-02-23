using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


    public abstract class GAction : MonoBehaviour
    {

    public string actionName = "Action";
    public float cost = 1.0f;
    public GameObject target;
    public string targetTag;
    public float duration = 0;
    public WorldState[] preConditions;
    public WorldState[] afterEffects;
    public NavMeshAgent agent;
    public GInventory inventory;
    public Dictionary<string, int> preconditions;
    public Dictionary<string, int> aftereffects;
    public GWorldStates beliefs;
    public GWorldStates agentBeliefs;
    public bool running = false;
    public GAction()
    {
        preconditions = new Dictionary<string, int>();
        aftereffects = new Dictionary<string, int>();

    }
    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (preConditions != null)
        {
            foreach (WorldState w in preConditions)
            {
                preconditions.Add(w.key, w.value);
            }
        }
        if (afterEffects != null)
        {
            foreach (WorldState w in afterEffects)
            {
                aftereffects.Add(w.key, w.value);
            }
        }
        beliefs = GetComponent<GAgent>().beliefs;
        inventory = this.GetComponent<GAgent>().inventory;
    }
    public bool IsAchievable()
    {
        return true;
    }
    public bool IsAchievableGiven(Dictionary<string, int> conditions)
    {
        foreach (KeyValuePair<string, int> p in preconditions)
        {
            if (!conditions.ContainsKey(p.Key))
            {
                return false;
            }
        }
        return true;
    }
    public abstract bool PrePerform();
    public abstract bool PostPerform();
}
