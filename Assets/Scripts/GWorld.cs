using System.Runtime.CompilerServices;
using UnityEngine;


public sealed class GWorld
{
    private static readonly GWorld instance = new GWorld();
    private static GWorldStates world;
    static GWorld()
    {
        world = new GWorldStates();
    }
    private GWorld() { }

    public static GWorld Instance { get { return instance; } }
    public GWorldStates GetWorld() { return world; }
}
