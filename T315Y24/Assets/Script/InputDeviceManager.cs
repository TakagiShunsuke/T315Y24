/*=====
<InputDeviceManager.cs> 
└作成者：iwamuro

＞内容
コントローラーを識別するスプリクト

＞更新履歴
__Y24
_MP7
D
04:コントローラ処理追加:iwamuro
=====*/

//＞名前空間宣言
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

//＞クラス定義
public class InputDeviceManager : MonoBehaviour
{
    // シングルトン
    public static InputDeviceManager Instance { get; private set; }

    /// <summary>
    /// 入力デバイスの種別
    /// </summary>
    public enum InputDeviceType
    {
        Keyboard,   // キーボード・マウス
        Xbox,       // Xboxコントローラー
        DualShock4, // DualShock4(PS4)
        DualSense,  // DualSense(PS5)
        Switch,     // SwitchのProコントローラー
    }

    // 直近に操作された入力デバイスタイプ
    public InputDeviceType CurrentDeviceType { get; private set; } = InputDeviceType.Keyboard;

    // 各デバイスのすべてのキーを１つにバインドしたInputAction（キー種別検知用）
    private InputAction keyboardAnyKey = new InputAction(type: InputActionType.PassThrough, binding: "<Keyboard>/AnyKey", interactions: "Press");
    private InputAction mouseAnyKey = new InputAction(type: InputActionType.PassThrough, binding: "<Mouse>/*", interactions: "Press");
    private InputAction xInputAnyKey = new InputAction(type: InputActionType.PassThrough, binding: "<XInputController>/*", interactions: "Press");
    private InputAction dualShock4AnyKey = new InputAction(type: InputActionType.PassThrough, binding: "<DualShockGamepad>/*", interactions: "Press");
    private InputAction detectDualSenseAnyKey = new InputAction(type: InputActionType.PassThrough, binding: "<DualSenseGamepadHID>/*", interactions: "Press");
    private InputAction switchProControllerAnyKey = new InputAction(type: InputActionType.PassThrough, binding: "<SwitchProControllerHID>/*", interactions: "Press");

    // 入力デバイスタイプ変更イベント
    public UnityEvent OnChangeDeviceType { get; private set; } = new();

    private void Awake()
    {
        // シングルトン
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // キー検知用アクションの有効化
        keyboardAnyKey.Enable();
        mouseAnyKey.Enable();
        xInputAnyKey.Enable();
        dualShock4AnyKey.Enable();
        detectDualSenseAnyKey.Enable();
        switchProControllerAnyKey.Enable();
    }


    private void Start()
    {
        // 初回のみ、必ず入力デバイスの種別検知を行ってコールバック発火
        StartCoroutine(InitializeDetection());
    }

    private void Update()
    {
        // 検知の更新処理
        UpdateDeviceTypesDetection();
    }

    /// <summary>
    /// 入力デバイスの種別検知を初期化する
    /// </summary>
    /// <returns></returns>
    IEnumerator InitializeDetection()
    {
        // 入力デバイスの種別検知を更新
        UpdateDeviceTypesDetection();
        // １フレーム待機
        yield return null;
        // イベント強制発火
        OnChangeDeviceType.Invoke();
    }


    /// <summary>
    /// 入力デバイスの種別検知を更新する
    /// </summary>
    public void UpdateDeviceTypesDetection()
    {
        var beforeDeviceType = CurrentDeviceType;

        if (xInputAnyKey.triggered)
        {
            CurrentDeviceType = InputDeviceType.Xbox;
        }

        // DualSense(PS5)は、DualShock4(PS4)としても認識される。
        // つまり、DualSenseを操作しているときは、DualSchock4とDualSenseの両方が検知される。
        // DualSenseとDualShockの両方から同時に入力検知した場合は、DualSenseとして扱うようにする。
        if (dualShock4AnyKey.triggered)
        {
            CurrentDeviceType = InputDeviceType.DualShock4;
        }
        if (detectDualSenseAnyKey.triggered)
        {
            CurrentDeviceType = InputDeviceType.DualSense;
        }

        if (switchProControllerAnyKey.triggered)
        {
            CurrentDeviceType = InputDeviceType.Switch;
        }

        if (keyboardAnyKey.triggered || mouseAnyKey.triggered)
        {
            CurrentDeviceType = InputDeviceType.Keyboard;
        }

        // 操作デバイスが切り替わったとき、イベント発火
        if (beforeDeviceType != CurrentDeviceType)
        {
            OnChangeDeviceType.Invoke();
        }
    }
}