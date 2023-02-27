using UnityEngine;


    public class GoToWaitingRoom : GAction
    {


    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState("Waiting",1);
        GWorld.Instance.GetQueue(EResourceType.Patient).AddResource(gameObject, GWorld.Instance.GetWorld());
        beliefs.ModifyState("atHospital", 1);
        return true;
    }
}
