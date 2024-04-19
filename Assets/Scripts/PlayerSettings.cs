using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player를 조작하는 방식에 때란 Enum 클래스
/// </summary>
public enum EControlType
{
    Mouse,
    KeyboardMouse,
    Count
};

/// <summary>
/// Plyaer 환경 설정에 관한 데이터 타입을 담고 있는 클래스
/// </summary>
public class PlayerSettings
{
    public static EControlType controlType;
    public static string nickname;
}
