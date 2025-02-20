using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

namespace InTheGameScene
{
	[DisallowMultipleComponent]
	public class CharacterAnimationController : MonoBehaviour
	{
		[SerializeField] private NavMeshAgent _agent;
		[SerializeField] private Animator _animator;

		private static readonly int Move = Animator.StringToHash("move");
		private static readonly int Left = Animator.StringToHash("left");
		private static readonly int Right = Animator.StringToHash("right");
		private static readonly int Forward = Animator.StringToHash("forward");
		private static readonly int Backward = Animator.StringToHash("backward");

		private int _lastDirection = Forward;


		private void Update()
		{
			var velocity = _agent.velocity;
			if (velocity == Vector3.zero)
			{
				_animator.SetBool(Move, false);
				return;
			}

			_animator.SetBool(Move, true);

			var ang = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
			ang = ang < 0 ? 360f + ang : ang;
			var direction = ang switch
			{
				< 315f and >= 225f => Forward,
				< 225f and >= 135f => Left,
				< 135f and >= 45f => Backward,
				_ => Right
			};

			if (direction == _lastDirection)
			{
				return;
			}

			_lastDirection = direction;
			_animator.SetTrigger(direction);
		}

		private void OnValidate()
		{
			Assert.IsNotNull(_agent, "_agent != null");
			Assert.IsNotNull(_animator, "_animator != null");
		}
	}
}