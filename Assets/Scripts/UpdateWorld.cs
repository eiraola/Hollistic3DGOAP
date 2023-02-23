using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpdateWorld : MonoBehaviour
{
    public Text states;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Dictionary<string, int> worldstates = GWorld.Instance.GetWorld().GetStates();
        states.text = "";
        foreach (KeyValuePair<string, int> state in worldstates)
        {
            states.text += state.Key + " " + state.Value + System.Environment.NewLine
                ;
        }
    }
}
