using UnityEngine;

public abstract class ComponentHolder : MonoBehaviour
{
    
    public void FindComponent<ComponentType>(ref ComponentType component) where ComponentType : MonoBehaviour
    {
        if (component == null)
        {
            component = gameObject.GetComponentInChildren<ComponentType>();
        }
    }
}
