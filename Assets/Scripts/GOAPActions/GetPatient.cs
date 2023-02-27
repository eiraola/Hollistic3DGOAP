using UnityEngine;


    public class GetPatient : GAction
    {
    GameObject resource;

    public override bool PrePerform()
    {
        target = GWorld.Instance.GetQueue(EResourceType.Patient).RemoveResource( GWorld.Instance.GetWorld());
        if (target == null)
        {
            return false;
        }
        resource = GWorld.Instance.GetQueue(EResourceType.Cubicle).RemoveResource(GWorld.Instance.GetWorld());
        if (resource!=null)
        {
            inventory.AddItem(resource);
        }
        else
        {
            GWorld.Instance.GetQueue(EResourceType.Patient).AddResource(target, GWorld.Instance.GetWorld());
            target = null;
            return false;
        }
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState("Waiting", -1);
        if (target)
        {
            target.GetComponent<GAgent>().inventory.AddItem(resource);
        }
      return true;
    }
}
