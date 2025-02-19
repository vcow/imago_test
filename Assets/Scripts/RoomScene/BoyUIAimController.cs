using System;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

namespace RoomScene
{
	[DisallowMultipleComponent, RequireComponent(typeof(PlayerInput))]
	public class BoyUIAimController : MonoBehaviour
	{
		[SerializeField] private Camera _camera;
		[SerializeField] private TextMeshProUGUI _message;

		private InteractiveObject _interactiveObject;
		private float _distance;

		public void OnFire(InputAction.CallbackContext context)
		{
			if (_interactiveObject)
			{
				_interactiveObject.Interact(_distance);
			}
		}

		private void Awake()
		{
			_message.text = string.Empty;
		}

		private void Update()
		{
			var ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 10f));
			if (!Physics.Raycast(ray, out var hitInfo, _camera.farClipPlane, LayerMask.GetMask("Interactive")))
			{
				_message.text = string.Empty;
				_interactiveObject = null;
				return;
			}

			_interactiveObject = hitInfo.collider.GetComponent<InteractiveObject>();
			if (!_interactiveObject)
			{
				Debug.LogError("Interactive object hasn't InteractiveObject component.");
				_message.text = string.Empty;
				_interactiveObject = null;
				return;
			}

			_distance = hitInfo.distance;
			_message.text = _interactiveObject.GetMessage(_distance);
		}

		private void OnValidate()
		{
			Assert.IsNotNull(_camera, "_camera != null");
			Assert.IsNotNull(_message, "_message != null");
		}
	}
}