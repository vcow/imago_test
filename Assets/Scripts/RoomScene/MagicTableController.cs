using UnityEngine;
using Zenject;

namespace RoomScene
{
	public class MagicTableController : InteractiveObject
	{
		[Inject] private readonly ZenjectSceneLoader _sceneLoader;

		private bool _interactable = true;

		public override async void Interact(float distance)
		{
			if (!_interactable)
			{
				return;
			}

			_interactable = false;
			await _sceneLoader.LoadSceneAsync(Const.InTheGameSceneName);
			_interactable = true;
		}
	}
}