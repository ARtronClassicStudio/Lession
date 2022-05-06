using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class PlayerSystem : ComponentSystem
{

    private EntityQuery query;
    private InputAction InputPosition;
    private InputAction InputCamera;
    private InputAction InputSprint;
     
    private float2 pose;
    private float2 camera;
    private float sprint;

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
        InputSprint = new InputAction(name: "sprint", binding: "<Keyboard>/Space");

        InputSprint.performed += context => { sprint = context.ReadValue<float>(); };
        InputSprint.started += context => { sprint = context.ReadValue<float>(); };
        InputSprint.canceled += context => { sprint = context.ReadValue<float>(); };
        InputSprint.Enable();
        #endregion

    }

    protected override void OnStopRunning()
    {
        InputPosition.Disable();
        InputCamera.Disable();
        InputSprint.Disable();
    }

    protected override void OnUpdate()
    {
        Entities.With(query).ForEach(
            (Entity entity, ref PlayerData player) => 
            {
                player.Move = pose;
                player.MoveCamera = camera;
                player.Sprint = sprint;

            });

    }
}

   
