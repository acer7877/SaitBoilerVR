Shader "Custom/OutlineShader"
{

    Properties{  
        _Diffuse("Diffuse", Color) = (1,1,1,1)  
        _OutlineColor("Outline Color", Color) = (1,0,0,1)  
        _Thickness("Thickness", Range(0,1)) = 0.001  
        _MainTex("Base 2D", 2D) = "white"{}  
    }
    
    SubShader  
    {  
        Pass  
        {  
            Cull Front
            
            CGPROGRAM  

            #pragma vertex vert  
            #pragma fragment frag  
            #include "UnityCG.cginc"  
            fixed4 _OutlineColor;  
            float _Thickness;
            
            struct v2f  
            {  
                float4 pos : SV_POSITION;  
            }; 
            
            v2f vert(appdata_full v)  
            {  
                v2f o;  

                o.pos = UnityObjectToClipPos(v.vertex); 
                float3 vnormal = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);  
                float2 offset = TransformViewToProjection(vnormal.xy);  
                o.pos.xy += offset * _Thickness / 100;  
                return o;  
            }
            
            fixed4 frag(v2f i) : SV_Target  
            {  
                return _OutlineColor;  
            }  
            ENDCG  
        }
 
    }  
  
    FallBack "Diffuse"  
}

