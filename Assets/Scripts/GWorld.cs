using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum EResourceType
{
    None,
    Patient,
    Office,
    Toilet,
    Cubicle
}
public class ResourceQueue
{
    public Queue<GameObject> queue = new Queue<GameObject>();
    public string tag = "";
    public string modState;
    public ResourceQueue(string tag, string state, GWorldStates w) {
        this.tag = tag;
        modState= state;
        if (tag.Equals("")) { return; }
        GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject go in gos)
        {
            queue.Enqueue(go);
        }
        if (gos.Length > 0)
        {
            w.ModifyState(modState, gos.Length);
        }
    }
    public void AddResource(GameObject r, GWorldStates w)
    {
        queue.Enqueue(r); 
        w.ModifyState(modState, 1);


    }
    public GameObject RemoveResource(GWorldStates w)
    {
        if (queue.Count == 0)
        {
            return null;
        }
        w.ModifyState(modState, -1);
        return queue.Dequeue();
    }
}
public sealed class GWorld
{
    private static readonly GWorld instance = new GWorld();
    private static GWorldStates world;
    private static ResourceQueue patients;
    private static ResourceQueue cubicles;
    private static ResourceQueue offices;
    private static ResourceQueue toilets;
    private static Dictionary<EResourceType, ResourceQueue> resources = new Dictionary<EResourceType, ResourceQueue>();
    static GWorld()
    {
        world = new GWorldStates();
        patients = new ResourceQueue("", "", world);
        cubicles = new ResourceQueue("Cubicle", "FreeCubicle", world);
        offices = new ResourceQueue("Office", "FreeOffice", world);
        toilets = new ResourceQueue("Toilet", "FreeToilet", world);
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cubicle");
        resources.Add(EResourceType.Patient, patients);
        resources.Add(EResourceType.Cubicle, cubicles);
        resources.Add(EResourceType.Office, offices);
        resources.Add(EResourceType.Toilet, toilets);

    }
    private GWorld() { }
    public ResourceQueue GetQueue(EResourceType type)
    {
        return resources[type];
    }
    public static GWorld Instance { get { return instance; } }
    public GWorldStates GetWorld() { return world; }
}
