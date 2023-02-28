using UnityEngine;


    public class GStateMonitor : MonoBehaviour
    {
    public string state;
    public float stateStrength;
    public float stateDecayRate;
    public GWorldStates belifs;
    public GameObject resourcePrefab;
    public EResourceType queueName = EResourceType.Puddle;
    public string worldState;
    public GAction action;
    bool stateFound = false;
    float initialStrength;
        private void Awake()
        {
            belifs= GetComponent<GAgent>().beliefs;
            initialStrength = stateStrength;
        }
        private void LateUpdate()
        {
            if (action.running)
            {
                stateFound = false;
                stateStrength = initialStrength;
            }
            if (!stateFound && belifs.HasState(state))
            {
                stateFound = true;
            }
        if (stateFound)
        {
            stateStrength -= stateDecayRate * Time.deltaTime;
            if (stateStrength <= 0)
            {
                Vector3 location = new Vector3(this.transform.position.x, resourcePrefab.transform.position.y,this.transform.position.z);
                GameObject p = Instantiate(resourcePrefab, location, resourcePrefab.transform.rotation);
                stateFound = false;
                stateStrength = initialStrength;
                belifs.RemoveState(state);
                GWorld.Instance.GetQueue(queueName).AddResource(p, GWorld.Instance.GetWorld());
            }
        }
        }
    }
