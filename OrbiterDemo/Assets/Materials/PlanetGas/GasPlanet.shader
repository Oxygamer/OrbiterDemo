// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:33088,y:32688,varname:node_4013,prsc:2|diff-7066-OUT,emission-1207-OUT;n:type:ShaderForge.SFN_Color,id:1304,x:32438,y:32583,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_1304,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Color,id:3097,x:32422,y:33023,ptovrint:False,ptlb:EmissionColor,ptin:_EmissionColor,varname:node_3097,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:8683,x:32422,y:33197,ptovrint:False,ptlb:Intensity,ptin:_Intensity,varname:node_8683,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:7992,x:32695,y:33020,varname:node_7992,prsc:2|A-3097-RGB,B-8683-OUT,C-6351-RGB;n:type:ShaderForge.SFN_Tex2d,id:3506,x:32438,y:32769,ptovrint:False,ptlb:MainTexture,ptin:_MainTexture,varname:node_3506,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:7066,x:32712,y:32676,varname:node_7066,prsc:2|A-1304-RGB,B-3506-RGB;n:type:ShaderForge.SFN_Tex2d,id:6351,x:32422,y:33283,ptovrint:False,ptlb:EmissionTexture,ptin:_EmissionTexture,varname:node_6351,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:4962,x:32888,y:33353,ptovrint:False,ptlb:CloudsTex,ptin:_CloudsTex,varname:node_4962,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-2692-OUT;n:type:ShaderForge.SFN_TexCoord,id:5591,x:32216,y:33493,varname:node_5591,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Time,id:3974,x:32216,y:33652,varname:node_3974,prsc:2;n:type:ShaderForge.SFN_Code,id:2692,x:32508,y:33520,varname:node_2692,prsc:2,code:ZgBsAG8AYQB0ADIAIAByAGUAcwB1AGwAdABVAFYAPQBmAGwAbwBhAHQAMgAoAFUAVgAuAHgAKwBUACoAUwAsAFUAVgAuAHkAKQA7AAoAcgBlAHQAdQByAG4AIAByAGUAcwB1AGwAdABVAFYAOwA=,output:1,fname:UVShift,width:352,height:249,input:1,input:0,input:0,input_1_label:UV,input_2_label:T,input_3_label:S|A-5591-UVOUT,B-3974-T,C-4663-OUT;n:type:ShaderForge.SFN_Blend,id:1207,x:32858,y:33012,varname:node_1207,prsc:2,blmd:10,clmp:True|SRC-7992-OUT,DST-4962-RGB;n:type:ShaderForge.SFN_ValueProperty,id:4663,x:32361,y:33791,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_4663,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;proporder:1304-3097-8683-3506-6351-4962-4663;pass:END;sub:END;*/

Shader "Shader Forge/GasPlanet" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _EmissionColor ("EmissionColor", Color) = (0.5,0.5,0.5,1)
        _Intensity ("Intensity", Float ) = 1
        _MainTexture ("MainTexture", 2D) = "white" {}
        _EmissionTexture ("EmissionTexture", 2D) = "white" {}
        _CloudsTex ("CloudsTex", 2D) = "white" {}
        _Speed ("Speed", Float ) = 0.5
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _Color;
            uniform float4 _EmissionColor;
            uniform float _Intensity;
            uniform sampler2D _MainTexture; uniform float4 _MainTexture_ST;
            uniform sampler2D _EmissionTexture; uniform float4 _EmissionTexture_ST;
            uniform sampler2D _CloudsTex; uniform float4 _CloudsTex_ST;
            float2 UVShift( float2 UV , float T , float S ){
            float2 resultUV=float2(UV.x+T*S,UV.y);
            return resultUV;
            }
            
            uniform float _Speed;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 _MainTexture_var = tex2D(_MainTexture,TRANSFORM_TEX(i.uv0, _MainTexture));
                float3 diffuseColor = (_Color.rgb*_MainTexture_var.rgb);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float4 _EmissionTexture_var = tex2D(_EmissionTexture,TRANSFORM_TEX(i.uv0, _EmissionTexture));
                float4 node_3974 = _Time;
                float2 node_2692 = UVShift( i.uv0 , node_3974.g , _Speed );
                float4 _CloudsTex_var = tex2D(_CloudsTex,TRANSFORM_TEX(node_2692, _CloudsTex));
                float3 emissive = saturate(( _CloudsTex_var.rgb > 0.5 ? (1.0-(1.0-2.0*(_CloudsTex_var.rgb-0.5))*(1.0-(_EmissionColor.rgb*_Intensity*_EmissionTexture_var.rgb))) : (2.0*_CloudsTex_var.rgb*(_EmissionColor.rgb*_Intensity*_EmissionTexture_var.rgb)) ));
/// Final Color:
                float3 finalColor = diffuse + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _Color;
            uniform float4 _EmissionColor;
            uniform float _Intensity;
            uniform sampler2D _MainTexture; uniform float4 _MainTexture_ST;
            uniform sampler2D _EmissionTexture; uniform float4 _EmissionTexture_ST;
            uniform sampler2D _CloudsTex; uniform float4 _CloudsTex_ST;
            float2 UVShift( float2 UV , float T , float S ){
            float2 resultUV=float2(UV.x+T*S,UV.y);
            return resultUV;
            }
            
            uniform float _Speed;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 _MainTexture_var = tex2D(_MainTexture,TRANSFORM_TEX(i.uv0, _MainTexture));
                float3 diffuseColor = (_Color.rgb*_MainTexture_var.rgb);
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
