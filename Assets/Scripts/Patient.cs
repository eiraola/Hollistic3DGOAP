using UnityEngine;


    public class Patient : GAgent
    {
        protected override void Start()
        {
        base.Start();
            SubGoal s1 = new SubGoal("isWaiting", 1, true);
            SubGoal s2 = new SubGoal("isTreated", 1, true);
            SubGoal s3 = new SubGoal("isAtHome", 1, true);
            SubGoal s4 = new SubGoal("peed", 1, false);
            goals.Add(s1, 3);
            goals.Add(s2, 5);
            goals.Add(s3, 7);
            goals.Add(s4, 8);
        Invoke("GetPeeing", Random.Range(1, 10));
    }
    void GetPeeing()
    {
        beliefs.ModifyState("peeing", 0);
        Invoke("GetPeeing", Random.Range(10, 20));
    }
}
