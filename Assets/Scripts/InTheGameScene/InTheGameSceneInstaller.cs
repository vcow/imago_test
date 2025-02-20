using InTheGameScene.Signals;
using UnityEngine;
using Zenject;

namespace InTheGameScene
{
	[DisallowMultipleComponent]
	public sealed class InTheGameSceneInstaller : MonoInstaller<InTheGameSceneInstaller>
	{
		public override void InstallBindings()
		{
			Container.DeclareSignal<UIMessageSignal>();
			Container.DeclareSignal<GameOverSignal>();
		}
	}
}