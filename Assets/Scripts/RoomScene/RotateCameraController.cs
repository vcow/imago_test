using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

namespace RoomScene
{
	[DisallowMultipleComponent, RequireComponent(typeof(PlayerInput))]
	public class RotateCameraController : MonoBehaviour
	{
		[SerializeField] private Transform _cameraAnchor;
		[SerializeField] private Vector2 _verticalAngleLimits;
		[SerializeField] private Vector2 _horizontalAngleLimits;
		[SerializeField] private float _sensitivityVertical;
		[SerializeField] private float _sensitivityHorizontal;

		public void OnLook(InputAction.CallbackContext context)
		{
			var offset = context.ReadValue<Vector2>();
			var anchorRotation = _cameraAnchor.localRotation.eulerAngles;
			var verticalOffset = _sensitivityVertical * offset.y;
			var horizontalOffset = _sensitivityHorizontal * offset.x;
			var newRotation = new Vector3(
				Mathf.Clamp((anchorRotation.x > 180f ? anchorRotation.x - 360f : anchorRotation.x) - verticalOffset,
					_verticalAngleLimits.x, _verticalAngleLimits.y),
				Mathf.Clamp((anchorRotation.y > 180f ? anchorRotation.y - 360f : anchorRotation.y) + horizontalOffset,
					_horizontalAngleLimits.x, _horizontalAngleLimits.y),
				0f);
			Debug.Log($"{anchorRotation} -> {newRotation}");
			_cameraAnchor.localRotation = Quaternion.Euler(newRotation);
		}

		private void OnValidate()
		{
			Assert.IsNotNull(_cameraAnchor);
			Assert.IsTrue(_verticalAngleLimits.x <= _verticalAngleLimits.y, "Max value less than Min.");
			Assert.IsTrue(_horizontalAngleLimits.x <= _horizontalAngleLimits.y, "Max value less than Min.");
			Assert.IsTrue(_sensitivityVertical >= 0 && _sensitivityHorizontal >= 0, "Negative sensitivity.");
		}
	}
}