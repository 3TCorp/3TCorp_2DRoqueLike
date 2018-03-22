using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets._2D;

[RequireComponent(typeof (PlatformerCharacter2D))]
public class User2DController : MonoBehaviour
{
	private PlatformerCharacter2D m_Character;
	private bool m_Jump;

	public VirtualJoystick joystick;

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
			m_Jump = joystick.GetAxis("Vertical") > 0.5;
		}
	}


	private void FixedUpdate()
	{
		// Read the inputs.
		bool crouch = joystick.GetAxis ("Vertical") < -0.5;
		float h = joystick.GetAxis ("Horizontal");//CrossPlatformInputManager.GetAxis("Horizontal");
		// Pass all parameters to the character control script.
		m_Character.Move(h, crouch, m_Jump);
		m_Jump = false;
	}
}
