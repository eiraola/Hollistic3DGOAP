using UnityEngine;


    public class GoToCubical : GAction
    {
    GameObject resource;

    public override bool PrePerform()
    {
       
        target = GetComponent<GAgent>().inventory.FindItemWithTag("Cubicle");
        if (target == null)
        {
            return false;
        }
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState("TreatingPatient", 1);
        GWorld.Instance.GetQueue(EResourceType.Cubicle).AddResource(target,GWorld.Instance.GetWorld());
        inventory.RemoveItem(target);
        return true;
    }
}
