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
    public StatePreference PreferredIdleState = new StatePreference(typeof(PlayerStateMachineComponent.IdleState));
    public StatePreference PreferredBallState = new StatePreference(typeof(PlayerStateMachineComponent.BallState));
    public StatePreference PreferredEnemyState = new StatePreference(typeof(PlayerStateMachineComponent.EnemyState));
    public StatePreference PreferredKickState = new StatePreference(typeof(PlayerStateMachineComponent.KickState));
    public StatePreference PreferredDeadState = new StatePreference(typeof(PlayerStateMachineComponent.DeadState));
}

//public class StatePreference
//{
//    public int PreferredState;
//    public float PreferenceStrength;

//    public StatePreference(int preferredState, float preferenceStrength = 100)
//    {
//        PreferredState = preferredState;
//        PreferenceStrength = preferenceStrength;
//    }

//    public StatePreference()
//    {
//        PreferredState = 0;
//        PreferenceStrength = 100f;
//    }
//}

public class StatePreference
{
    public Type RelatedStateEnum;
    public List<float> PreferencesStrength;

    public StatePreference(Type enumType, List<float> preferencesStrength = null)
    {
        if (enumType.IsEnum)
        {
            RelatedStateEnum = enumType;
            if (preferencesStrength == null)
            {
                PreferencesStrength = new List<float>();
                var listLength = Enum.GetNames(enumType).Length;
                for (int i = 0; i < listLength; ++i)
                {
                    PreferencesStrength.Add(1f / listLength);
                }
            }
            else
            {
                PreferencesStrength = preferencesStrength;
            }
        }
    }    
}
