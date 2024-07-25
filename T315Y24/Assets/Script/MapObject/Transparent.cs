/*=====
<Transparent.cs>
└作成者：tei

＞内容

＞注意事項


＞更新履歴
__Y24
_M06
D
21:スクリプト作成：tei
=====*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparent : MonoBehaviour
{
    //＞変数宣言
    public Color color = Color.white;      //colorはwhiteをベースに使用します。

    //親 子オブジェクトを格納。
    private MeshRenderer[] meshRenderers;
    private MaterialPropertyBlock m_mpb;
    [SerializeField] private string _shaderName = "Unlit/UnlitTransparent";
    [SerializeField] private string _shaderNameChange = "Shader Graphs/ArnoldStandardSurface";
    [SerializeField] private float ObjectAlpha = 1.0f;
    //[SerializeField] private string propertyName = "_Progress"; // ShaderGraph内定義した変数名
    //[SerializeField] private Material SceneTransitionMaterial;  // マテリアル
    [SerializeField] private Texture newTexture;

    public MaterialPropertyBlock mpb
    {
        get { return m_mpb ?? (m_mpb = new MaterialPropertyBlock()); }
    }

    void Awake()
    {
        //子オブジェクトと親オブジェクトのmeshrendererを取得
        meshRenderers = this.GetComponentsInChildren<MeshRenderer>();
    }

    /*＞透過シェーダー呼び出し関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：透過用のシェーダーを呼び出し
    */
    public void ClearMaterialInvoke()
    {
        color.a = ObjectAlpha;
        ObjectAlpha -= Time.deltaTime;
        if (color.a <= 0.3f)
        {
            color.a = 0.3f;
        }
        // オブジェクトのRendererコンポーネントを取得
        Renderer renderer = GetComponent<Renderer>();

        // マテリアルのmainTextureを新しいテクスチャに変更
        if (renderer != null && renderer.material != null)
        {
            renderer.material.mainTexture = newTexture;
        }
        mpb.SetColor(Shader.PropertyToID("_Color"), color);
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].GetComponent<Renderer>().material.shader = Shader.Find(_shaderName);
            meshRenderers[i].SetPropertyBlock(mpb);
        }
    }

    /*＞不透過シェーダー呼び出し関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：元(不透過)のシェーダーを呼び出し
    */
    public void NotClearMaterialInvoke()
    {
        ObjectAlpha = 1.0f;
        color.a = ObjectAlpha;
        mpb.SetColor(Shader.PropertyToID("_Color"), color);
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].GetComponent<Renderer>().material.shader = Shader.Find(_shaderNameChange);
            meshRenderers[i].SetPropertyBlock(mpb);
        }
    }
}
