using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{

    public static GameObject SpawnToGarbage(this GameObject prefab, Vector3 position, Quaternion quaternion)
    {
        return PoolGarbage.Instance.Spawn(prefab, position, quaternion);
    }

    public static Vector3 GarbagePosition()
    {
        return PoolGarbage.Instance.GetPosition();
    }

    public static void ResetTransform(this GameObject prefab)
    {
        prefab.transform.position = Vector3.zero;
        prefab.transform.rotation = Quaternion.Euler(Vector3.zero);
        prefab.transform.localScale = Vector3.one;
    }

}
