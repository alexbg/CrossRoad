using UnityEngine;
using System.Collections;

namespace CrossRoad.Character{

	[AddComponentMenu("Camera-Control/Mouse Look")]
	public class MouseLookYoystick : MonoBehaviour {

		public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
		public RotationAxes axes = RotationAxes.MouseXAndY;
		public float sensitivityX = 15F;
		public float sensitivityY = 15F;
		
		public float minimumX = -360F;
		public float maximumX = 360F;
		
		public float minimumY = -60F;
		public float maximumY = 60F;

		public int joystick;

		float rotationY = 0F;
		
		void Update ()
		{
			if (axes == RotationAxes.MouseXAndY)
			{
				float rotationX = transform.localEulerAngles.y + Input.GetAxis("MouseXPlayer"+this.joystick) * sensitivityX;
				
				rotationY += Input.GetAxis("MouseYPlayer"+this.joystick) * sensitivityY;
				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
				
				transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
			}
			else if (axes == RotationAxes.MouseX)
			{
				transform.Rotate(0, Input.GetAxis("MouseXPlayer"+this.joystick) * sensitivityX, 0);
			}
			else
			{
				rotationY += Input.GetAxis("MouseYPlayer"+this.joystick) * sensitivityY;
				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
				
				transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
			}
		}
		
		void Start ()
		{
			// Make the rigid body not change rotation
			if (GetComponent<Rigidbody>())
				GetComponent<Rigidbody>().freezeRotation = true;
		}
	}
}
