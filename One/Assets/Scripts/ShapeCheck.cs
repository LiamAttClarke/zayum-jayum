using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeCheck : MonoBehaviour {

	public RenderTexture shapeTexture;
	private Texture2D initialTexture, secondTexture;
	// Use this for initialization
	void Start () {
		

	}

	Texture2D copyTex(Texture2D target = null)
	{
		RenderTexture.active = shapeTexture;
		target.ReadPixels(new Rect(0, 0, shapeTexture.width, shapeTexture.height), 0, 0);
		target.Apply();
		return target;
	}

	void CalculateIntersection(Texture2D shape, Texture2D stencil)
	{
		int intersected = 0, shapeMiss = 0, stencilMiss = 0;
		float thresh = 0.5f;
		for(int x = 0; x < shape.width;  x++)
		{
			for(int y = 0; y < shape.width; y++)
			{
				Color shapeCol = shape.GetPixel(x, y);
				Color stencilCol = stencil.GetPixel(x, y);
				bool shapeOn = shapeCol.r > thresh && shapeCol.g > thresh && shapeCol.b > thresh;
				bool stencilOn = stencilCol.r > thresh && stencilCol.g > thresh && stencilCol.b > thresh;
				if (shapeOn)
				{
					if (stencilOn)
					{
						intersected++;
					}
					else
					{
						shapeMiss++;
					}
				}
				else if (stencilOn)
				{
					stencilMiss++;
				}
			}
		}
		float shapeIntersecting = ((float)intersected / (intersected + shapeMiss)) * 100f;
		float stencilIntersecting = ((float)intersected / (intersected + stencilMiss)) * 100f;
		print("Intersected: " + intersected + " ShapeMiss: " + shapeMiss + " StencilMiss: " + stencilMiss);
		print("ShapeIntersecting: " + shapeIntersecting + "%  StencilIntersecting: " + stencilIntersecting + "%");
	}
	

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			print("SPACE");
			initialTexture = new Texture2D(shapeTexture.width, shapeTexture.height);
			secondTexture = new Texture2D(shapeTexture.width, shapeTexture.height);
			copyTex(initialTexture);
		}
		if (Input.GetMouseButtonDown(0))
		{
			print("Mousedown");
			if (initialTexture != null)
			{
				copyTex(secondTexture);
				CalculateIntersection(secondTexture, initialTexture);
			}
		}
	}
}
