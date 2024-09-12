/*=====
<FlashingSprite.cs>
└作成者：yamamoto

＞内容
Spriteを点滅表示させる

＞更新履歴
__Y24
_M09
D
12:プログラム作成:yamamoto

=====*/

//＞名前空間宣言
using UnityEngine;

//＞クラス定義
public class FlashingSprite : MonoBehaviour
{
    //変数宣言
    [Header("速度変更")]
    [SerializeField, Tooltip("点滅の速度")] float fadeSpeed = 1.0f;          // フェード速度

    private SpriteRenderer spriteRenderer;  // SpriteRendererの参照
    private bool fadingOut = true;          // フェードアウトフラグ

    /*＞初期化関数
  引数１：なし
  ｘ
  戻値：なし
  ｘ
  概要：インスタンス生成時に行う処理
  */
    void Start()
    {
        // 同じゲームオブジェクトにアタッチされているSpriteRendererを取得
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /*＞物理更新関数
   引数：なし
   ｘ
   戻値：なし
   ｘ
   概要：一定時間ごとに行う更新処理
   */
    void FixedUpdate()
    {
        Color color = spriteRenderer.color;  // 現在のスプライトの色を取得

        // アルファ値を調整して点滅させる処理
        if (fadingOut)
        {
            color.a -= fadeSpeed * Time.deltaTime;  // フェードアウト
            if (color.a <= 0.0f)
            {
                color.a = 0.0f;
                fadingOut = false;  // フェードインに切り替え
            }
        }
        else
        {
            color.a += fadeSpeed * Time.deltaTime;  // フェードイン
            if (color.a >= 1.0f)
            {
                color.a = 1.0f;
                fadingOut = true;  // 再びフェードアウトに切り替え
            }
        }

        spriteRenderer.color = color;  // 変更したアルファ値を反映
    }
}
