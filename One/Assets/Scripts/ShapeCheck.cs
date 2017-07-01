using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeCheck : MonoBehaviour {
	public CaptureShadow captureShadow;
	Texture2D _randomTexture, _trackedTexture;
	// Use this for initialization
	void Start () {
		

	}

	Texture2D copyTex(RenderTexture source, Texture2D target = null)
	{
		RenderTexture.active = source;
		target.ReadPixels(new Rect(0, 0, source.width, source.height), 0, 0);
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
				bool shapeOn = shapeCol.r < thresh && shapeCol.g < thresh && shapeCol.b < thresh;
				bool stencilOn = stencilCol.r < thresh && stencilCol.g < thresh && stencilCol.b < thresh;
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
		float score = 100f - (100f - shapeIntersecting) / 2f - (100f - stencilIntersecting) / 2f;
		print("Intersected: " + intersected + " ShapeMiss: " + shapeMiss + " StencilMiss: " + stencilMiss);
		print("ShapeIntersecting: " + shapeIntersecting + "%  StencilIntersecting: " + stencilIntersecting + "%");
		print("SCORE: " + score + "%");
	}
	

	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			captureShadow.CaptureRandomTexture();
			_randomTexture = new Texture2D(captureShadow.RandomTexture.width, captureShadow.RandomTexture.height);
			_trackedTexture = new Texture2D(captureShadow.TrackedTexture.width, captureShadow.TrackedTexture.height);
			copyTex(captureShadow.RandomTexture, _randomTexture);
		}
		if (Input.touchCount > 1 && Input.GetTouch(1).phase == TouchPhase.Began)
		{
			captureShadow.CaptureTrackedTexture();
			if (_randomTexture != null)
			{
				copyTex(captureShadow.TrackedTexture, _trackedTexture);
				CalculateIntersection(_trackedTexture, _randomTexture);
			}
		}
		
	}
}
