using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets._2D;

[RequireComponent(typeof (PlatformerCharacter2D))]
public class User2DController : MonoBehaviour
{
	private PlatformerCharacter2D m_Character;
	private bool m_Jump;
	private bool _inputButtonJump;
	private bool _inputButtonCrouch;


	public VirtualJoystick joystick;

	public void ButtonCrouchDown()
	{
		_inputButtonCrouch = true;
	}

	public void ButtonCrouchUp()
	{
		_inputButtonCrouch = false;
	}


	public void ButtonJumpDown()
	{
		_inputButtonJump = true;
	}

	public void ButtonJumpUp()
	{
		_inputButtonJump = false;
	}

	private void Awake()
	{
		m_Character = GetComponent<PlatformerCharacter2D>();
	}


	private void Update()
	{
		if (!m_Jump)
		{
			// Read the jump input in Update so button presses aren't missed.
			//m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
			m_Jump = _inputButtonJump;
		}
	}


	private void FixedUpdate()
	{
		// Read the inputs.
		bool crouch = _inputButtonCrouch;
		float h = joystick.GetAxis ("Horizontal");//CrossPlatformInputManager.GetAxis("Horizontal");
		// Pass all parameters to the character control script.
		m_Character.Move(h, crouch, m_Jump);
		m_Jump = false;
	}
}
