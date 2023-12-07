//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts/GameMobile/MobileController.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @MobileController: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @MobileController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MobileController"",
    ""maps"": [
        {
            ""name"": ""MobileCameraController"",
            ""id"": ""d2adef17-4290-4f3c-a998-845459c51035"",
            ""actions"": [
                {
                    ""name"": ""LookMobile"",
                    ""type"": ""Value"",
                    ""id"": ""16d5e48f-4b8a-4354-ab84-4a86b7be35f3"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""12f5bdae-92c0-468d-96b4-ffc4e0c6c059"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cfc2e3fd-7433-4e89-915c-17e793445f82"",
                    ""path"": ""<Touchscreen>/delta/x"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": ""Touch"",
                    ""action"": ""LookMobile"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ce855c28-f148-43a7-9b46-da44cc487e44"",
                    ""path"": ""<Touchscreen>/delta/y"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": ""Touch"",
                    ""action"": ""LookMobile"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9c91fb3a-ad6c-4fe7-b066-72c0efe6348d"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Touch"",
            ""bindingGroup"": ""Touch"",
            ""devices"": []
        }
    ]
}");
        // MobileCameraController
        m_MobileCameraController = asset.FindActionMap("MobileCameraController", throwIfNotFound: true);
        m_MobileCameraController_LookMobile = m_MobileCameraController.FindAction("LookMobile", throwIfNotFound: true);
        m_MobileCameraController_Move = m_MobileCameraController.FindAction("Move", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // MobileCameraController
    private readonly InputActionMap m_MobileCameraController;
    private List<IMobileCameraControllerActions> m_MobileCameraControllerActionsCallbackInterfaces = new List<IMobileCameraControllerActions>();
    private readonly InputAction m_MobileCameraController_LookMobile;
    private readonly InputAction m_MobileCameraController_Move;
    public struct MobileCameraControllerActions
    {
        private @MobileController m_Wrapper;
        public MobileCameraControllerActions(@MobileController wrapper) { m_Wrapper = wrapper; }
        public InputAction @LookMobile => m_Wrapper.m_MobileCameraController_LookMobile;
        public InputAction @Move => m_Wrapper.m_MobileCameraController_Move;
        public InputActionMap Get() { return m_Wrapper.m_MobileCameraController; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MobileCameraControllerActions set) { return set.Get(); }
        public void AddCallbacks(IMobileCameraControllerActions instance)
        {
            if (instance == null || m_Wrapper.m_MobileCameraControllerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MobileCameraControllerActionsCallbackInterfaces.Add(instance);
            @LookMobile.started += instance.OnLookMobile;
            @LookMobile.performed += instance.OnLookMobile;
            @LookMobile.canceled += instance.OnLookMobile;
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
        }

        private void UnregisterCallbacks(IMobileCameraControllerActions instance)
        {
            @LookMobile.started -= instance.OnLookMobile;
            @LookMobile.performed -= instance.OnLookMobile;
            @LookMobile.canceled -= instance.OnLookMobile;
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
        }

        public void RemoveCallbacks(IMobileCameraControllerActions instance)
        {
            if (m_Wrapper.m_MobileCameraControllerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMobileCameraControllerActions instance)
        {
            foreach (var item in m_Wrapper.m_MobileCameraControllerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MobileCameraControllerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MobileCameraControllerActions @MobileCameraController => new MobileCameraControllerActions(this);
    private int m_TouchSchemeIndex = -1;
    public InputControlScheme TouchScheme
    {
        get
        {
            if (m_TouchSchemeIndex == -1) m_TouchSchemeIndex = asset.FindControlSchemeIndex("Touch");
            return asset.controlSchemes[m_TouchSchemeIndex];
        }
    }
    public interface IMobileCameraControllerActions
    {
        void OnLookMobile(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
    }
}
