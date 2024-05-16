/*cameraSample.cs
//作成者　石井爽太
//＞内容
//プレイヤーの位置取得とプレイヤーから離れる数値と回転数値追加
//カメラにアタッチするスクリプト

//＞更新履歴
__Y24
_M05
D
//3　カメラ制作 :isii
//8　コメントアウト追加 仮でカメラの座標と回転数値初期化&変更 :isii
//10    コメントアウト追加　で移動と回転の数値変更可能に :isii
//11 コメントアウト追加 余分な文削除:isii
 */

//>名前空間宣言
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;



//>クラス定義
public class cameraSample : MonoBehaviour
{
    //>変数定義
    // Start is called before the first frame update
    private GameObject Player;//プレイヤー格納(オブジェクト)用

    [SerializeField] private GameObject　target ;//追尾するターゲット
    //カメラの移動設定
    [SerializeField] private float PosX = 0.0f;//プレイヤーから離れるxの数値変更
    [SerializeField] private float PosY = 30.0f;//プレイヤーから離れるYの数値変更
    [SerializeField] private float PosZ = -40.0f;//プレイヤーから離れるzの数値変更
    //カメラの回転設定
    [SerializeField] private float angleX = 30.0f;//x回転の値を変更できる
    [SerializeField] private float angleY = 0.0f;//y回転の値を変更できる
    [SerializeField] private float angleZ = 0.0f;//z回転の値を変更できる

    void Start()
    {
        /*＞カメラの変数
   引数１：なし
   ｘ
   戻値：なし
   ｘ
   概要：カメラの変数を念のため初期化
   */
        //カメラの座標初期化
        Vector3 pos = this.transform.position;//移動関数取得
        Debug.Log("X = " + pos.x);//移動x初期化
        Debug.Log("Y = " + pos.y);//移動y初期化
        Debug.Log("Z = " + pos.z);//移動z初期化

        //カメラの角度初期化
        Vector3 angle = this.transform.localEulerAngles;//回転関数の取得
        Debug.Log("X = " + angle.x);//回転x初期化
        Debug.Log("Y = " + angle.y);//回転y初期化
        Debug.Log("Z = " + angle.z);//回転z初期化
        /*＞プレイヤーの位置取得
   引数１：なし
   ｘ
   戻値：なし
   ｘ
   概要：プレイヤーの位置のみ取得
   */
        Vector3 a;
        a = new Vector3( +PosX,  +PosY, +PosZ);//プレイヤーの座標＋プレイヤーから離れる数値
        //プレイヤーの追尾
        this.Player =  target;//追尾したい名前を入れる  
        //カメラの座標変更
        pos = Player.gameObject.transform.position + a;//プレイヤー座標取得
      
        this.transform.position = pos;//カメラの座標更新

    }



    // Update is called once per frame
    void Update()
    {
        /*＞カメラの位置変更
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：カメラの座標変更
    */


        Vector3 a;
        a = new Vector3(+PosX, +PosY, +PosZ);//プレイヤーの座標＋プレイヤーから離れる数値
        //プレイヤーの追尾
        Vector3 pos =Player.gameObject.transform.position + a ;//pos関数取得
        this.gameObject.transform.position = pos;//カメラの座標更新
        /*＞カメラの角度変更
   引数１：なし
   ｘ
   戻値：なし
   ｘ
   概要：カメラ角度変更
   */
        //カメラの角度変更
        Vector3 angle = this.transform.localEulerAngles;//回転関数取得
       
        
         angle = new Vector3(angleX, angleY, angleZ);//回転数値変更
        
        this.transform.localEulerAngles = angle;//カメラの回転数値更新

      
    }
}
