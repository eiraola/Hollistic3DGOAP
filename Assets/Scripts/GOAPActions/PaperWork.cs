using UnityEngine;


    public class PaperWork : GAction
    {


    public override bool PrePerform()
    {
        target = GWorld.Instance.RemoveOffice();
        if (target == null) {
            return false;
        }
        inventory.AddItem(target);
        
        return true;
    }

    public override bool PostPerform()
    {
        
        GWorld.Instance.AddOffice(target);
        inventory.RemoveItem(target);
        return true;
    }
}
