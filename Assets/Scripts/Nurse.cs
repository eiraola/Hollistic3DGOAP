using UnityEngine;


    public class Nurse : GAgent
    {
        protected override void Start()
        {
        base.Start();
            SubGoal s1 = new SubGoal("treatPatient", 1, false);
            SubGoal s2 = new SubGoal("rested", 1, false);
            SubGoal s3 = new SubGoal("peed", 1, false);
        goals.Add(s1, 3);
        goals.Add(s2, 1);
        goals.Add(s3, 4);
        Invoke("GetTired", Random.Range(10,20));
        Invoke("GetPeeing", Random.Range(1, 10));
        }
        void GetTired()
        {
            beliefs.ModifyState("exhausted", 0);
            Invoke("GetTired", Random.Range(10,20));
        }
    void GetPeeing()
    {
        beliefs.ModifyState("peeing", 0);
        Invoke("GetPeeing", Random.Range(10, 20));
    }
}
