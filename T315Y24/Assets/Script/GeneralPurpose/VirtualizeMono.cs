/*=====
<VIrtualizeMono.cs> //�X�N���v�g��
���쐬�ҁFtakagi

�����e
MonoBehavior�̃C�x���g�֐������z��(sealed�C���q�p)

�����ӎ���
MonoBehavior�̊e�C�x���g�֐���sealed���邱�Ƃ��ł���悤�ɂȂ�܂�
���������͖h�����������̃^�C�~���O�Œǉ��ŏ������������̂�����΂��ꂼ�ꂱ���ŉ��z�I�ɒ�`����Ă���֐�(Custom~~()�֐�)���g���̂������Ǝv���܂��B
�Ȃ��A�����Œ�`����Ă���C�x���g�łȂ��֐��͒�`�݂̂ł���A�����܂Ŋe�q�N���X�Œ�`���Ȃ�����Ԃ��Ȃ��Ă���ɂ����܂���B
    �Ăяo���͂���Ă��Ȃ��̂�sealed���`����ۂɌĂяo���Ă�������(override�ŏ㏑�����Ă���̂œ�����O�ł���)

�܂��Aprotected�̏C���q�͕ύX�������܂���   �F    �����������Ȃ���΂Ȃ�Ȃ��ꍇ�͂��̃N���X�̎g�p�͂��T�����������B
(public���Ȃ��̂̓C�x���g�֐����C�x���g�炵���������邽�߁Aprivate���Ȃ��̂̓I�[�o�[���[�h��h�����߂ł�)


���X�V����
__Y24
_M06
D
06:�v���O�����쐬:takagi
=====*/

//�����O��Ԑ錾
using UnityEngine;  //Unity

//���N���X��`
public class CVirtualizeMono : MonoBehaviour
{
    /*���������֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�C���X�^���X��������ɍs������
    */
    virtual protected void Awake() { }

    /*��Awake()�֐��J�X�^���p
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FAwake()�֐��ŔC�ӂɏ�����ǉ����₷���悤�ɒ�`
    */
    virtual protected void CustomAwake() { }

    /*���������֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F����X�V���O�ɍs������
    */
    virtual protected void Start() { }

    /*��Start()�֐��J�X�^���p
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FStart()�֐��ŔC�ӂɏ�����ǉ����₷���悤�ɒ�`
    */
    virtual protected void CustomStart() { }

    /*���������֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F���g�̃I�u�W�F�N�g���L���ɂȂ����u�Ԃɍs������
    */
    virtual protected void OnEnable() { }

    /*��OnEnable()�֐��J�X�^���p
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FOnEnable()�֐��ŔC�ӂɏ�����ǉ����₷���悤�ɒ�`
    */
    virtual protected void CustomOnEnable() { }

    /*���X�V�֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F��莞�Ԃ��Ƃɍs���X�V����
    */
    virtual protected void Update() { }

    /*��Update()�֐��J�X�^���p
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FUpdate()�֐��ŔC�ӂɏ�����ǉ����₷���悤�ɒ�`
    */
    virtual protected void CustomUpdate() { }

    /*���x�����X�V�֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�e�t���[���ɂ����Ēʏ��Update�֐��̌�ɍs���X�V����
    */
    virtual protected void LateUpdate() { }

    /*��LateUpdate()�֐��J�X�^���p
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FLateUpdate()�֐��ŔC�ӂɏ�����ǉ����₷���悤�ɒ�`
    */
    virtual protected void CustomLateUpdate() { }

    /*�������X�V�֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F��莞�Ԃ��Ƃɍs�������I�ȍX�V����
    */
    virtual protected void FixedUpdate() { }

    /*��FixedUpdate()�֐��J�X�^���p
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FFixedUpdate()�֐��ŔC�ӂɏ�����ǉ����₷���悤�ɒ�`
    */
    virtual protected void CustomFixedUpdate() { }

    /*��GUI�X�V�֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FGUI�`��p�ɍ��p�x�ōs����X�V����
    */
    virtual protected void OnGUI() { }

    /*��OnGUI()�֐��J�X�^���p
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FOnGUI()�֐��ŔC�ӂɏ�����ǉ����₷���悤�ɒ�`
    */
    virtual protected void CustomOnGUI() { }

    /*���j���֐��J�X�^���p
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�C���X�^���X�j�����ɍs������
    */
    virtual protected void OnDestroy() { }

    /*��OnDestroy()�֐��J�X�^���p
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FOnDestroy()�֐��ŔC�ӂɏ�����ǉ����₷���悤�ɒ�`
    */
    virtual protected void CustomOnDestroy() { }

    /*���������֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F���g�̃I�u�W�F�N�g�������ɂȂ����u�Ԃɍs������
    */
    virtual protected void OnDisable() { }

    /*��OnDisable()�֐��J�X�^���p
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FOnDisable()�֐��ŔC�ӂɏ�����ǉ����₷���悤�ɒ�`
    */
    virtual protected void CustomOnDisable() { }

    /*����`�掞�֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�C�ӂ̃J�����ɉf��n�߂��u�Ԃɍs������
    */
    virtual protected void OnBecameVisible() { }

    /*��OnBecameVisible()�֐��J�X�^���p
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FOnBecameVisible()�֐��ŔC�ӂɏ�����ǉ����₷���悤�ɒ�`
    */
    virtual protected void CustomOnBecameVisible() { }

    /*���V���C���֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�C�ӂ̃J�����ɉf��Ȃ��Ȃ����u�Ԃɍs������
    */
    virtual protected void OnBecameInvisible() { }

    /*��OnBecameInvisible()�֐��J�X�^���p
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FOnBecameInvisible()�֐��ŔC�ӂɏ�����ǉ����₷���悤�ɒ�`
    */
    virtual protected void CustomOnBecameInvisible() { }

    /*���`�撆�֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�f���Ă���ԃJ�������ƂɌĂяo����鏈��(IsTrigger off��)
    */
    virtual protected void OnWillRenderObject() { }

    /*��OnWillRenderObject()�֐��J�X�^���p
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FOnWillRenderObject()�֐��ŔC�ӂɏ�����ǉ����₷���悤�ɒ�`
    */
    virtual protected void CustomOnWillRenderObject() { }

    /*���ڐG���֐�3D
    �����FCollision _Collision�F�ڐG����
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F3D��ԏ�ŐڐG�̓����蔻�肪���ꂽ�u�Ԃɍs������(IsTrigger off��)
    */
    virtual protected void OnCollisionEnter(Collision _Collision) { }

    /*��OnCollisionEnter()�֐��J�X�^���p
    �����FCollision _Collision�F�ڐG����
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FOnCollisionEnter()�֐��ŔC�ӂɏ�����ǉ����₷���悤�ɒ�`
    */
    virtual protected void CustomOnCollisionEnter(Collision _Collision) { }

    /*���ڐG���֐�2D
    �����FCollision2D _Collision�F�ڐG����
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F2D��ԏ�ŐڐG�̓����蔻�肪���ꂽ�u�Ԃɍs������(IsTrigger off��)
    */
    virtual protected void OnCollisionEnter2D(Collision2D _Collision) { }

    /*��OnCollisionEnter2D()�֐��J�X�^���p
    �����FCollision2D _Collision�F�ڐG����
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FOnCollisionEnter2D()�֐��ŔC�ӂɏ�����ǉ����₷���悤�ɒ�`
    */
    virtual protected void CustomOnCollisionEnter2D(Collision2D _Collision) { }

    /*���ڐG���֐�3D
    �����FCollider _Collider�F�ڐG����
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F3D��ԏ�ŐڐG�̓����蔻�肪���ꂽ�u�Ԃɍs������(IsTrigger on��)
    */
    virtual protected void OnTriggerEnter(Collider _Collider) { }

    /*��OnTriggerEnter()�֐��J�X�^���p
    �����FCollider _Collider�F�ڐG����
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FOnTriggerEnter()�֐��ŔC�ӂɏ�����ǉ����₷���悤�ɒ�`
    */
    virtual protected void CustomOnTriggerEnter(Collider _Collider) { }

    /*���ڐG���֐�2D
    �����FCollider2D _Collider�F�ڐG����

    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F2D��ԏ�ŐڐG�̓����蔻�肪���ꂽ�u�Ԃɍs������(IsTrigger on��)
    */
    virtual protected void OnTriggerEnter2D(Collider2D _Collider) { }

    /*��OnTriggerEnter2D()�֐��J�X�^���p
    �����FCollider2D _Collider�F�ڐG����
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FOnTriggerEnter2D()�֐��ŔC�ӂɏ�����ǉ����₷���悤�ɒ�`
    */
    virtual protected void CustomOnTriggerEnter2D(Collider2D _Collider) { }

    /*���ڐG���֐�3D
    �����FCollision _Collision�F�ڐG����
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F3D��ԏ�ŐڐG�̓����蔻�肪����Ă���ԍs������(IsTrigger off��)
    */
    virtual protected void OnCollisionStay(Collision _Collision) { }

    /*��OnCollisionStay()�֐��J�X�^���p
    �����FCollision _Collision�F�ڐG����
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FOnCollisionStay()�֐��ŔC�ӂɏ�����ǉ����₷���悤�ɒ�`
    */
    virtual protected void CustomOnCollisionStay(Collision _Collision) { }

    /*���ڐG���֐�2D
    �����FCollision2D _Collision�F�ڐG����
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F2D��ԏ�ŐڐG�̓����蔻�肪����Ă���ԍs������(IsTrigger off��)
    */
    virtual protected void OnCollisionStay2D(Collision2D _Collision) { }

    /*��OnCollisionStay2D()�֐��J�X�^���p
    �����FCollision2D _Collision�F�ڐG����
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FOnCollisionStay2D()�֐��ŔC�ӂɏ�����ǉ����₷���悤�ɒ�`
    */
    virtual protected void CustomOnCollisionStay2D(Collision2D _Collision) { }

    /*���ڐG���֐�3D
    �����FCollider _Collider�F�ڐG����
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F3D��ԏ�ŐڐG�̓����蔻�肪����Ă���ԍs������(IsTrigger on��)
    */
    virtual protected void OnTriggerStay(Collider _Collider) { }

    /*��OnTriggerStay()�֐��J�X�^���p
    �����FCollider _Collider�F�ڐG����
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FOnTriggerStay()�֐��ŔC�ӂɏ�����ǉ����₷���悤�ɒ�`
    */
    virtual protected void CustomOnTriggerStay(Collider _Collider) { }

    /*���ڐG���֐�2D
    �����FCollider2D _Collider�F�ڐG����
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F2D��ԏ�ŐڐG�̓����蔻�肪����Ă���ԍs������(IsTrigger on��)
    */
    virtual protected void OnTriggerStay2D(Collider2D _Collider) { }

    /*��OnTriggerStay2D()�֐��J�X�^���p
    �����FCollider2D _Collider�F�ڐG����
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FOnTriggerStay2D()�֐��ŔC�ӂɏ�����ǉ����₷���悤�ɒ�`
    */
    virtual protected void CustomOnTriggerStay2D(Collider2D _Collider) { }

    /*���������֐�3D
    �����FCollision _Collision�F�ڐG����
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F3D��ԏ�ŐڐG���Ă������̂Ɨ��ꂽ�u�Ԃɍs������(IsTrigger off��)
    */
    virtual protected void OnCollisionExit(Collision _Collision) { }

    /*��OnCollisionExit()�֐��J�X�^���p
    �����FCollision _Collision�F�ڐG����
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FOnCollisionExit()�֐��ŔC�ӂɏ�����ǉ����₷���悤�ɒ�`
    */
    virtual protected void CustomOnCollisionExit(Collision _Collision) { }

    /*���������֐�2D
    �����FCollision2D _Collision�F�ڐG����
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F2D��ԏ�ŐڐG���Ă������̂Ɨ��ꂽ�u�Ԃɍs������(IsTrigger off��)
    */
    virtual protected void OnCollisionExit2D(Collision2D _Collision) { }

    /*��OnCollisionExit2D()�֐��J�X�^���p
    �����FCollision2D _Collision�F�ڐG����
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FOnCollisionExit2D()�֐��ŔC�ӂɏ�����ǉ����₷���悤�ɒ�`
    */
    virtual protected void CustomOnCollisionExit2D(Collision2D _Collision) { }

    /*���������֐�3D
    �����FCollider _Collider�F�ڐG����
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F3D��ԏ�ŐڐG���Ă������̂Ɨ��ꂽ�u�Ԃɍs������(IsTrigger on��)
    */
    virtual protected void OnTriggerExit(Collider _Collider) { }

    /*��OnTriggerExit()�֐��J�X�^���p
    �����FCollider _Collider�F�ڐG����
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FOnTriggerExit()�֐��ŔC�ӂɏ�����ǉ����₷���悤�ɒ�`
    */
    virtual protected void CustomOnTriggerExit(Collider _Collider) { }

    /*���������֐�2D
    �����FGameObject _GameObject�F���������I�u�W�F�N�g�̏��
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F2D��ԏ�ŐڐG���Ă������̂Ɨ��ꂽ�u�Ԃɍs������(IsTrigger on��)
    */
    virtual protected void OnTriggerExit2D(Collider2D _Collider) { }

    /*��OnTriggerExit2D()�֐��J�X�^���p
    �����FGameObject _GameObject�F���������I�u�W�F�N�g�̏��
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FOnTriggerExit2D()�֐��ŔC�ӂɏ�����ǉ����₷���悤�ɒ�`
    */
    virtual protected void CustomOnTriggerExit2D(Collider2D _Collider) { }

    /*���p�[�e�B�N���ڐG���֐�
    �����FGameObject _GameObject�F���������I�u�W�F�N�g�̏��
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�p�[�e�B�N�����R���C�_�[�ƐڐG���Ă���ԍs������(SendCollisionMessage on��)
    */
    virtual protected void OnParticleCollision(GameObject _GameObject) { }

    /*��OnParticleCollision()�֐��J�X�^���p
    �����FGameObject _GameObject�F���������I�u�W�F�N�g�̏��
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FOnParticleCollision()�֐��ŔC�ӂɏ�����ǉ����₷���悤�ɒ�`
    */
    virtual protected void CustomOnParticleCollision(GameObject _GameObject) { }

    /*���p�[�e�B�N���g���K�[����֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FParticleSystem��Triggers���W���[���𓋍ڂ��Ă���ԌĂяo����鏈��
    */
    virtual protected void OnParticleTrigger() { }

    /*��OnParticleTrigger()�֐��J�X�^���p
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FOnParticleTrigger()�֐��ŔC�ӂɏ�����ǉ����₷���悤�ɒ�`
    */
    virtual protected void CustomOnParticleTrigger() { }
}