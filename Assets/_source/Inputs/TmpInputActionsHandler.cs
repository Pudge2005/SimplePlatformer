using System;
using UnityEditor;
using UnityEngine;

namespace Game
{
    public class TmpInputActionsHandler : MonoBehaviour
    {

        [SerializeField] private TmpCharacterController2D _controller;

        [Header("DEBUG character stats")]
        [SerializeField] private float _moveSpeed = 2f;
        [SerializeField] private float _jumpHeight = 2f;

        [Header("DEBUG controller settings")]
        [SerializeField, Range(-50f, -0.1f)] private float _gravity = -15f;
        [SerializeField] private float _groundCheckOffset = 0.9f;
        [SerializeField] private float _groundCheckRadius = 0.5f;
        [SerializeField] private LayerMask _groundLayers = 1;

        private const float _terminalVelocity = 53f;
        private const float _threshold = 0.01f;
        private const float _defaultVerticalVelocity = -2f;

        private float _verticalVelocity;

        private bool _grounded;
        private float _horizontalInput;
        private bool _jumpRequested;


        private static readonly Collider2D[] _collidersBuffer = new Collider2D[1024];
        private static readonly ReadOnlyMemory<Collider2D> _collidersMemBuffer = new(_collidersBuffer);

        public void Move_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            _horizontalInput = context.ReadValue<float>();
        }

        public void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            _jumpRequested = true;
        }


        private void Update()
        {
            _grounded = CheckGroundedState();
            HandleVerticalMovement();
            HandleHorizontalMovement();
        }

        private void OnDrawGizmos()
        {
            Handles.color = Color.cyan;
            Vector2 pos = _controller.transform.position;
            pos.y += _groundCheckOffset;
            Handles.DrawWireDisc(pos, new Vector3(0, 0, -1), _groundCheckRadius);
        }


        private bool CheckGroundedState()
        {
            Vector2 pos = _controller.transform.position;
            pos.y += _groundCheckOffset;

            var filter = new ContactFilter2D
            {
                layerMask = _groundLayers,
                useLayerMask = true,
                useTriggers = false,
            };

            int count = Physics2D.OverlapCircle(pos, _groundCheckRadius, filter, _collidersBuffer);

            var span = _collidersMemBuffer[..count].Span;

            foreach (var col in span)
            {
                if (col.gameObject == _controller.gameObject) //если вдруг нужно будет прыгать "по головам"
                    continue;

                return true;
            }

            return false;
        }

        private void HandleVerticalMovement()
        {
            if (_grounded)
            {
                ClampDownForce();
                HandleJumpRequest();
            }

            _jumpRequested = false;

            ApplyGravity();
        }

        private void ApplyGravity()
        {
            if (_verticalVelocity > -_terminalVelocity)
                _verticalVelocity += _gravity * Time.deltaTime;
        }

        private void HandleHorizontalMovement()
        {
            float frameTime = Time.deltaTime;
            Vector2 movement;
            movement.x = _horizontalInput * _moveSpeed;
            movement.y = _verticalVelocity;

            _controller.Move(movement * frameTime);
        }


        private void HandleJumpRequest()
        {
            if (_jumpRequested)
                _verticalVelocity = (float)System.Math.Sqrt(_jumpHeight * 2f * -_gravity);
        }

        private void ClampDownForce()
        {
            if (_verticalVelocity < 0f)
                _verticalVelocity = _defaultVerticalVelocity;
        }
    }
}