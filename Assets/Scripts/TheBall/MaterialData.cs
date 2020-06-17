using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MaterialData 
{
    [System.Serializable]
    public struct SMaterial
    {
        public Material ballMaterial;
        public Material directShape;
        public GameObject ballExplosion;
        public GameObject particleMoving;
        
    }

    public SMaterial[] arrayMaterials = new SMaterial[4];
}
