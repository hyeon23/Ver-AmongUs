using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

/// <summary>
/// OnLine UI�� �����ϴ� Ŭ����
/// </summary>
public class OnlineUI : MonoBehaviour
{
    //�г����� �Է��ϴ� �ʵ��� TMP.text
    [SerializeField]
    private TMP_InputField nicknameInputField;

    [SerializeField]
    private GameObject createRoomUI;

    /// <summary>
    /// �� ����� ��ư�� ������ �� ȣ��
    /// createRoomUI���� �濡 ���� ���� ���� ���� �� ����
    /// </summary>
    public void OnClickCreateRoom()
    {
        if(nicknameInputField.text != "")
        {
            PlayerSettings.nickname = nicknameInputField.text;
            createRoomUI.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            nicknameInputField.GetComponent<Animator>().SetTrigger("on");
        }
    }

    /// <summary>
    /// ���� �����ϱ� ��ư�� ������ �� ȣ��Ǵ� �Լ�
    /// StartClient()�� ���� Client�μ� ���ӿ� �����ϰ� ��
    /// </summary>
    public void OnClickEnterGameRoom()
    {
        if (nicknameInputField.text != "")
        {
            var manager = AmongUsRoomManager.singleton;
            manager.StartClient();
        }
        else
        {
            nicknameInputField.GetComponent<Animator>().SetTrigger("on");
        }
    }
}
