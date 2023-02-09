using UnityEngine;

public static class ExtensionMethods
{
    public static bool Contains(this LayerMask layerMask, int layer)
    {
        return ((layerMask & (1 << layer)) != 0);
    }
}