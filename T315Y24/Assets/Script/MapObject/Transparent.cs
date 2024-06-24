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
    private Color color = Color.white;      //colorはwhiteをベースに使用します。

    //親 子オブジェクトを格納。
    private MeshRenderer[] meshRenderers;
    private MaterialPropertyBlock m_mpb;

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
        color.a = 0.5f;

        mpb.SetColor(Shader.PropertyToID("_Color"), color);
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].GetComponent<Renderer>().material.shader = Shader.Find("Unlit/UnlitTransparent");
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
        color.a = 1f;
        mpb.SetColor(Shader.PropertyToID("_Color"), color);
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
            meshRenderers[i].SetPropertyBlock(mpb);
        }
    }
}
