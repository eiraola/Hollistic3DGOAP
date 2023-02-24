using UnityEngine;


    public class GoToPee : GAction
    {


    public override bool PrePerform()
    {
        target = GWorld.Instance.RemoveToilet();
        if (target == null) return false;
        inventory.AddItem(target);
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.AddToilet(target);
        inventory.RemoveItem(target);
        target= null;
        beliefs.RemoveState("peeing");
        return true;
    }
}
