using InTheGameScene.Signals;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace InTheGameScene
{
	[DisallowMultipleComponent, RequireComponent(typeof(Collider2D))]
	public class TerminalHitPointController : MonoBehaviour
	{
		[SerializeField] private GameObject _arrow;
		[SerializeField] private Transform _terminal;
		[SerializeField] private Collider2D _boyCollider;
		[SerializeField] private CinemachineTargetGroup _targetGroup;

		[Inject] private readonly SignalBus _signalBus;

		private void Start()
		{
			if (_arrow)
			{
				_arrow.SetActive(false);
			}
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other != _boyCollider)
			{
				return;
			}

			if (_arrow)
			{
				_arrow.SetActive(true);
			}

			_signalBus.TryFire(new UIMessageSignal("You found the terminal! Use it."));
			_targetGroup.AddMember(_terminal, 1f, 1f);
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (other != _boyCollider)
			{
				return;
			}

			if (_arrow)
			{
				_arrow.SetActive(false);
			}

			if (_targetGroup)
			{
				_targetGroup.RemoveMember(_terminal);
			}
		}

		private void OnValidate()
		{
			Assert.IsNotNull(_terminal, "_terminal != null");
			Assert.IsNotNull(_boyCollider, "_boyCollider != null");
			Assert.IsNotNull(_targetGroup, "_targetGroup != null");
		}
	}
}