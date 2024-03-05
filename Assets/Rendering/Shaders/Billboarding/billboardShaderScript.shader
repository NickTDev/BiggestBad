/*Please do support www.bitshiftprogrammer.com by joining the facebook page : fb.com/BitshiftProgrammer
Legal Stuff:
This code is free to use no restrictions but attribution would be appreciated.
Any damage caused either partly or completly due to usage this stuff is not my responsibility*/

// Made some modifications to allow the sprite renderer to affect this shader!!
// -- Merle Roji

Shader "BitshiftProgrammer/Billboard"
{
	Properties
	{
		_MainTex ("Texture Image", 2D) = "white" {}
		_Scaling("Scaling", Float) = 1.0
		[Toggle] _KeepConstantScaling("Keep Constant Scaling", Int) = 1
		[Enum(RenderOnTop, 0,RenderWithTest, 4)] _ZTest("Render on top", Int) = 1
	}
	SubShader
	{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" "DisableBatching" = "True" }

		ZWrite On
		ZTest [_ZTest]
		Blend SrcAlpha OneMinusSrcAlpha
        Cull Off

		Pass
		{
            CGPROGRAM

             #pragma vertex vert
             #pragma fragment frag

             uniform sampler2D _MainTex;
		     int _KeepConstantScaling;
		     float _Scaling;

		    struct appdata
		    {
                float4 vertex : POSITION;
                fixed4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            struct v2f 
		    {
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
            };
 
            v2f vert(appdata v)
            {
                half3x3 m = (half3x3)UNITY_MATRIX_M;
                half3 objectScale = half3(
                    length(half3(m[0][0], m[1][0], m[2][0])),
                    length(half3(m[0][1], m[1][1], m[2][1])),
                    length(half3(m[0][2], m[1][2], m[2][2]))
                    );

                v2f o;
			    float relativeScaler = (_KeepConstantScaling) ? distance(mul(unity_ObjectToWorld, v.vertex), _WorldSpaceCameraPos) : objectScale;
                o.vertex = mul(UNITY_MATRIX_P, mul(UNITY_MATRIX_MV, float4(0.0, 0.0, 0.0, 1.0)) + float4(v.vertex.x, v.vertex.y, 0.0, 0.0) * relativeScaler * _Scaling);
                o.uv = v.uv;
                o.color = v.color;
                return o;
            }
 
            float4 frag(v2f i) : SV_Target
            {
               return tex2D(_MainTex, float2(i.uv)) * i.color;
            }

             ENDCG
        }
   }
}