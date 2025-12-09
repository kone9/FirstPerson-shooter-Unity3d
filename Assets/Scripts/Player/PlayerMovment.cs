using Assets.Scripts.Manager;
using UnityEngine;

namespace Assets.Scripts
{
	public class PlayerMovment : MonoBehaviour
	{
		public float Speed = 6f;
		private Vector3 _movement;
		private Animator _anim;
		private Rigidbody _playerRigidbody;

		public VirtualJoystick JoystickMove;
		public VirtualJoystick JoysticRotate;

		// Rotación tipo FPS
		public bool EnableMouseRotation = true;
		public float MouseSensitivity = 5f;

		private void Awake()
		{
			_anim = GetComponent<Animator>();
			_playerRigidbody = GetComponent<Rigidbody>();
		}

		private void Start()
		{
			// Captura el mouse
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}

		private void FixedUpdate()
		{
			// Movimiento con teclado
			float h = Input.GetAxis("Horizontal");
			float v = Input.GetAxis("Vertical");
			Move(h, v);
			Animating(h, v);

			// Movimiento con joystick
			float hJoy = JoystickMove.Horizontal();
			float vJoy = JoystickMove.Vertical();
			Move(hJoy, vJoy);
			Animating(hJoy, vJoy);

			// Rotación joystick virtual
			JoysticRotate.RotateTarget(transform);

			// Rotación FPS con mouse
			if (EnableMouseRotation)
				MouseTurning();
		}

		private void Move(float h, float v)
		{
			// Dirección local del movimiento
			Vector3 direction =
				transform.forward * v +
				transform.right * h;

			_movement = direction.normalized * Speed * Time.deltaTime;

			_playerRigidbody.MovePosition(transform.position + _movement);
		}

		private void MouseTurning()
		{
			// Obtiene movimiento del mouse
			float mouseX = Input.GetAxis("Mouse X");

			// Calcula rotación
			Vector3 rotation = new Vector3(0f, mouseX * MouseSensitivity, 0f);

			// Aplica rotación suave
			_playerRigidbody.MoveRotation(
				_playerRigidbody.rotation * Quaternion.Euler(rotation)
			);
		}

		private void Animating(float h, float v)
		{
			_anim.SetBool("IsWalking", h != 0 || v != 0);
		}
	}
}
