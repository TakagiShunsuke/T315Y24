/*=====
<VIrtualizeMono.cs> //スクリプト名
└作成者：takagi

＞内容
MonoBehaviorのイベント関数を仮想化(sealed修飾子用)

＞注意事項
MonoBehaviorの各イベント関数をsealedすることができるようになります
書き換えは防ぎたいがそのタイミングで追加で処理したいものがあればそれぞれここで仮想的に定義されている関数(Custom~~()関数)を使うのが早いと思います。
なお、ここで定義されているイベントでない関数は定義のみであり、あくまで各子クラスで定義しなおす手間を省いているにすぎません。
    呼び出しはされていないのでsealedを定義する際に呼び出してください(overrideで上書きしているので当たり前ですが)

また、protectedの修飾子は変更が効きません   ：    これをいじらなければならない場合はこのクラスの使用はお控えください。
(publicがないのはイベント関数をイベントらしく実装するため、privateがないのはオーバーロードを防ぐためです)


＞更新履歴
__Y24
_M06
D
06:プログラム作成:takagi
=====*/

//＞名前空間宣言
using UnityEngine;  //Unity

//＞クラス定義
public class CVirtualizeMono : MonoBehaviour
{
    /*＞初期化関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：インスタンス生成直後に行う処理
    */
    virtual protected void Awake() { }

    /*＞Awake()関数カスタム用
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：Awake()関数で任意に処理を追加しやすいように定義
    */
    virtual protected void CustomAwake() { }

    /*＞初期化関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：初回更新直前に行う処理
    */
    virtual protected void Start() { }

    /*＞Start()関数カスタム用
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：Start()関数で任意に処理を追加しやすいように定義
    */
    virtual protected void CustomStart() { }

    /*＞初期化関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：自身のオブジェクトが有効になった瞬間に行う処理
    */
    virtual protected void OnEnable() { }

    /*＞OnEnable()関数カスタム用
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：OnEnable()関数で任意に処理を追加しやすいように定義
    */
    virtual protected void CustomOnEnable() { }

    /*＞更新関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：一定時間ごとに行う更新処理
    */
    virtual protected void Update() { }

    /*＞Update()関数カスタム用
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：Update()関数で任意に処理を追加しやすいように定義
    */
    virtual protected void CustomUpdate() { }

    /*＞遅延性更新関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：各フレームにおいて通常のUpdate関数の後に行う更新処理
    */
    virtual protected void LateUpdate() { }

    /*＞LateUpdate()関数カスタム用
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：LateUpdate()関数で任意に処理を追加しやすいように定義
    */
    virtual protected void CustomLateUpdate() { }

    /*＞物理更新関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：一定時間ごとに行う物理的な更新処理
    */
    virtual protected void FixedUpdate() { }

    /*＞FixedUpdate()関数カスタム用
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：FixedUpdate()関数で任意に処理を追加しやすいように定義
    */
    virtual protected void CustomFixedUpdate() { }

    /*＞GUI更新関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：GUI描画用に高頻度で行われる更新処理
    */
    virtual protected void OnGUI() { }

    /*＞OnGUI()関数カスタム用
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：OnGUI()関数で任意に処理を追加しやすいように定義
    */
    virtual protected void CustomOnGUI() { }

    /*＞破棄関数カスタム用
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：インスタンス破棄時に行う処理
    */
    virtual protected void OnDestroy() { }

    /*＞OnDestroy()関数カスタム用
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：OnDestroy()関数で任意に処理を追加しやすいように定義
    */
    virtual protected void CustomOnDestroy() { }

    /*＞無効化関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：自身のオブジェクトが無効になった瞬間に行う処理
    */
    virtual protected void OnDisable() { }

    /*＞OnDisable()関数カスタム用
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：OnDisable()関数で任意に処理を追加しやすいように定義
    */
    virtual protected void CustomOnDisable() { }

    /*＞非描画時関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：任意のカメラに映り始めた瞬間に行う処理
    */
    virtual protected void OnBecameVisible() { }

    /*＞OnBecameVisible()関数カスタム用
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：OnBecameVisible()関数で任意に処理を追加しやすいように定義
    */
    virtual protected void CustomOnBecameVisible() { }

    /*＞シャイ時関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：任意のカメラに映らなくなった瞬間に行う処理
    */
    virtual protected void OnBecameInvisible() { }

    /*＞OnBecameInvisible()関数カスタム用
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：OnBecameInvisible()関数で任意に処理を追加しやすいように定義
    */
    virtual protected void CustomOnBecameInvisible() { }

    /*＞描画中関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：映っている間カメラごとに呼び出される処理(IsTrigger off時)
    */
    virtual protected void OnWillRenderObject() { }

    /*＞OnWillRenderObject()関数カスタム用
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：OnWillRenderObject()関数で任意に処理を追加しやすいように定義
    */
    virtual protected void CustomOnWillRenderObject() { }

    /*＞接触時関数3D
    引数：Collision _Collision：接触相手
    ｘ
    戻値：なし
    ｘ
    概要：3D空間上で接触の当たり判定が取られた瞬間に行う処理(IsTrigger off時)
    */
    virtual protected void OnCollisionEnter(Collision _Collision) { }

    /*＞OnCollisionEnter()関数カスタム用
    引数：Collision _Collision：接触相手
    ｘ
    戻値：なし
    ｘ
    概要：OnCollisionEnter()関数で任意に処理を追加しやすいように定義
    */
    virtual protected void CustomOnCollisionEnter(Collision _Collision) { }

    /*＞接触時関数2D
    引数：Collision2D _Collision：接触相手
    ｘ
    戻値：なし
    ｘ
    概要：2D空間上で接触の当たり判定が取られた瞬間に行う処理(IsTrigger off時)
    */
    virtual protected void OnCollisionEnter2D(Collision2D _Collision) { }

    /*＞OnCollisionEnter2D()関数カスタム用
    引数：Collision2D _Collision：接触相手
    ｘ
    戻値：なし
    ｘ
    概要：OnCollisionEnter2D()関数で任意に処理を追加しやすいように定義
    */
    virtual protected void CustomOnCollisionEnter2D(Collision2D _Collision) { }

    /*＞接触時関数3D
    引数：Collider _Collider：接触相手
    ｘ
    戻値：なし
    ｘ
    概要：3D空間上で接触の当たり判定が取られた瞬間に行う処理(IsTrigger on時)
    */
    virtual protected void OnTriggerEnter(Collider _Collider) { }

    /*＞OnTriggerEnter()関数カスタム用
    引数：Collider _Collider：接触相手
    ｘ
    戻値：なし
    ｘ
    概要：OnTriggerEnter()関数で任意に処理を追加しやすいように定義
    */
    virtual protected void CustomOnTriggerEnter(Collider _Collider) { }

    /*＞接触時関数2D
    引数：Collider2D _Collider：接触相手

    ｘ
    戻値：なし
    ｘ
    概要：2D空間上で接触の当たり判定が取られた瞬間に行う処理(IsTrigger on時)
    */
    virtual protected void OnTriggerEnter2D(Collider2D _Collider) { }

    /*＞OnTriggerEnter2D()関数カスタム用
    引数：Collider2D _Collider：接触相手
    ｘ
    戻値：なし
    ｘ
    概要：OnTriggerEnter2D()関数で任意に処理を追加しやすいように定義
    */
    virtual protected void CustomOnTriggerEnter2D(Collider2D _Collider) { }

    /*＞接触中関数3D
    引数：Collision _Collision：接触相手
    ｘ
    戻値：なし
    ｘ
    概要：3D空間上で接触の当たり判定が取られている間行う処理(IsTrigger off時)
    */
    virtual protected void OnCollisionStay(Collision _Collision) { }

    /*＞OnCollisionStay()関数カスタム用
    引数：Collision _Collision：接触相手
    ｘ
    戻値：なし
    ｘ
    概要：OnCollisionStay()関数で任意に処理を追加しやすいように定義
    */
    virtual protected void CustomOnCollisionStay(Collision _Collision) { }

    /*＞接触中関数2D
    引数：Collision2D _Collision：接触相手
    ｘ
    戻値：なし
    ｘ
    概要：2D空間上で接触の当たり判定が取られている間行う処理(IsTrigger off時)
    */
    virtual protected void OnCollisionStay2D(Collision2D _Collision) { }

    /*＞OnCollisionStay2D()関数カスタム用
    引数：Collision2D _Collision：接触相手
    ｘ
    戻値：なし
    ｘ
    概要：OnCollisionStay2D()関数で任意に処理を追加しやすいように定義
    */
    virtual protected void CustomOnCollisionStay2D(Collision2D _Collision) { }

    /*＞接触中関数3D
    引数：Collider _Collider：接触相手
    ｘ
    戻値：なし
    ｘ
    概要：3D空間上で接触の当たり判定が取られている間行う処理(IsTrigger on時)
    */
    virtual protected void OnTriggerStay(Collider _Collider) { }

    /*＞OnTriggerStay()関数カスタム用
    引数：Collider _Collider：接触相手
    ｘ
    戻値：なし
    ｘ
    概要：OnTriggerStay()関数で任意に処理を追加しやすいように定義
    */
    virtual protected void CustomOnTriggerStay(Collider _Collider) { }

    /*＞接触中関数2D
    引数：Collider2D _Collider：接触相手
    ｘ
    戻値：なし
    ｘ
    概要：2D空間上で接触の当たり判定が取られている間行う処理(IsTrigger on時)
    */
    virtual protected void OnTriggerStay2D(Collider2D _Collider) { }

    /*＞OnTriggerStay2D()関数カスタム用
    引数：Collider2D _Collider：接触相手
    ｘ
    戻値：なし
    ｘ
    概要：OnTriggerStay2D()関数で任意に処理を追加しやすいように定義
    */
    virtual protected void CustomOnTriggerStay2D(Collider2D _Collider) { }

    /*＞分離時関数3D
    引数：Collision _Collision：接触相手
    ｘ
    戻値：なし
    ｘ
    概要：3D空間上で接触していた物体と離れた瞬間に行う処理(IsTrigger off時)
    */
    virtual protected void OnCollisionExit(Collision _Collision) { }

    /*＞OnCollisionExit()関数カスタム用
    引数：Collision _Collision：接触相手
    ｘ
    戻値：なし
    ｘ
    概要：OnCollisionExit()関数で任意に処理を追加しやすいように定義
    */
    virtual protected void CustomOnCollisionExit(Collision _Collision) { }

    /*＞分離時関数2D
    引数：Collision2D _Collision：接触相手
    ｘ
    戻値：なし
    ｘ
    概要：2D空間上で接触していた物体と離れた瞬間に行う処理(IsTrigger off時)
    */
    virtual protected void OnCollisionExit2D(Collision2D _Collision) { }

    /*＞OnCollisionExit2D()関数カスタム用
    引数：Collision2D _Collision：接触相手
    ｘ
    戻値：なし
    ｘ
    概要：OnCollisionExit2D()関数で任意に処理を追加しやすいように定義
    */
    virtual protected void CustomOnCollisionExit2D(Collision2D _Collision) { }

    /*＞分離時関数3D
    引数：Collider _Collider：接触相手
    ｘ
    戻値：なし
    ｘ
    概要：3D空間上で接触していた物体と離れた瞬間に行う処理(IsTrigger on時)
    */
    virtual protected void OnTriggerExit(Collider _Collider) { }

    /*＞OnTriggerExit()関数カスタム用
    引数：Collider _Collider：接触相手
    ｘ
    戻値：なし
    ｘ
    概要：OnTriggerExit()関数で任意に処理を追加しやすいように定義
    */
    virtual protected void CustomOnTriggerExit(Collider _Collider) { }

    /*＞分離時関数2D
    引数：GameObject _GameObject：当たったオブジェクトの情報
    ｘ
    戻値：なし
    ｘ
    概要：2D空間上で接触していた物体と離れた瞬間に行う処理(IsTrigger on時)
    */
    virtual protected void OnTriggerExit2D(Collider2D _Collider) { }

    /*＞OnTriggerExit2D()関数カスタム用
    引数：GameObject _GameObject：当たったオブジェクトの情報
    ｘ
    戻値：なし
    ｘ
    概要：OnTriggerExit2D()関数で任意に処理を追加しやすいように定義
    */
    virtual protected void CustomOnTriggerExit2D(Collider2D _Collider) { }

    /*＞パーティクル接触中関数
    引数：GameObject _GameObject：当たったオブジェクトの情報
    ｘ
    戻値：なし
    ｘ
    概要：パーティクルがコライダーと接触している間行う処理(SendCollisionMessage on時)
    */
    virtual protected void OnParticleCollision(GameObject _GameObject) { }

    /*＞OnParticleCollision()関数カスタム用
    引数：GameObject _GameObject：当たったオブジェクトの情報
    ｘ
    戻値：なし
    ｘ
    概要：OnParticleCollision()関数で任意に処理を追加しやすいように定義
    */
    virtual protected void CustomOnParticleCollision(GameObject _GameObject) { }

    /*＞パーティクルトリガー判定関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：ParticleSystemがTriggersモジュールを搭載している間呼び出される処理
    */
    virtual protected void OnParticleTrigger() { }

    /*＞OnParticleTrigger()関数カスタム用
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：OnParticleTrigger()関数で任意に処理を追加しやすいように定義
    */
    virtual protected void CustomOnParticleTrigger() { }
}