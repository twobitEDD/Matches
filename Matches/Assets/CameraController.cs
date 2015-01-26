using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	//Vector3 mouseButton

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if ( Input.GetMouseButton (0) )
		{
			float x = Input.GetAxisRaw("Mouse X");
			float y = Input.GetAxisRaw("Mouse Y");
			if ( Mathf.Abs( x ) > Mathf.Abs(y) )
			{
				y = 0;
			}
			else
			{
				x = 0;
			}

			transform.Rotate( new Vector3 ( 10*y, 10*x, 0));
		}
		else if ( Input.GetMouseButtonUp ( 0 ) )
		{
			Vector3 vec = transform.eulerAngles;
			vec.x = Mathf.Round(vec.x / 45) * 45;
			vec.y = Mathf.Round(vec.y / 45) * 45;
			vec.z = Mathf.Round(vec.z / 45) * 45;
			transform.eulerAngles = vec;
		}
	}
}
