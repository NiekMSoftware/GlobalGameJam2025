using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Bubble.Utils
{
    public class PIM : MonoBehaviour ,PlayerInputActions.IPlayerActions, PlayerInputActions.IUIActions
    {
        private PlayerInputActions _inputSystem;
        private PlayerInputActions.PlayerActions _playerActions;
        private PlayerInputActions.UIActions _uiActions;

        //These events are called in the OnEvent functions
        //When an input event is called these are called afterwards
        public event Action JumpEvent;
        public event Action JumpCancelledEvent;
        public event Action BasicAttackEvent;
        public event Action PauseEvent;
        public event Action<Vector2> MoveEvent;
        public event Action<Vector2> LookEvent;

        public event Action UIResumeEvent;
        public event Action UIClickEvent;
        public event Action<Vector2> UIPointEvent;
        public event Action<Vector2> UINavigateEvent;
        public event Action UIOnSubmitEvent;
        public event Action UIOnCancelEvent;
        public event Action UIOnRightClickEvent;
        public bool KeyboardActive = true;

        void Update()
        {
            if (Keyboard.current.anyKey.isPressed)
            {
                _inputSystem.devices = new InputDevice[] { Keyboard.current,Mouse.current };
                KeyboardActive = true;
            }
            else if (Gamepad.current != null && Gamepad.current.buttonEast.isPressed)
            {
                _inputSystem.devices = new InputDevice[] { Gamepad.current };
                KeyboardActive = false;
            }
        }

        private void OnEnable()
        {
            if (_inputSystem == null)
            {
                _inputSystem = new PlayerInputActions();

                _playerActions = _inputSystem.Player;
                _uiActions = _inputSystem.UI;

                // SetCallbacks maps the events from the InputSystem to the functions in this script,
                // so when an event is called in the input system the corresponding function here is called.
                _playerActions.SetCallbacks(this);
                _uiActions.SetCallbacks(this);
                // You to call it twice because UIActions and PlayerActions are two different interfaces.

                EnablePlayerActions();
                _uiActions.Disable();
            }
        }

        // Makes sure only one action map is active at once
        public void EnablePlayerActions()
        {
            _playerActions.Enable();
            _uiActions.Disable();
        }

        public void EnableUIActions()
        {
            _playerActions.Disable();
            _uiActions.Enable();
        }

        // PlayerAction functions
        public void OnMove(InputAction.CallbackContext context)
        {
            MoveEvent?.Invoke(context.phase == InputActionPhase.Performed ? context.ReadValue<Vector2>() : Vector2.zero);
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                if (KeyboardActive)
                {
                    LookEvent?.Invoke(context.ReadValue<Vector2>());
                }
                else if (!KeyboardActive)
                {
                    LookEvent?.Invoke(context.ReadValue<Vector2>());
                }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                JumpEvent?.Invoke();
            }

            else if (context.phase == InputActionPhase.Canceled)
            {
                JumpCancelledEvent?.Invoke();
            }
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                PauseEvent?.Invoke();
                EnableUIActions();
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                BasicAttackEvent?.Invoke();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        // UIAction functions
        public void OnNavigate(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                UINavigateEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnResume(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                UIResumeEvent?.Invoke();
                EnablePlayerActions();
            }
        }

        public void OnPoint(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                UIPointEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                UIClickEvent?.Invoke();
        }

        public void OnSubmit(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnRightClick(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnMiddleClick(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnScrollWheel(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnUIOnSubmitEvent()
        {
            UIOnSubmitEvent?.Invoke();
        }

        public void OnUIOnCancelEvent()
        {
            UIOnCancelEvent?.Invoke();
        }

        public void OnUIOnRightClickEvent()
        {
            UIOnRightClickEvent?.Invoke();
        }

        public void OnR(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
                Time.timeScale = 1;
            }
        }
    }
}

