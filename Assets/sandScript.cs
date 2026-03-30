using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class sandScript : MonoBehaviour
{
    [SerializeField] private ComputeShader computeShader;
    [SerializeField] private Material material;
    [SerializeField] private InputActionAsset inputs;
    [SerializeField] private RawImage image;

    public RenderTexture renderTexture;
    public RenderTexture claimGridTexture;
    public int width = 200;
    public int height = 200;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderTexture = new RenderTexture(width, height, 0, RenderTextureFormat.ARGBFloat);
        renderTexture.enableRandomWrite = true;
        renderTexture.Create();
        computeShader.SetTexture(0, "sands", renderTexture);
        claimGridTexture = new RenderTexture(width, height, 0, RenderTextureFormat.ARGBFloat);
        claimGridTexture.enableRandomWrite = true;
        claimGridTexture.Create();
        computeShader.SetTexture(0, "claimGrid", claimGridTexture);
    }

    // Update is called once per frame
    void Update()
    {
        var pos = Camera.main.ScreenToWorldPoint(InputSystem.actions.FindAction("mousePosition").ReadValue<Vector2>());
        pos.z = 1;
        computeShader.SetVector("mousePosition", pos);
        computeShader.SetInt("width", width);
        computeShader.SetInt("height", height);
        computeShader.SetFloat("time", Time.time);
        computeShader.SetBool("mouseClickedThisFrame", InputSystem.actions.FindAction("click").triggered);
        
        computeShader.GetKernelThreadGroupSizes(0, out uint kx, out uint ky, out uint kz);
        
        computeShader.Dispatch(0, Mathf.CeilToInt(width / (float)kx), Mathf.CeilToInt(height / (float)ky), 1);

        image.texture = renderTexture;
    }
}
