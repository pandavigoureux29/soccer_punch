using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerDataAsset : ScriptableObject {

    public string PrefabPath;
    public float Cooldown;
    public float MaxLife;
    public float LifeCost;
    public float Speed;
    public float TravelDistance;
    public float ActionRadius;
    public float AwarenessDuration;
    public float KickSpeed;
    public float KickPrecision;
    public float BallCatchThreshold;
    public Quaternion ActionAngle;
    public float OffensiveStrength;
    public float DefensiveStrength;
    
    public Sprite imageA;
    public Sprite imageB;

    //State Machine Preferences
    public StatePreference PreferredPlayerState = new StatePreference(typeof(PlayerStateMachineComponent.PlayerState));
    public StatePreference PreferredIdleState = new StatePreference(typeof(PlayerStateMachineComponent.IdleState));
    public StatePreference PreferredBallState = new StatePreference(typeof(PlayerStateMachineComponent.BallState));
    public StatePreference PreferredEnemyState = new StatePreference(typeof(PlayerStateMachineComponent.EnemyState));
    public StatePreference PreferredKickState = new StatePreference(typeof(PlayerStateMachineComponent.KickState));
    public StatePreference PreferredDeadState = new StatePreference(typeof(PlayerStateMachineComponent.DeadState));
}

[System.Serializable]
public class StatePreference
{
    [SerializeField]
    public Type RelatedStateEnum;
    [SerializeField]
    public List<float> PreferencesStrength;
    [SerializeField]
    public List<string> StatesNames;

    public StatePreference(Type enumType, List<float> preferencesStrength = null, List<string> statesNames = null)
    {
        if (enumType.IsEnum)
        {
            RelatedStateEnum = enumType;
            if (preferencesStrength == null)
            {
                PreferencesStrength = new List<float>();
                StatesNames = new List<string>();
                var listLength = Enum.GetNames(enumType).Length;
                for (int i = 0; i < listLength; ++i)
                {
                    PreferencesStrength.Add(i+1);
                    StatesNames.Add(Enum.GetNames(enumType)[i]);
                }
            }
            else
            {
                PreferencesStrength = preferencesStrength;
                StatesNames = statesNames;
            }
        }
    }    
}
