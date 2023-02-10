using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
    public class WorldState
    {
        public string key;
        public int value;
    }
public class GWorldStates
{
    public Dictionary<string,int> states;
    public GWorldStates()
    {
        states = new Dictionary<string,int>();
    }
    public bool HasState(string state)
    {
        return states.ContainsKey(state);
    }
    public void AddState(string key, int value) {
            states.Add(key, value);
    }
    public void ModifyState(string key, int value)
    {
        if (states.ContainsKey(key))
        {
            states[key] += value;
            if (states[key] <= 0)
            {
                RemoveState(key);
            }
            else
            {
                states.Add(key, value);
            }
        }
    }
    public void RemoveState(string key) {
        if (states.ContainsKey(key))
        {
            states.Remove(key);
        }
    }
     public void SetState(string key, int value)
    {
        if (states.ContainsKey(key))
        {
            states[key] = value;
        }
        else
        {
            states.Add(key, value);
        }
    }
    public Dictionary<string, int> GetStates() { 
        return states;
    }
        
}
