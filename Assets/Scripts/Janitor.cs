using UnityEngine;


public class Janitor : GAgent
{
    protected override void Start()
    {
        base.Start();
        SubGoal s1 = new SubGoal("CleanPee", 1, false);
        SubGoal s2 = new SubGoal("Rest", 1, false);

        goals.Add(s1, 10);
        goals.Add(s2, 1);
    }
}
