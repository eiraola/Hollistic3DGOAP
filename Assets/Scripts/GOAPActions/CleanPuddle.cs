public class CleanPuddle : GAction
{


    public override bool PrePerform()
    {
        target = GWorld.Instance.GetQueue(EResourceType.Puddle).RemoveResource(GWorld.Instance.GetWorld());
        if (target == null)
        {
            return false;
        }
        return true;
    }

    public override bool PostPerform()
    {
        if (target)
        {
            Destroy(target);
            target = null;
        }
        return true;
    }
}