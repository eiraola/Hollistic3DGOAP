using UnityEngine;


    public class PaperWork : GAction
    {


    public override bool PrePerform()
    {
        target = GWorld.Instance.GetQueue(EResourceType.Office).RemoveResource(GWorld.Instance.GetWorld());
        if (target == null) {
            return false;
        }
        inventory.AddItem(target);
        
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetQueue(EResourceType.Office).AddResource(target, GWorld.Instance.GetWorld());
        inventory.RemoveItem(target);
        return true;
    }
}
