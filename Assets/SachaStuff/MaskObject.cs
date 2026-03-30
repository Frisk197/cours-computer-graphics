using UnityEngine;

public class MaskObject : MonoBehaviour
{
    [ExecuteInEditMode]
    void Start()
    {
        GetComponent<MeshRenderer>().material.renderQueue = 3002;
    }
}
