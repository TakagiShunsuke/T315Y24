/*=====
<CameraRay.cs>
└作成者：tei

＞内容
カメラからレイを飛ばして、プレイヤーとの間に他のオブジェクト
があったら、オブジェクトを透過処理します。

＞注意事項


＞更新履歴
__Y24
_M06
D
21:スクリプト作成：tei
25:リファクタリング:takagi
=====*/
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CCameraRay : MonoBehaviour
{
    //＞変数宣言
    [SerializeField] private GameObject player;     // インスペクター上でプレイヤーを接続
    Vector3 tergetPosition;
    float tergetOffsetYFoot = 0.1f;     // rayを飛ばす方向のオフセット（足元）
    float tergetOffsetYHead = 17f;      // rayを飛ばす方向のオフセット（頭）

    public GameObject[] prevRaycast;
    public List<GameObject> raycastHitsList_ = new List<GameObject>();

    /*＞更新処理関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：カメラレイ判定の更新
    */
    void Update()
    {
        //カメラ→足元間のオブジェクトを半透明化している
        prevRaycast = raycastHitsList_.ToArray();   //前フレームで透明にしているオブジェクト（リスト）を配列prevRayCastに出力
        raycastHitsList_.Clear();                   //前フレームで透明にしているオブジェクト（リスト）を初期化？消去？
        tergetPosition = player.transform.position; //tergetPositionにPlayerのpositionを格納
        tergetPosition.y += tergetOffsetYFoot;      //tergetPositionのy軸（高さ方向）にオフセットを反映。ここでは足元の高さに合わせている。（足元の値をそのままいれると真下の床が透明になることがあったためオフセットした。）
        Vector3 _difference = (tergetPosition - this.transform.position);   //カメラ位置→tergetPositionへのベクトルを取得
        RayCastHit(_difference);                    //↓のメソッドを参照。rayを飛ばして条件に合うものを半透明にして、raycastHitListに追加している。

        //カメラ→頭頂部間のオブジェクトを半透明化している
        tergetPosition.y += tergetOffsetYHead;      //tergetPositionのy軸（高さ方向）にオフセットを反映。ここでは頭の高さに合わせている。
        _difference = (tergetPosition - this.transform.position);   //カメラ位置→tergetPositionへのベクトルを取得
        RayCastHit(_difference);

        //ヒットしたGameObjectの差分を求めて、衝突しなかったオブジェクトを不透明に戻す
        foreach (GameObject _gameObject in prevRaycast.Except<GameObject>(raycastHitsList_))    //prevRaycastとraycastHitList_との差分を抽出してる。
        {
            Transparent noSampleMaterial = _gameObject.GetComponent<Transparent>();
            if (_gameObject != null)
            {
                noSampleMaterial.NotClearMaterialInvoke();
            }

        }
    }

    /*＞レイ関数
    引数：Vector3 _difference
    ｘ
    戻値：なし
    ｘ
    概要：カメラレイとレイの判定を作成、当たったオブジェクトをlistに追加
    */
    //rayを飛ばして条件に合うものを半透明にして、raycastHitListに追加している。
    public void RayCastHit(Vector3 _difference)
    {
        Vector3 direction = _difference.normalized;           //カメラ-ターゲット間のベクトルの正規ベクトルを抽出

        Ray ray = new Ray(this.transform.position, direction);//Rayを発射
        RaycastHit[] rayCastHits = Physics.RaycastAll(ray);    //Rayにあたったオブジェクトをすべて取得

        foreach (RaycastHit hit in rayCastHits)
        {
            float distance = Vector3.Distance(hit.point, transform.position);       //カメラ-rayがあたった場所間の距離を取得
            if (distance < _difference.magnitude)      //カメラ-rayがあたった場所間の距離とカメラ-ターゲット間の距離を比較。（この比較を行わないとPlayerの奥側のオブジェクトも透明になる。）
            {
                Transparent transparent = hit.collider.GetComponent<Transparent>();
                if (
                hit.collider.tag == "Map")          //タグを確認
                {
                    transparent.ClearMaterialInvoke();                  //透明にするメソッドを呼び出す。
                    raycastHitsList_.Add(hit.collider.gameObject);      //hitしたgameobjectを追加する
                }
            }
        }
    }
}
