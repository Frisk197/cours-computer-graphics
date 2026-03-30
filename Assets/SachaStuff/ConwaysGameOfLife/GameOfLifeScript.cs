using UnityEngine;

public class GameOfLifeScript : MonoBehaviour
{
    [SerializeField] private RenderTexture gameOfLifeTexture;
    [SerializeField] private ComputeShader shader;

    [SerializeField] private float updateTime = 0.25f;
    [SerializeField] private float pencilRadius = 3f;

    private int kernel;
    private float updateTimer = 0.0f;
    private bool mouseClick;

    private void UpdateGameOfLife()
    {
        var width = gameOfLifeTexture.width;
        var height = gameOfLifeTexture.height;
        
        var textureOrigin = transform.position - new Vector3(width / 2.0f, height / 2.0f);
        var mousePos = Input.mousePosition;
        var relativeMousePos = mousePos - textureOrigin;

        shader.SetFloat("MouseX", width/2f);
        shader.SetFloat("MouseY", height/2f);
        shader.SetBool("MouseClick", true);
        shader.SetFloat("PencilRadius", pencilRadius);
        shader.Dispatch(kernel, 1+ width / 8, 1+ height / 8, 1);

        mouseClick = false;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        kernel = shader.FindKernel("CSMain");

        shader.SetTexture(kernel, "Result", gameOfLifeTexture);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > 17.5)
        {
            updateTimer -= Time.deltaTime;
            pencilRadius += 17 * Time.deltaTime;
            if (Input.GetKey(KeyCode.Mouse0))
                mouseClick = true;

            if (updateTimer <= 0.0f)
            {
                UpdateGameOfLife();

                updateTimer = updateTime;
            }
        }
    }
}
