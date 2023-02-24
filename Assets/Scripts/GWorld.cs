using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public sealed class GWorld
{
    private static readonly GWorld instance = new GWorld();
    private static GWorldStates world;
    private static Queue<GameObject> patients;
    private static Queue<GameObject> cubicles;
    private static Queue<GameObject> offices;
    private static Queue<GameObject> toilets;
    static GWorld()
    {
        world = new GWorldStates();
        patients = new Queue<GameObject>();
        cubicles = new Queue<GameObject>();
        offices = new Queue<GameObject>();
        toilets = new Queue<GameObject>();
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cubicle");
        
        foreach (GameObject item in cubes)
        {
            cubicles.Enqueue(item);
        }
        if (cubicles.Count > 0)
        {
            world.ModifyState("FreeCubicle", cubicles.Count);
        }
        GameObject[] offs = GameObject.FindGameObjectsWithTag("Office");
        foreach (GameObject item in offs)
        {
            offices.Enqueue(item);
        }
        if (offices.Count > 0)
        {
            world.ModifyState("FreeOffice", offs.Length);
        }
        GameObject[] tl = GameObject.FindGameObjectsWithTag("Toilet");
        foreach (GameObject item in tl)
        {
            toilets.Enqueue(item);
        }
        if (toilets.Count > 0)
        {
            world.ModifyState("FreeToilet", tl.Length);
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
    public void AddOffice(GameObject c)
    {
        offices.Enqueue(c);
        world.ModifyState("FreeOffice", 1);
    }
    public GameObject RemoveOffice()
    {
        if (offices.Count == 0)
        {
            return null;
        }
        world.ModifyState("FreeOffice", -1);
        return offices.Dequeue();
    }

    public void AddToilet(GameObject c)
    {
        toilets.Enqueue(c);
        world.ModifyState("FreeToilet", 1);
    }
    public GameObject RemoveToilet()
    {
        if (toilets.Count == 0)
        {
            return null;
        }
        world.ModifyState("FreeToilet", -1);
        return toilets.Dequeue();
    }
    public static GWorld Instance { get { return instance; } }
    public GWorldStates GetWorld() { return world; }
}
