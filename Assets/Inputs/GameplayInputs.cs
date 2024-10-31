//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Inputs/GameplayInputs.inputactions
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

public partial class @GameplayInputs: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameplayInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameplayInputs"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""be6c4bf5-cce8-4353-ac99-d5a013e6256a"",
            ""actions"": [
                {
                    ""name"": ""MoveNorth"",
                    ""type"": ""Button"",
                    ""id"": ""ec7d4462-88fd-4f43-8928-11e27232ea53"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.1)"",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MoveSouth"",
                    ""type"": ""Button"",
                    ""id"": ""90c3aca1-1a2b-4b05-a9a6-0ecf98f6952b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.1)"",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MoveEast"",
                    ""type"": ""Button"",
                    ""id"": ""73cffe50-c322-4fb9-abca-558083ba5c9c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.1)"",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MoveWest"",
                    ""type"": ""Button"",
                    ""id"": ""b8d500fa-47da-4058-8b0d-e707024e92ed"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.1)"",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ShowPauseMenus"",
                    ""type"": ""Button"",
                    ""id"": ""e412642b-f162-4c33-9f57-0efea12c64c4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3cfa1491-41a9-4ca7-a280-5a43d52d446e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveNorth"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ecc743e3-3393-43b9-8bb9-fc74b1d7df00"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveNorth"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""84d99ad0-1484-4aab-991c-2d9fa37ffddc"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveSouth"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""552cac4e-9249-4cbe-9e57-4af35a458181"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveSouth"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3b8fdf28-c343-4bca-b5d3-32eceed31fd4"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShowPauseMenus"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""61722d3b-ca01-4c19-a1d2-ce562465d3e7"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShowPauseMenus"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""017cf7cf-4873-4b5d-96fb-7ee66d0562ad"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveEast"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0afe1dae-d4ca-4b75-8f45-5cff4abc5dc1"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveEast"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""12895781-0125-46b1-99c1-975f7f24c953"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveWest"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""761f79dc-7d53-4906-a9fa-980e6c0af36f"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveWest"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""2c59a263-0f77-4ae4-9037-7ce1f80ad724"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""8a2aebc2-0fb3-4a77-af5e-c5b8594a8266"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""8db554eb-4388-4196-b7d1-8b7674c915b0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2d8e19a1-bc0d-48dc-9c4c-ba5be6778dc5"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""ea502efe-d2b9-4a14-935b-bf4ac132a4b9"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""db46f6dd-c84c-44c8-956d-e452b573efde"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""bae529f8-76ee-4a28-9350-8b025a7fa6fd"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""44a5a784-1baf-4556-87b2-bc0acac40528"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""68abafd8-f61b-4c49-8d51-a70fc06f8d4f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""8c459c03-28d6-4d8c-97dd-34ce9d196619"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c6ffc3e2-3099-4b9e-807b-be5d4439bd09"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_MoveNorth = m_Player.FindAction("MoveNorth", throwIfNotFound: true);
        m_Player_MoveSouth = m_Player.FindAction("MoveSouth", throwIfNotFound: true);
        m_Player_MoveEast = m_Player.FindAction("MoveEast", throwIfNotFound: true);
        m_Player_MoveWest = m_Player.FindAction("MoveWest", throwIfNotFound: true);
        m_Player_ShowPauseMenus = m_Player.FindAction("ShowPauseMenus", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_Move = m_Menu.FindAction("Move", throwIfNotFound: true);
        m_Menu_Submit = m_Menu.FindAction("Submit", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_MoveNorth;
    private readonly InputAction m_Player_MoveSouth;
    private readonly InputAction m_Player_MoveEast;
    private readonly InputAction m_Player_MoveWest;
    private readonly InputAction m_Player_ShowPauseMenus;
    public struct PlayerActions
    {
        private @GameplayInputs m_Wrapper;
        public PlayerActions(@GameplayInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveNorth => m_Wrapper.m_Player_MoveNorth;
        public InputAction @MoveSouth => m_Wrapper.m_Player_MoveSouth;
        public InputAction @MoveEast => m_Wrapper.m_Player_MoveEast;
        public InputAction @MoveWest => m_Wrapper.m_Player_MoveWest;
        public InputAction @ShowPauseMenus => m_Wrapper.m_Player_ShowPauseMenus;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @MoveNorth.started += instance.OnMoveNorth;
            @MoveNorth.performed += instance.OnMoveNorth;
            @MoveNorth.canceled += instance.OnMoveNorth;
            @MoveSouth.started += instance.OnMoveSouth;
            @MoveSouth.performed += instance.OnMoveSouth;
            @MoveSouth.canceled += instance.OnMoveSouth;
            @MoveEast.started += instance.OnMoveEast;
            @MoveEast.performed += instance.OnMoveEast;
            @MoveEast.canceled += instance.OnMoveEast;
            @MoveWest.started += instance.OnMoveWest;
            @MoveWest.performed += instance.OnMoveWest;
            @MoveWest.canceled += instance.OnMoveWest;
            @ShowPauseMenus.started += instance.OnShowPauseMenus;
            @ShowPauseMenus.performed += instance.OnShowPauseMenus;
            @ShowPauseMenus.canceled += instance.OnShowPauseMenus;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @MoveNorth.started -= instance.OnMoveNorth;
            @MoveNorth.performed -= instance.OnMoveNorth;
            @MoveNorth.canceled -= instance.OnMoveNorth;
            @MoveSouth.started -= instance.OnMoveSouth;
            @MoveSouth.performed -= instance.OnMoveSouth;
            @MoveSouth.canceled -= instance.OnMoveSouth;
            @MoveEast.started -= instance.OnMoveEast;
            @MoveEast.performed -= instance.OnMoveEast;
            @MoveEast.canceled -= instance.OnMoveEast;
            @MoveWest.started -= instance.OnMoveWest;
            @MoveWest.performed -= instance.OnMoveWest;
            @MoveWest.canceled -= instance.OnMoveWest;
            @ShowPauseMenus.started -= instance.OnShowPauseMenus;
            @ShowPauseMenus.performed -= instance.OnShowPauseMenus;
            @ShowPauseMenus.canceled -= instance.OnShowPauseMenus;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Menu
    private readonly InputActionMap m_Menu;
    private List<IMenuActions> m_MenuActionsCallbackInterfaces = new List<IMenuActions>();
    private readonly InputAction m_Menu_Move;
    private readonly InputAction m_Menu_Submit;
    public struct MenuActions
    {
        private @GameplayInputs m_Wrapper;
        public MenuActions(@GameplayInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Menu_Move;
        public InputAction @Submit => m_Wrapper.m_Menu_Submit;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void AddCallbacks(IMenuActions instance)
        {
            if (instance == null || m_Wrapper.m_MenuActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MenuActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Submit.started += instance.OnSubmit;
            @Submit.performed += instance.OnSubmit;
            @Submit.canceled += instance.OnSubmit;
        }

        private void UnregisterCallbacks(IMenuActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Submit.started -= instance.OnSubmit;
            @Submit.performed -= instance.OnSubmit;
            @Submit.canceled -= instance.OnSubmit;
        }

        public void RemoveCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMenuActions instance)
        {
            foreach (var item in m_Wrapper.m_MenuActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MenuActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MenuActions @Menu => new MenuActions(this);
    public interface IPlayerActions
    {
        void OnMoveNorth(InputAction.CallbackContext context);
        void OnMoveSouth(InputAction.CallbackContext context);
        void OnMoveEast(InputAction.CallbackContext context);
        void OnMoveWest(InputAction.CallbackContext context);
        void OnShowPauseMenus(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnSubmit(InputAction.CallbackContext context);
    }
}
