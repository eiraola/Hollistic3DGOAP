using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public sealed class GWorld
{
    private static readonly GWorld instance = new GWorld();
    private static GWorldStates world;
    private static Queue<GameObject> patients;
    private static Queue<GameObject> cubicles;
    static GWorld()
    {
        world = new GWorldStates();
        patients = new Queue<GameObject>();
        cubicles = new Queue<GameObject>();
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cubicle");
        foreach (GameObject item in cubes)
        {
            cubicles.Enqueue(item);
        }
        if (cubicles.Count > 0)
        {
            world.ModifyState("FreeCubicle", cubicles.Count);
        }
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
    public void AddCubicles(GameObject c)
    {
        cubicles.Enqueue(c);
        world.ModifyState("FreeCubicle", 1);
    }
    public GameObject RemoveCubicle()
    {
        if (cubicles.Count == 0)
        {
            return null;
        }
        world.ModifyState("FreeCubicle", - 1);
        return cubicles.Dequeue();
    }
    public static GWorld Instance { get { return instance; } }
    public GWorldStates GetWorld() { return world; }
}
