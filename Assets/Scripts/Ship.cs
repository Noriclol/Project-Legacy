using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(fileName = "Unnamed Asteroid", menuName = "ScriptableObjects/Ships/Ship")]
public class Ship : ScriptableObject
{
    public GameObject Prefab;
    
    public float mass = 5; 
    
    public float ForwardThrust = 20f;
    public float BackwardThrust = 10f;
    public float LeftThrust = 7f;
    public float RightThrust = 7f;
    public float UpThrust = 5f;
    public float DownThrust = 5f;

    public float RollThrust = 5f;
    public float PitchThrust = 5f;
    public float YawThrust = 5f;


}
