using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Bubble.Utils
{
    [CreateAssetMenu(fileName = "InputManager", menuName = "Custom/Data/InputManager")]
    public class InputManager : ScriptableObject ,PlayerInputActions.IPlayerActions, PlayerInputActions.IUIActions
    {
        public PlayerInputActions InputSystem;
        public PlayerInputActions.PlayerActions playerActions;
        public PlayerInputActions.UIActions uiActions;

        //These events are called in the OnEvent functions
        //When an input event is called these are called afterwards
        public event Action JumpEvent;
        public event Action JumpCancelledEvent;
        public event Action BasicAttackEvent;
        public event Action CrouchEvent;
        public event Action CrouchEventCancelled;
        public event Action SprintEvent;
        public event Action SprintEventCancelled;
        public event Action PauseEvent;
        public event Action<Vector2> MoveEvent;
        public event Action<Vector2> LookEvent;

        public event Action UIResumeEvent;
        public event Action UIClickEvent;
        public event Action<Vector2> UIPointEvent;
        public event Action<Vector2> UINavigateEvent;
        public event Action UIOnPreviousEvent;
        public event Action UIOnNextEvent;
        public event Action UIOnSubmitEvent;
        public event Action UIOnCancelEvent;
        public event Action UIOnRightClickEvent;


        private void OnEnable()
        {
            if (InputSystem == null)
            {
                InputSystem = new PlayerInputActions();

                playerActions = InputSystem.Player;
                uiActions = InputSystem.UI;

                // SetCallbacks maps the events from the InputSystem to the functions in this script,
                // so when an event is called in the input system the corresponding function here is called.
                playerActions.SetCallbacks(this);
                uiActions.SetCallbacks(this);
                // You to call it twice because UIActions and PlayerActions are two different interfaces.

                EnablePlayerActions();
            }
        }

        // Makes sure only one action map is active at once
        public void EnablePlayerActions()
        {
            playerActions.Enable();
            uiActions.Disable();
        }

        public void EnableUIActions()
        {
            playerActions.Disable();
            uiActions.Enable();
        }

        // PlayerAction functions
        public void OnMove(InputAction.CallbackContext context)
        {
            MoveEvent?.Invoke(context.phase == InputActionPhase.Performed ? context.ReadValue<Vector2>() : Vector2.zero);
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                LookEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnBasic_Attack(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                BasicAttackEvent?.Invoke();
        }

        public void OnCrouch(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                CrouchEvent?.Invoke();
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                CrouchEventCancelled?.Invoke();
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

        public void OnSprint(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                SprintEvent?.Invoke();
            else if (context.phase == InputActionPhase.Canceled)
                SprintEventCancelled?.Invoke();
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
            throw new NotImplementedException();
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

        public void OnPrevious(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnNext(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
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
    }
}

