using UnityEngine;

public class CaptureShadow : MonoBehaviour
{
	public float distance;
	public Transform lookAtObj;
	public Transform MatchObj;
	public Camera StencilCamera;
	public RenderTexture RandomTexture;
	public RenderTexture TrackedTexture;
	public Shader StencilShader;

	public void MoveToRandomPosition()
	{
		var dir = Random.insideUnitSphere;
		transform.position = lookAtObj.position + dir * distance;
		transform.LookAt(lookAtObj);
		transform.Rotate(new Vector3(0, 0, Random.Range(-180, 180)), Space.Self);
	}
	public void MatchCameraToObj(Transform obj)
	{
		var dir = obj.position - lookAtObj.position;
		dir.Normalize();
		StencilCamera.transform.position = lookAtObj.position + dir * distance;
		StencilCamera.transform.rotation = obj.rotation;
	}

	// Use this for initialization
	void Start ()
	{
		StencilCamera.enabled = false;
		MoveToRandomPosition();
	}

	void CaptureRandomTexture()
	{

		MoveToRandomPosition();
		MatchCameraToObj(this.transform);
		StencilCamera.targetTexture = RandomTexture;
		StencilCamera.RenderWithShader(StencilShader, null);
	}

	void CaptureTrackedTexture()
	{
		MatchCameraToObj(MatchObj);
		StencilCamera.targetTexture = TrackedTexture;
		StencilCamera.RenderWithShader(StencilShader, null);

	}
	// Update is called once per frame
	void Update () {

		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			CaptureRandomTexture();
		}
		if (Input.touchCount > 1 && Input.GetTouch(1).phase == TouchPhase.Began)
		{
			CaptureTrackedTexture();
		}
	}
}
