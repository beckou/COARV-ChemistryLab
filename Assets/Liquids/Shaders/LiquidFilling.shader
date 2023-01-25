Shader "Custom/LiquidFilling" {
    //show values to edit in inspector
    // Clips a GameObject past a given plane
    Properties{
        _Color("Tint", Color) = (0, 0, 0, 0)
        [HDR]_Emission("Emission", color) = (0,0,0)
    }

        SubShader{
            //the material is completely non-transparent and is rendered at the same time as the other opaque geometry
            Tags{ "RenderType" = "Opaque" "Queue" = "Geometry"}

            // render faces regardless if they point towards the camera or away from it
            Cull Off

            CGPROGRAM
            //the shader is a surface shader, meaning that it will be extended by unity in the background
            //to have fancy lighting and other features
            //our surface shader function is called surf and we use our custom lighting model
            //fullforwardshadows makes sure unity adds the shadow passes the shader might need
            //vertex:vert makes the shader use vert as a vertex shader function
            #pragma surface surf Standard fullforwardshadows
            #pragma target 3.0

            fixed4 _Color;

            half3 _Emission;

            float4 _Plane;

            //input struct which is automatically filled by unity
            struct Input {
                float3 worldPos;
                float facing : VFACE;
            };

            //the surface shader function which sets parameters the lighting function then uses
            void surf(Input i, inout SurfaceOutputStandard o) {
                //calculate signed distance to plane
                float distance = dot(i.worldPos, _Plane.xyz);
                distance = distance + _Plane.w;
                //discard surface above plane
                clip(-distance);

                float facing = i.facing * 0.5 + 0.5;

                //normal color stuff
                o.Albedo = _Color.rgb * facing;
                o.Emission = lerp(_Color, _Emission, facing);
            }
            ENDCG
        }
            FallBack "Standard" //fallback adds a shadow pass so we get shadows on other objects
}