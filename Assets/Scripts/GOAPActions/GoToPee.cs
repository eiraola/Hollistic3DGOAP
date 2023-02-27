using UnityEngine;
using static UnityEngine.GraphicsBuffer;


    public class GoToPee : GAction
    {


    public override bool PrePerform()
    {
        target = GWorld.Instance.GetQueue(EResourceType.Toilet).RemoveResource(GWorld.Instance.GetWorld());
        if (target == null) return false;
        inventory.AddItem(target);
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetQueue(EResourceType.Toilet).AddResource(target, GWorld.Instance.GetWorld());
        inventory.RemoveItem(target);
        target= null;
        beliefs.RemoveState("peeing");
        return true;
    }
}
