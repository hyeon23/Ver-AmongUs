using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [Header("UI Objects")]
    [SerializeField]
    private Button MouseControlButton;
    [SerializeField]
    private Button KeyboardMouseControlButton;
    [SerializeField]
    private TextMeshProUGUI MouseTMP;
    [SerializeField]
    private TextMeshProUGUI KeyboardMouseTMP;

    [Header("Animator")]
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        ControlTypeSetting(PlayerSettings.controlType);
    }

    /// <summary>
    /// ���� ����� �����ϴ� ���
    /// </summary>
    /// <param name="controlType">EControlType�� �ش��ϴ� int ��</param>
    public void SetControlMode(int controlType)
    {
        PlayerSettings.controlType = (EControlType)controlType;

        ControlTypeSetting(PlayerSettings.controlType);
    }

    /// <summary>
    /// ������ ControlType�� ���� UI ����
    /// </summary>
    /// <param name="eControlType"></param>
    private void ControlTypeSetting(EControlType eControlType)
    {
        switch (eControlType)
        {
            case EControlType.Mouse:
                MouseControlButton.image.color = Color.green;
                MouseTMP.color = Color.green;
                KeyboardMouseControlButton.image.color = Color.white;
                KeyboardMouseTMP.color = Color.white;
                break;
            case EControlType.KeyboardMouse:
                MouseControlButton.image.color = Color.white;
                MouseTMP.color = Color.white;
                KeyboardMouseControlButton.image.color = Color.green;
                KeyboardMouseTMP.color = Color.green;
                break;
            case EControlType.Count:
                break;
        }
    }

    /// <summary>
    /// Settingâ Close
    /// </summary>
    public virtual void Close()
    {
        StartCoroutine(CloseAfterDelay());
    }

    /// <summary>
    /// Close�� Delay�� �ִ� ���
    /// ��� ���� ���ư��� ����, Close ���� ��, ResetTrigger ȣ��
    /// </summary>
    /// <returns></returns>
    private IEnumerator CloseAfterDelay()
    {
        animator.SetTrigger("close");
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        animator.ResetTrigger("close");
    }
}
