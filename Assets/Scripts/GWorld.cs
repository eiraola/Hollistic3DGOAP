using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public sealed class GWorld
{
    private static readonly GWorld instance = new GWorld();
    private static GWorldStates world;
    private static Queue<GameObject> patients;
    static GWorld()
    {
        world = new GWorldStates();
        patients = new Queue<GameObject>();
    }
    private GWorld() { }
    public void AddPatient(GameObject p)
    {
        patients.Enqueue(p);
    }
    public GameObject RemovePatient()
    {
        if (patients.Count == 0 )
        {
            return null;
        }
        return patients.Dequeue();
    }
    public static GWorld Instance { get { return instance; } }
    public GWorldStates GetWorld() { return world; }
}
