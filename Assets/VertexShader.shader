Shader "Unlit/SandParticle"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            RWTexture2D<float4> sands;
            uint width;
            uint height;
            float4 _Color;

            // Vertex shader
            float4 vert(uint id : SV_VertexID) : SV_POSITION
            {
                uint x = modf(id, width);
                uint y = id-x;
                return float4(x, y, 0.0, 1.0);
            }

            // Fragment shader
            float4 frag() : SV_Target
            {
                
                return _Color;
            }

            ENDCG
        }
    }
}