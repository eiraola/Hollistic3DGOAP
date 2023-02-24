using UnityEngine;


    public class GoToPee : GAction
    {


    public override bool PrePerform()
    {
        target = GWorld.Instance.RemoveToilet();
        if (target == null) return false;
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.AddToilet(target);
        target= null;
        beliefs.RemoveState("peeing");
        return true;
    }
}
