using UnityEngine;
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
    public Time AwarenessDuration;
    public float KickSpeed;
    public float KickPrecision;
    public float BallCatchThreshold;
    public Quaternion ActionAngle;
    public float OffensiveStrength;
    public float DefensiveStrength;

    public Sprite imageA;
    public Sprite imageB;
}
