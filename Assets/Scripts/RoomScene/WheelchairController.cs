using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

namespace RoomScene
{
	[DisallowMultipleComponent, RequireComponent(typeof(PlayerInput))]
	public class WheelchairController : MonoBehaviour
	{
		[SerializeField] private WheelCollider _leftWheelCollider;
		[SerializeField] private WheelCollider _rightWheelCollider;
		[SerializeField] private WheelCollider _helmCollider;
		[SerializeField] private Transform _leftWheel;
		[SerializeField] private Transform _rightWheel;
		[SerializeField] private float _torque;
		[SerializeField] private float _brakeTorque;
		[Header("Animation controller"), SerializeField] private Rigidbody _boyRigidBody;
		[SerializeField] private Animator _boyAnimator;

		private const float HelmAngle = 45f;

		private static readonly int LeftRightBlend = Animator.StringToHash("left_right_blend");
		private static readonly int FrontBackBlend = Animator.StringToHash("front_back_blend");

		private Vector2 _moveValue;

		public void OnMove(InputAction.CallbackContext context)
		{
			_moveValue = context.ReadValue<Vector2>();

			_helmCollider.steerAngle = _moveValue.x * HelmAngle;

			if (_moveValue.y.Equals(0f))
			{
				_helmCollider.motorTorque = 0f;
				_helmCollider.brakeTorque = _brakeTorque;
			}
			else
			{
				_helmCollider.motorTorque = _torque * _moveValue.y;
				_helmCollider.brakeTorque = 0f;
			}
		}

		private void Update()
		{
			_leftWheelCollider.GetWorldPose(out var pos, out var rot);
			_leftWheel.position = pos;
			_leftWheel.rotation = rot;

			_rightWheelCollider.GetWorldPose(out pos, out rot);
			_rightWheel.position = pos;
			_rightWheel.rotation = rot;

			var frontBackBlend = Mathf.Clamp(_moveValue.y * _boyRigidBody.linearVelocity.magnitude, -1f, 1f);
			var leftRightBlend = Mathf.Clamp(_boyRigidBody.angularVelocity.y, -1f, 1f);
			_boyAnimator.SetFloat(LeftRightBlend, Mathf.Clamp01((leftRightBlend + 1f) * 0.5f));
			_boyAnimator.SetFloat(FrontBackBlend, Mathf.Clamp01((frontBackBlend + 1f) * 0.5f));
		}

		private void OnValidate()
		{
			Assert.IsNotNull(_leftWheelCollider, "_leftWheelCollider != null");
			Assert.IsNotNull(_rightWheelCollider, "_rightWheelCollider != null");
			Assert.IsNotNull(_helmCollider, "_helmCollider != null");
			Assert.IsNotNull(_leftWheel, "_leftWheel != null");
			Assert.IsNotNull(_rightWheel, "_rightWheel != null");
			Assert.IsNotNull(_boyRigidBody, "_boyRigidBody != null");
			Assert.IsNotNull(_boyAnimator, "_boyAnimator != null");
		}
	}
}