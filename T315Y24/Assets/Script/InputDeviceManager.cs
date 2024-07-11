/*=====
<InputDeviceManager.cs> 
���쐬�ҁFiwamuro

�����e
�R���g���[���[�����ʂ���X�v���N�g

���X�V����
__Y24
_MP7
D
04:�R���g���[�������ǉ�:iwamuro
=====*/

//�����O��Ԑ錾
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

//���N���X��`
public class InputDeviceManager : MonoBehaviour
{
    // �V���O���g��
    public static InputDeviceManager Instance { get; private set; }

    /// <summary>
    /// ���̓f�o�C�X�̎��
    /// </summary>
    public enum InputDeviceType
    {
        Keyboard,   // �L�[�{�[�h�E�}�E�X
        Xbox,       // Xbox�R���g���[���[
        DualShock4, // DualShock4(PS4)
        DualSense,  // DualSense(PS5)
        Switch,     // Switch��Pro�R���g���[���[
    }

    // ���߂ɑ��삳�ꂽ���̓f�o�C�X�^�C�v
    public InputDeviceType CurrentDeviceType { get; private set; } = InputDeviceType.Keyboard;

    // �e�f�o�C�X�̂��ׂẴL�[���P�Ƀo�C���h����InputAction�i�L�[��ʌ��m�p�j
    private InputAction keyboardAnyKey = new InputAction(type: InputActionType.PassThrough, binding: "<Keyboard>/AnyKey", interactions: "Press");
    private InputAction mouseAnyKey = new InputAction(type: InputActionType.PassThrough, binding: "<Mouse>/*", interactions: "Press");
    private InputAction xInputAnyKey = new InputAction(type: InputActionType.PassThrough, binding: "<XInputController>/*", interactions: "Press");
    private InputAction dualShock4AnyKey = new InputAction(type: InputActionType.PassThrough, binding: "<DualShockGamepad>/*", interactions: "Press");
    private InputAction detectDualSenseAnyKey = new InputAction(type: InputActionType.PassThrough, binding: "<DualSenseGamepadHID>/*", interactions: "Press");
    private InputAction switchProControllerAnyKey = new InputAction(type: InputActionType.PassThrough, binding: "<SwitchProControllerHID>/*", interactions: "Press");

    // ���̓f�o�C�X�^�C�v�ύX�C�x���g
    public UnityEvent OnChangeDeviceType { get; private set; } = new();

    private void Awake()
    {
        // �V���O���g��
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // �L�[���m�p�A�N�V�����̗L����
        keyboardAnyKey.Enable();
        mouseAnyKey.Enable();
        xInputAnyKey.Enable();
        dualShock4AnyKey.Enable();
        detectDualSenseAnyKey.Enable();
        switchProControllerAnyKey.Enable();
    }


    private void Start()
    {
        // ����̂݁A�K�����̓f�o�C�X�̎�ʌ��m���s���ăR�[���o�b�N����
        StartCoroutine(InitializeDetection());
    }

    private void Update()
    {
        // ���m�̍X�V����
        UpdateDeviceTypesDetection();
    }

    /// <summary>
    /// ���̓f�o�C�X�̎�ʌ��m������������
    /// </summary>
    /// <returns></returns>
    IEnumerator InitializeDetection()
    {
        // ���̓f�o�C�X�̎�ʌ��m���X�V
        UpdateDeviceTypesDetection();
        // �P�t���[���ҋ@
        yield return null;
        // �C�x���g��������
        OnChangeDeviceType.Invoke();
    }


    /// <summary>
    /// ���̓f�o�C�X�̎�ʌ��m���X�V����
    /// </summary>
    public void UpdateDeviceTypesDetection()
    {
        var beforeDeviceType = CurrentDeviceType;

        if (xInputAnyKey.triggered)
        {
            CurrentDeviceType = InputDeviceType.Xbox;
        }

        // DualSense(PS5)�́ADualShock4(PS4)�Ƃ��Ă��F�������B
        // �܂�ADualSense�𑀍삵�Ă���Ƃ��́ADualSchock4��DualSense�̗��������m�����B
        // DualSense��DualShock�̗������瓯���ɓ��͌��m�����ꍇ�́ADualSense�Ƃ��Ĉ����悤�ɂ���B
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

        // ����f�o�C�X���؂�ւ�����Ƃ��A�C�x���g����
        if (beforeDeviceType != CurrentDeviceType)
        {
            OnChangeDeviceType.Invoke();
        }
    }
}