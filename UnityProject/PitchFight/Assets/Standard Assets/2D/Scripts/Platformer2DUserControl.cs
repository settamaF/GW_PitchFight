using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
		private bool _GDEvent;
		private PlatformerCharacter2D m_Character;
		private bool m_Jump;
		private int _GDEventValue = 1;
		public int playerNumber;
		public bool GDEvent
		{
			get
			{
				return _GDEvent; 
			}
			set
			{
				this._GDEvent = value;
				this.SwitchControls(value);
			}
		}


		public void SwitchControls(bool b)
		{
			if (b)
				this._GDEventValue = -1;
			else
				this._GDEventValue = 1;
		}

        private void Awake()
        {
            m_Character = this.GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("J" + playerNumber.ToString() + "Jump");
            }
			if (Input.GetKeyDown(KeyCode.Z))
				this.GDEvent = !this.GDEvent;
        }


        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
			float h = Input.GetAxis("J" + playerNumber.ToString() + "Horizontal") * this._GDEventValue;
            // Pass all parameters to the character control script.
            m_Character.Move(h, crouch, m_Jump);
            m_Jump = false;
        }
    }
}
