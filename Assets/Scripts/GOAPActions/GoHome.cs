using UnityEngine;


    public class GoHome : GAction
    {


    public override bool PrePerform()
    {
        GWorld.Instance.GetWorld().ModifyState("TreatingPatient", -1);
        return true;
    }

    public override bool PostPerform()
    {
        beliefs.ModifyState("atHospital", -1);
        return true;
    }
}
