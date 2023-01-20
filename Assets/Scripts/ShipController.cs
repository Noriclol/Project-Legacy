using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

// ReSharper disable All

public class ShipController : MonoBehaviour
{
  private InputMaster Controls;
  private GameObject Instance;
  
  
  private InputAction _ForwardBack;
  private InputAction _LeftRight;
  
  private InputAction _Roll;
  private InputAction _Pitch;
  private InputAction _Yaw;

  [Header("Thrusters")]
  [Space]
  public float ForwardBack;
  public float LeftRight;
  [Space]
  public float Roll;
  public float Pitch;
  public float Yaw;
  [Space]
  [Header("Modifiers")] 
  [Space]
  public float ThrustMultiplier = 1;






  public bool RandomPick;
  
  public Ship ship;
  
  private Rigidbody rigidbody;
  

  [SerializeField]
  private Transform orientation;

  [SerializeField] 
  private List<Ship> ships;


  private void Start()
  {
    Controls = new InputMaster();

    rigidbody = GetComponent<Rigidbody>();
    
    
    if (RandomPick)
      ship = ships[Random.Range(0, ships.Count)];

    rigidbody.mass = ship.mass;
    Instance = Instantiate(ship.Prefab, transform.position, Quaternion.identity);
    Instance.transform.parent = this.transform;
  }

  private void OnEnable()
  {
    // References
    
    
    Controls = new InputMaster();
    
    
    
    // Input Setup
    
    
    this._ForwardBack = Controls.Ship.ForwardBack;
    this._LeftRight = Controls.Ship.LeftRight;
    
    this._Roll = Controls.Ship.Roll;
    this._Pitch = Controls.Ship.Pitch;
    this._Yaw = Controls.Ship.Yaw;
    
    Controls.Enable();
    
    _ForwardBack.Enable();
    _LeftRight.Enable();
    
    _Roll.Enable();
    _Pitch.Enable();
    _Yaw.Enable();
    
    

    
    
    _ForwardBack.performed += RegisterForwardBack; 
    _ForwardBack.canceled += RegisterForwardBack;

    _LeftRight.performed += RegisterLeftRight;
    _LeftRight.canceled += RegisterLeftRight;
    
    _Roll.performed += RegisterRoll;
    _Roll.canceled += RegisterRoll;
    
    _Yaw.performed += RegisterYaw;
    _Yaw.canceled += RegisterYaw;
    
    _Pitch.performed += RegisterPitch;
    _Pitch.canceled += RegisterPitch;
    
  }
  
  
  private void OnDisable()
  {
    _ForwardBack.performed -= RegisterForwardBack; 
    _ForwardBack.canceled -= RegisterForwardBack;

    _LeftRight.performed -= RegisterLeftRight;
    _LeftRight.canceled -= RegisterLeftRight;
    
    _Roll.performed -= RegisterRoll;
    _Roll.canceled -= RegisterRoll;
    
    _Yaw.performed -= RegisterYaw;
    _Yaw.canceled -= RegisterYaw;
    
    _Pitch.performed -= RegisterPitch;
    _Pitch.canceled -= RegisterPitch;
    
    
    
    _ForwardBack.Disable();
    _LeftRight.Disable();
    
    _Roll.Disable();
    _Pitch.Disable();
    _Yaw.Disable();
    
    Controls.Disable();
  }

  private void FixedUpdate()
  {



    if (ForwardBack >= 1)
    {
      ThrusterForward();
      print("Forward");
    }
    if (ForwardBack <= -1)
    {
      ThrusterBackward();
      print("Backward");
    }
    if (Roll <= -1 || Roll >= 1)
      ThrusterRoll();
    if (Yaw <= -1 || Yaw >= 1)
      ThrusterYaw();
      
     if (Pitch <= -1 || Pitch >= 1)
      ThrusterPitch();

    
    
    // if (ForwardBack >= 1)
    //   ThrusterForward();
    //
    // if (ForwardBack <= -1)
    //   ThrusterBackward();
    //
    // if (ForwardBack >= 1)
    //   ThrusterForward();
    //
    // if (ForwardBack <= -1)
    //   ThrusterBackward();
    //
    // if (ForwardBack >= 1)
    //   ThrusterForward();
    //
    // if (ForwardBack <= -1)
    //   ThrusterBackward();
    
  }


  private void RegisterPitch(InputAction.CallbackContext obj) => Pitch = obj.ReadValue<float>();
  private void RegisterYaw(InputAction.CallbackContext obj) => Yaw = obj.ReadValue<float>();
  private void RegisterRoll(InputAction.CallbackContext obj) => Roll = obj.ReadValue<float>();
  private void RegisterLeftRight(InputAction.CallbackContext obj) => LeftRight = obj.ReadValue<float>();
  private void RegisterForwardBack(InputAction.CallbackContext obj) => ForwardBack = obj.ReadValue<float>();



  private void ThrusterForward() => rigidbody.AddForce(orientation.forward * ship.ForwardThrust);
  private void ThrusterBackward() => rigidbody.AddForce(-orientation.forward * ship.BackwardThrust);

  private void ThrusterPitch() => rigidbody.AddTorque(new Vector3(Pitch * ship.PitchThrust,0,0) );
  private void ThrusterYaw()   => rigidbody.AddTorque(new Vector3(0,Yaw * ship.YawThrust,0));
  private void ThrusterRoll()  => rigidbody.AddTorque(new Vector3(0,0,Roll * ship.RollThrust));



}
