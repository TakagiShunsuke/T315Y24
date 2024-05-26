/*=====
<PlayerDashInterval.cs> //�X�N���v�g��
���쐬�ҁFtakagi

�����e


�����ӎ���
����̃I�u�W�F�N�g�Ɉȉ��̃R���|�[�l���g���Ȃ��ƓG�Ƃ��ď\���ȋ@�\�����܂���B
�P.IFeatureBase���p�������A������\���R���|�[�l���g
�Q.�U���͈͂�\����`�̗̈攻��AreaSector
�R.�������Z���s��Rigidbody


�ȉ��̃R���|�[�l���g������ꍇ�͂��̏����l���V���A���C�Y����Ď��������l�����������ď��������܂��B
�P.IMove���p�������A�ړ����s���R���|�[�l���g�̕ϐ�Speed


���X�V����
__Y24
_M05
D
21:�v���O�����쐬:takagi
=====*/

//�����O��Ԑ錾
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;  //Unity

//���N���X��`
public class CPlayerDashInterval : MonoBehaviour
{
    //private Canvas m_Canvas = null;    //���m�Ώ�
    //[SerializeField] protected AssetReferenceTexture2D m_AssetRef; //�����ΏۃA�Z�b�g�Ǘ�
    //[SerializeField] private Sprite m_Sp;   //�摜
    //[SerializeField] private Vector2 m_First = new Vector2(500.0f, 500.0f);   //1��
    //[SerializeField] private double m_dInterval;    //�����Ԋu
    //[SerializeField] private float a = 1.6f;
    private GameObject CanvasObj;
    private CPlayerScript Player = null;
    private Sprite sprite;
    private List<AssetReferenceTexture2D> Assets;
    List<AsyncOperationHandle<Texture2D>> op = new List<AsyncOperationHandle<Texture2D>>();
    Sprite sp;
    private double m_inner;
    private GameObject m_Text;

    [SerializeField] private TextMeshProUGUI m_TextMeshProUGUI;

    /*���������֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�C���X�^���X�������ɍs������
    */
    public void Start()
    {
        //HP�擾
        Player = GetComponent<CPlayerScript>();
        //CanvasObj = new GameObject();
        //�L�����o�X���
        //CanvasObj.AddComponent<Canvas>();
        //m_Canvas = CanvasObj.GetComponent<Canvas>();
        //m_Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        //m_Canvas.AddComponent<CanvasScaler>();
        ////m_Canvas.AddComponent<GraphicRaycaster>();
        //m_Canvas.name = "EneCanvas";
        ////�e�L�X�g���
        //m_Text = new GameObject();
        //m_Text.GetComponent<CanvasRenderer>();
        //var textMesh = m_Text.GetComponent<TextMeshPro>();

        if(m_TextMeshProUGUI == null)
        {
        }

        //�C���[�W���
        //if (m_AssetRef != null)
        //    m_AssetRef.LoadAssetAsync().Completed += (option) =>
        //    {
        //        var tex = option.Result;
        //        sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        //    };
        if (Player != null)
        {
            m_inner = Player.DashCntDwn;
            n();
        }


        //var Tex2 = await hand.Task;
        //sp = Sprite.Create(Tex2, new Rect(0, 0, Tex2.width, Tex2.height), Vector2.zero);
        //Debug.Log(sp);
        //GameObject ImageObj = new GameObject();
        //ImageObj.AddComponent<Image>();
        //ImageObj.name = "Pl_Heart";
        //ImageObj.GetComponent<Image>().sprite = sp;
        //ImageObj.GetComponent<RectTransform>().position = new Vector2(0.0f + m_First.x,
        //Screen.currentResolution.height - m_First.y);
        //ImageObj.GetComponent<RectTransform>().localScale = new Vector2(a, a);
        ////�e�q�t��
        //ImageObj.transform.parent = CanvasObj.transform;
    }

    /*�������X�V�֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F��莞�Ԃ��Ƃɍs���X�V����
    */
    private void FixedUpdate()
    {
        if(m_inner != Player.DashCntDwn)
        {
            n();
        }
        //for (int i = CanvasObj.transform.childCount; i < Player.HP; i++)
        //{
        //    k(i);
        //}

        //if (Player.HP >= 0.0d)
        //    for (int i = (int)Player.HP; i < CanvasObj.transform.childCount; i++)
        //    {
        //        Destroy(CanvasObj.transform.GetChild(i).gameObject);
        //    }
    }

    //private async void k(int g = 0)
    //{
    //    GameObject ImageObj = new GameObject();
    //    ImageObj.AddComponent<Image>();
    //    ImageObj.name = "Pl_Heart";
    //    var qop = Addressables.LoadAssetAsync<Texture2D>(m_AssetRef);
    //    //var ass = new AssetReferenceTexture2D(m_AssetRef.ToString());
    //    //if (m_AssetRef != null)
    //    //    ass.LoadAssetAsync().Completed += (option) =>
    //    //    {
    //    //        var tex = option.Result;
    //    //        ImageObj.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
    //    //    };
    //    //op = m_AssetRef.LoadAssetAsync();
    //    var Tex2 = await qop.Task;
    //    sp = Sprite.Create(Tex2, new Rect(0, 0, Tex2.width, Tex2.height), Vector2.zero);
    //    ImageObj.GetComponent<Image>().sprite = sp;
    //    ImageObj.GetComponent<RectTransform>().position = new Vector2(0.0f + m_First.x + (float)m_dInterval * (g % 5),
    //    Screen.currentResolution.height - m_First.y - (float)m_dInterval * (g / 5));
    //    ImageObj.GetComponent<RectTransform>().localScale = new Vector2(a, a);
    //    //�e�q�t��
    //    ImageObj.transform.parent = CanvasObj.transform;
    //    //Assets.Add(ass);
    //    op.Add(qop);
    //    Debug.Log(sp);
    //}

    private void n()
    {
        m_TextMeshProUGUI.text = "b";
        if (Player.DashCntDwn> 0)
        {
            //m_TextMeshProUGUI.text = "�_�b�V���s�\\n�c��F" + Player.DashCntDwn.ToString("##") + "s"; //0.xxx[s]�̂Ƃ��\������Ȃ�
            m_TextMeshProUGUI.text = "�_�b�V���s�\\n�c��F" + ((uint)Player.DashCntDwn).ToString().PadRight(2) + "s"; //space�p�f�B���O
            //m_TextMeshProUGUI.text = "a";
        }
        else
        {
            m_TextMeshProUGUI.text = "�_�b�V���\";
        }
    }
    private void OnDestroy()
    {
        //for (var i = 0; i < op.Count; i++)
        //{
        //    //if
        //    Addressables.Release(op[i]);
        //}
    }
}