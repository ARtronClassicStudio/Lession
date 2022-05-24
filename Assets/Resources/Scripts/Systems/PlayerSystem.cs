using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerSystem : ComponentSystem
{

    private EntityQuery query;
    private InputAction InputPosition;
    private InputAction InputCamera;
    private InputAction InputFX;
     
    private float2 pose;
    private float2 camera;
    private float enabledFX ;

    protected override void OnCreate()
    {
        query = GetEntityQuery(ComponentType.ReadOnly<PlayerData>());
    }

    protected override void OnStartRunning()
    {
        #region  Joystick
        InputPosition = new InputAction(name: "move",binding:"<Gamepad>/leftStick");
        InputPosition.AddCompositeBinding("Dpad")
            .With(name: "Up", binding: "<Keyboard>/w")
            .With(name: "Down", binding: "<Keyboard>/s")
            .With(name: "Left", binding: "<Keyboard>/a")
            .With(name: "Right", binding: "<Keyboard>/d");

        InputPosition.performed += context => { pose = context.ReadValue<Vector2>(); };
        InputPosition.started += context => { pose = context.ReadValue<Vector2>(); };
        InputPosition.canceled += context => { pose = context.ReadValue<Vector2>(); };
        InputPosition.Enable();
        #endregion
        #region Camera
        InputCamera = new InputAction(name: "move", binding: "<Gamepad>/rightStick");
        InputCamera.AddCompositeBinding("Dpad");

        InputCamera.performed += context => { camera = context.ReadValue<Vector2>(); };
        InputCamera.started += context => { camera = context.ReadValue<Vector2>(); };
        InputCamera.canceled += context => { camera = context.ReadValue<Vector2>(); };
        InputCamera.Enable();
        #endregion
        #region Sprint
        InputFX = new InputAction(name: "sprint", binding: "<Keyboard>/Space");

        InputFX.performed += context => { enabledFX = context.ReadValue<float>(); };
        InputFX.started += context => { enabledFX = context.ReadValue<float>(); };
        InputFX.canceled += context => { enabledFX = context.ReadValue<float>(); };
        InputFX.Enable();
        #endregion

    }

    protected override void OnStopRunning()
    {
        InputPosition.Disable();
        InputCamera.Disable();
        InputFX.Disable();
    }

    protected override void OnUpdate()
    {
        Entities.With(query).ForEach(
            (Entity entity, ref PlayerData player) => 
            {
                player.Move = pose;
                player.MoveCamera = camera;
                player.enabledFX = Convert.ToBoolean(enabledFX);
            });

    }
}

   
