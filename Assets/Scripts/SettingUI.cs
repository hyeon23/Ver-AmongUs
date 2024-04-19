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
    /// 조작 방식을 설정하는 기능
    /// </summary>
    /// <param name="controlType">EControlType에 해당하는 int 값</param>
    public void SetControlMode(int controlType)
    {
        PlayerSettings.controlType = (EControlType)controlType;

        ControlTypeSetting(PlayerSettings.controlType);
    }

    /// <summary>
    /// 선택한 ControlType에 따른 UI 세팅
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
    /// Setting창 Close
    /// </summary>
    public void Close()
    {
        StartCoroutine(CloseAfterDelay());
    }

    /// <summary>
    /// Close에 Delay를 주는 방식
    /// 대기 모드로 돌아가기 위해, Close 수행 후, ResetTrigger 호출
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
