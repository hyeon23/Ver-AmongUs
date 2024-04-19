using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

/// <summary>
/// OnLine UI에 부착하는 클래스
/// </summary>
public class OnlineUI : MonoBehaviour
{
    //닉네임을 입력하는 필드의 TMP.text
    [SerializeField]
    private TMP_InputField nicknameInputField;

    [SerializeField]
    private GameObject createRoomUI;

    /// <summary>
    /// 방 만들기 버튼을 눌렀을 때 호출
    /// createRoomUI에서 방에 대한 세부 설정 수행 및 생성
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
    /// 게임 참가하기 버튼을 눌렀을 때 호출되는 함수
    /// StartClient()를 통해 Client로서 게임에 참가하게 됨
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
