/*=====
<MonoSingleton.cs> //スクリプト名
└作成者：takagi

＞内容
シングルトンの実装

＞注意事項
ジェネリック関数です。継承時に型指定を忘れないこと。
インスタンスはnullな場合があります。取得時にヌルチェックすること。

＞更新履歴
__Y24
_M06
D
05:プログラム作成:takagi
06:続き：takagi
21:リファクタリング:takagi
24:リファクタリング:takagi
=====*/

//＞クラス定義
using UnityEngine;

public abstract class CMonoSingleton<MonoType> : CVirtualizeMono where MonoType : CMonoSingleton<MonoType>  //where文で継承ツリーを明示：T←CMonoSingleton<T>←CVirtualizeMono←MonoBehaviour
{
    //＞変数宣言
    static private MonoType m_Instance; //インスタンス格納用

    //＞プロパティ定義
    public static MonoType Instance
    {
        get
        {
            if (m_Instance == null) //ヌルチェック
            {
                GameObject _GameObject = new GameObject();   //インスタンス作成
                m_Instance = _GameObject.AddComponent<MonoType>();   //自身のコンポーネント登録
            }
            return m_Instance;  //インスタンス提供
        }
    }   //継承先オブジェクトのインスタンス


    /*＞初期化関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：インスタンス生成直後に行う処理
    */
    protected override sealed void Awake()
    {
        //＞自身がいくつ目か
        if(m_Instance != null && m_Instance.gameObject != null) //すでに自身と同一のものがある
        {
            //＞生成キャンセル
            Destroy(this.gameObject);   //自身の生成をなかったことにする
            return; //虚無は処理されない
        }

        //＞インスタンス登録
        m_Instance = (MonoType)this;  //自身をインスタンスとして登録

        //＞追加の処理
        CustomAwake();  //子クラスがこのタイミングで行いたい処理
    }

    /*＞破棄関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：インスタンス破棄時に行う処理
    */
    protected override sealed void OnDestroy()
    {
        //＞生成キャンセル判定
        if(this != m_Instance)    //生成キャンセルのために行われた破棄である
        {
            //＞終了
            return; //これ以降の処理は虚無に行われるものではない
        }

        //＞インスタンス破棄
        if(m_Instance != null)    //インスタンスとして登録されている
        {
            m_Instance = null;    //インスタンスをヌルに初期化
        }

        //＞追加の処理
        CustomOnDestroy();  //子クラスがこのタイミングで行いたい処理
    }
}