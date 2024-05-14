using UnityEngine;
/**
*   this component identifies a game object as a place that items can be collected from
*/
public abstract class ItemCollecter : MonoBehaviour
{
    public abstract bool CanCollect();
    public abstract Item Collect();
}
