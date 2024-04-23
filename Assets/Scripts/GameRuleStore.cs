using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using TMPro;
using System.Text;

public enum EKillRange
{
    Short, Normal, Long, Count
}

public enum ETaskBarUpdates
{
    Always, Meetings, Never
}

public struct GameRuleData
{
    public bool confirmEjects;
    public int emergencyMeetings;
    public int emergencyMeetingsCooldown;
    public int meetingsTime;
    public int voteTime;
    public bool anonymousVotes;
    public float moveSpeed;
    public float crewSight;
    public float imposterSignt;
    public float killCooldown;
    public EKillRange killRange;
    public bool visualTasks;
    public ETaskBarUpdates taskBarUpdates;
    public int commonTask;
    public int complexTask;
    public int simpleTask;
}

public class GameRuleStore : NetworkBehaviour

{
    [SyncVar(hook = nameof(SetIsRecommandRule_Hook))]
    private bool isRecommandRule;
    [SerializeField]
    private Toggle isRecommandRuleToggle;
    public void SetIsRecommandRule_Hook(bool _, bool value)
    {
        UpdateGameRuleOverview();
    }
    public void OnRecommandToggle(bool value)
    {
        isRecommandRule = value;
        if (isRecommandRule)
        {
            SetRecommendGameRule();
        }
    }

    [SyncVar(hook = nameof(SetConfirmEjects_Hook))]
    private bool confirmEjects;
    [SerializeField]
    private Toggle confirmEjectsToggle;
    public void SetConfirmEjects_Hook(bool _, bool value)
    {
        UpdateGameRuleOverview();
    }
    public void OnConfirmEjectsToggle(bool value)
    {
        isRecommandRule = false;
        isRecommandRuleToggle.isOn = false;
        confirmEjects = value;
    }

    [SyncVar(hook = nameof(SetEmergencyMeetings_Hook))]
    private int emergencyMeetings;
    [SerializeField]
    private TextMeshProUGUI emergencyMeetingsTMP;
    public void SetEmergencyMeetings_Hook(int _, int value)
    {
        emergencyMeetingsTMP.text = value.ToString();
        UpdateGameRuleOverview();
    }
    public void OnChangeEmergencyMeetings(bool isPlus)
    {
        emergencyMeetings = Mathf.Clamp(emergencyMeetings + (isPlus ? 1 : -1), 0, 9);
        isRecommandRule = false;
        isRecommandRuleToggle.isOn = false;
    }

    [SyncVar(hook = nameof(SetEmergencyMeetingsCooldown_Hook))]
    private int emergencyMeetingsCooldown;
    [SerializeField]
    private TextMeshProUGUI emergencyMeetingsCooldownTMP;
    public void SetEmergencyMeetingsCooldown_Hook(int _, int value)
    {
        emergencyMeetingsCooldownTMP.text = string.Format("{0}s", value);
        UpdateGameRuleOverview();
    }
    public void OnChangeEmergencyMeetingsCooldown(bool isPlus)
    {
        emergencyMeetingsCooldown = Mathf.Clamp(emergencyMeetingsCooldown + (isPlus ? 5 : -5), 0, 60);
        isRecommandRule = false;
        isRecommandRuleToggle.isOn = false;
    }

    [SyncVar(hook = nameof(SetMeetingTime_Hook))]
    private int meetingsTime;
    [SerializeField]
    private TextMeshProUGUI meetingsTimeTMP;
    public void SetMeetingTime_Hook(int _, int value)
    {
        meetingsTimeTMP.text = string.Format("{0}s", value);
        UpdateGameRuleOverview();
    }
    public void OnChangeMeetingTime(bool isPlus)
    {
        meetingsTime = Mathf.Clamp(meetingsTime + (isPlus ? 5 : -5), 0, 120);
        isRecommandRule = false;
        isRecommandRuleToggle.isOn = false;
    }

    [SyncVar(hook = nameof(SetVoteTime_Hook))]
    private int voteTime;
    [SerializeField]
    private TextMeshProUGUI voteTimeTMP;
    public void SetVoteTime_Hook(int _, int value)
    {
        voteTimeTMP.text = string.Format("{0}s", value);
        UpdateGameRuleOverview();
    }
    public void OnChangeVoteTime(bool isPlus)
    {
        voteTime = Mathf.Clamp(voteTime + (isPlus ? 5 : -5), 0, 300);
        isRecommandRule = false;
        isRecommandRuleToggle.isOn = false;
    }

    [SyncVar(hook = nameof(SetAnonymousVotes_Hook))]
    private bool anonymousVotes;
    [SerializeField]
    private Toggle anonymousVotesToggle;
    public void SetAnonymousVotes_Hook(bool _, bool value)
    {
        UpdateGameRuleOverview();
    }
    public void OnAnonymousVotesToggle(bool value)
    {
        isRecommandRule = false;
        isRecommandRuleToggle.isOn = false;
        anonymousVotes = value;
    }

    [SyncVar(hook = nameof(SetMoveSpeed_Hook))]
    private float moveSpeed;
    [SerializeField]
    private TextMeshProUGUI moveSpeedTMP;
    public void SetMoveSpeed_Hook(float _, float value)
    {
        moveSpeedTMP.text = string.Format("{0:0:0}x", value);
        UpdateGameRuleOverview();
    }
    public void OnChangeMoveSpeed(bool isPlus)
    {
        moveSpeed = Mathf.Clamp(moveSpeed + (isPlus ? 0.25f : -0.25f), 0.5f, 3f);
        isRecommandRule = false;
        isRecommandRuleToggle.isOn = false;
    }

    [SyncVar(hook = nameof(SetCrewSight_Hook))]
    private float crewSight;
    [SerializeField]
    private TextMeshProUGUI crewSightTMP;
    public void SetCrewSight_Hook(float _, float value)
    {
        crewSightTMP.text = string.Format("{0:0:0}x", value);
        UpdateGameRuleOverview();
    }
    public void OnChangeCrewSight(bool isPlus)
    {
        crewSight = Mathf.Clamp(crewSight + (isPlus ? 0.25f : -0.25f), 0.25f, 5f);
        isRecommandRule = false;
        isRecommandRuleToggle.isOn = false;
    }

    [SyncVar(hook = nameof(SetImposterSight_Hook))]
    private float imposterSignt;
    [SerializeField]
    private TextMeshProUGUI imposterSigntTMP;
    public void SetImposterSight_Hook(float _, float value)
    {
        imposterSigntTMP.text = string.Format("{0:0:0}x", value);
        UpdateGameRuleOverview();
    }
    public void OnChangeImposterSight(bool isPlus)
    {
        imposterSignt = Mathf.Clamp(imposterSignt + (isPlus ? 0.25f : -0.25f), 0.25f, 5f);
        isRecommandRule = false;
        isRecommandRuleToggle.isOn = false;
    }

    [SyncVar(hook = nameof(SetKillCooldown_Hook))]
    private float killCooldown;
    [SerializeField]
    private TextMeshProUGUI killCooldownTMP;
    public void SetKillCooldown_Hook(float _, float value)
    {
        killCooldownTMP.text = string.Format("{0:0:0}s", value);
        UpdateGameRuleOverview();
    }
    public void OnChangeKillCooldown(bool isPlus)
    {
        killCooldown = Mathf.Clamp(killCooldown + (isPlus ? 2.5f : -2.5f), 10f, 60f);
        isRecommandRule = false;
        isRecommandRuleToggle.isOn = false;
    }

    [SyncVar(hook = nameof(SetKillRange_Hook))]
    private EKillRange killRange;
    [SerializeField]
    private TextMeshProUGUI killRangeTMP;
    public void SetKillRange_Hook(EKillRange _, EKillRange value)
    {
        killRangeTMP.text = value.ToString();
        UpdateGameRuleOverview();
    }
    public void OnChangeKillRange(bool isPlus)
    {
        killRange = (EKillRange)Mathf.Clamp((int)killRange + (isPlus ? 1 : -1), 0, 2);
        isRecommandRule = false;
        isRecommandRuleToggle.isOn = false;
    }

    [SyncVar(hook = nameof(SetVisualTasks_Hook))]
    private bool visualTasks;
    [SerializeField]
    private Toggle visualTasksToggle;
    public void SetVisualTasks_Hook(bool _, bool value)
    {
        UpdateGameRuleOverview();
    }
    public void OnVisualTasksToggle(bool value)
    {
        isRecommandRule = false;
        isRecommandRuleToggle.isOn = false;
        visualTasks = value;
    }

    [SyncVar(hook = nameof(SetTaskBarUpdates_Hook))]
    private ETaskBarUpdates taskBarUpdates;
    [SerializeField]
    private TextMeshProUGUI taskBarUpdatesTMP;
    public void SetTaskBarUpdates_Hook(ETaskBarUpdates _, ETaskBarUpdates value)
    {
        taskBarUpdatesTMP.text = value.ToString();
        UpdateGameRuleOverview();
    }
    public void OnChangeTaskBarUpdates(bool isPlus)
    {
        taskBarUpdates = (ETaskBarUpdates)Mathf.Clamp((int)taskBarUpdates + (isPlus ? 1 : -1), 0, 2);
        isRecommandRule = false;
        isRecommandRuleToggle.isOn = false;
    }

    [SyncVar(hook = nameof(SetCommonTask_Hook))]
    private int commonTask;
    [SerializeField]
    private TextMeshProUGUI commonTaskTMP;
    public void SetCommonTask_Hook(int _, int value)
    {
        commonTaskTMP.text = value.ToString();
        UpdateGameRuleOverview();
    }
    public void OnChangeCommonTask(bool isPlus)
    {
        commonTask = Mathf.Clamp(commonTask + (isPlus ? 1 : -1), 0, 2);
        isRecommandRule = false;
        isRecommandRuleToggle.isOn = false;
    }

    [SyncVar(hook = nameof(SetComplexTask_Hook))]
    private int complexTask;
    [SerializeField]
    private TextMeshProUGUI complexTaskTMP;
    public void SetComplexTask_Hook(int _, int value)
    {
        complexTaskTMP.text = value.ToString();
        UpdateGameRuleOverview();
    }
    public void OnChangeComplexTask(bool isPlus)
    {
        complexTask = Mathf.Clamp(complexTask + (isPlus ? 1 : -1), 0, 3);
        isRecommandRule = false;
        isRecommandRuleToggle.isOn = false;
    }

    [SyncVar(hook = nameof(SetSimpleTask_Hook))]
    private int simpleTask;
    [SerializeField]
    private TextMeshProUGUI simpleTaskTMP;
    public void SetSimpleTask_Hook(int _, int value)
    {
        simpleTaskTMP.text = value.ToString();
        UpdateGameRuleOverview();
    }
    public void OnChangeSimpleTask(bool isPlus)
    {
        simpleTask = Mathf.Clamp(simpleTask + (isPlus ? 1 : -1), 0, 5);
        isRecommandRule = false;
        isRecommandRuleToggle.isOn = false;
    }

    [SerializeField]
    private TextMeshProUGUI gameRuleOverview;

    public void UpdateGameRuleOverview()
    {
        var manager = NetworkManager.singleton as AmongUsRoomManager;
        StringBuilder sb = new StringBuilder(isRecommandRule ? "추천 설정\n" : "커스텀 설정n");
        sb.Append("맵: The Skeld\n");
        sb.Append($"#임포스터: {manager.imposterCount}\n");
        sb.Append(string.Format("Confirm Ejects: {0}\n", confirmEjects ? "켜짐" : "꺼짐"));
        sb.Append($"긴급 회의: {emergencyMeetings}\n");
        sb.Append(string.Format("Anomymous Votes: {0}\n", anonymousVotes ? "켜짐" : "꺼짐"));
        sb.Append($"긴급 회의 쿨타임: {emergencyMeetingsCooldown}\n");
        sb.Append($"회의 제한 시간: {meetingsTime}\n");
        sb.Append($"투표 제한 시간: {voteTime}\n");
        sb.Append($"이동 속도: {moveSpeed}\n");
        sb.Append($"크루원 시야: {crewSight}\n");
        sb.Append($"임포스터 시야: {imposterSignt}\n");
        sb.Append($"킬 쿨타임: {killCooldown}\n");
        sb.Append($"킬 범위: {killRange}\n");
        sb.Append($"Task Bar Updates: {taskBarUpdates}\n");
        sb.Append(string.Format("Visual Tasks: {0}\n", visualTasks ? "켜짐" : "꺼짐"));
        sb.Append($"공통 임무: {commonTask}\n");
        sb.Append($"복잡한 임무: {complexTask}\n");
        sb.Append($"간단한 임무: {simpleTask}\n");
        gameRuleOverview.text = sb.ToString();
    }

    private void SetRecommendGameRule()
    {
        isRecommandRule = true;
        confirmEjects = true;
        emergencyMeetings = 1;
        emergencyMeetingsCooldown = 15;
        meetingsTime = 15;
        voteTime = 120;
        moveSpeed = 1;
        crewSight = 1;
        imposterSignt = 1.5f;
        killCooldown = 45f;
        killRange = EKillRange.Normal;
        visualTasks = true;
        taskBarUpdates = ETaskBarUpdates.Meetings;
        commonTask = 1;
        complexTask = 1;
        simpleTask = 2;
    }

    private void Start()
    {
        if (isServer)
        {
            SetRecommendGameRule();
        }
    }
}
