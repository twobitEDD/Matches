using UnityEngine;
using System.Collections;

public class CubeColorRotator : MonoBehaviour 
{
    public Material material;
	public Color setColor = Color.clear;

	// Use this for initialization
	IEnumerator Start () 
    {
		if ( setColor == Color.clear )
		{
	        material = GetComponent<Renderer>().material; // clone the material
	        GetComponent<Renderer>().material = material; // set the clone back!
	        var current = material.color = Colors.Black;
	        // set up a coroutine to modify the color over time!
	        while (true)
	        {
	            float time = Random.Range(1f, 4f);
	            var chance = Random.value;
	            Color target = Colors.Black;
	            // lets flip a coin, heads for bight target color, tails for pastel
	            if (chance >= 0.5f)
	            {
	                target = ColorHelper.RandomBrightColor();
	            }
	            else
	            {
	                target = ColorHelper.RandomPastelColor();
	            }

	            ColorHelper.LerpOverTime(c => material.color = c, current, target, time);

	            yield return new WaitForSeconds(time);
	            current = target;
	        }
		}
		else
		{
			material = GetComponent<Renderer>().material; // clone the material
			GetComponent<Renderer>().material = material; // set the clone back!
			var current = material.color = setColor;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
