using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace InTheGameScene
{
	[DisallowMultipleComponent, RequireComponent(typeof(Collider2D))]
	public class TerminalController : MonoBehaviour
	{
		[SerializeField] private Collider2D _boyCollider;

		[Inject] private readonly ZenjectSceneLoader _sceneLoader;

		private async void OnTriggerEnter2D(Collider2D other)
		{
			if (other != _boyCollider)
			{
				return;
			}

			await _sceneLoader.LoadSceneAsync(Const.RoomSceneName);
		}

		private void OnValidate()
		{
			Assert.IsNotNull(_boyCollider, "_boyCollider != null");
		}
	}
}