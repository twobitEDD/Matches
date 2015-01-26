using UnityEngine;
using System.Collections;

public class SelectionController : MonoBehaviour {

	public GameObject spawnObject;

	Transform spawnHost; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(0)) {
			RaycastHit hit = new RaycastHit();
			Camera cam = Camera.main;
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);  
			if ( Physics.Raycast ( ray, out hit ) ) 
			{
				if ( hit.collider.CompareTag("CubeSide") ) 
				{
					Debug.Log ("CubeSide Found");
					CubeColorRotator cc = hit.collider.GetComponent<CubeColorRotator>();
					Debug.Log (" Color: " + cc.material.color );
					Debug.Log ("Cam Color: " + cam.backgroundColor );
					bool matchFound = Color.Equals ( cc.material.color, cam.backgroundColor );
					if (matchFound )
					{
					spawnHost = hit.collider.transform;
						Vector3 offset = spawnHost.position + new Vector3( 2.0F, 0, 0);
					AddBlock( offset );
					//mousePos = Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
					//hit.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
					}
				}
			}
		}
	}

	void AddBlock (Vector3 offset )
	{
		Instantiate(spawnObject, offset, Quaternion.identity);
	}
}
