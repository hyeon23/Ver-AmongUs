using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player�� �����ϴ� ��Ŀ� ���� Enum Ŭ����
/// </summary>
public enum EControlType
{
    Mouse,
    KeyboardMouse,
    Count
};

/// <summary>
/// Plyaer ȯ�� ������ ���� ������ Ÿ���� ��� �ִ� Ŭ����
/// </summary>
public class PlayerSettings
{
    public static EControlType controlType;
    public static string nickname;
}
