using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScreenShot : MonoBehaviour 
{
	private bool isProcessing = false;
	public string ScreenshotName = "ExxarsScreenshot{0}.png";
	Camera mainCam;
	RenderTexture renderTex;

	public void TakeaScreenShot()
	{
		#if UNITY_IOS
		if(!isProcessing)
			StartCoroutine(TakeaPicture());
		#else
		Debug.Log("No Camera set up for this platform.");
		#endif	
	}

	public void ShowaScreenShot()
	{
		#if UNITY_IOS
		if(!isProcessing)
			PickImage();
		#else
		Debug.Log("No Imagem Found");
		#endif	
	}

	private IEnumerator TakeaPicture ()
	{
		yield return new WaitForEndOfFrame();
		isProcessing = true;

		mainCam = Camera.main.GetComponent<Camera> ();
		renderTex = new RenderTexture (Screen.width, Screen.height, 24);
		mainCam.targetTexture = renderTex;
		RenderTexture.active = renderTex;
		mainCam.Render ();
		Texture2D ss = new Texture2D( Screen.width, Screen.height, TextureFormat.RGB24, false );
		ss.ReadPixels( new Rect( 0, 0, Screen.width, Screen.height ), 0, 0 );
		ss.Apply();
		RenderTexture.active = null;
		mainCam.targetTexture = null;

		// Save the screenshot to Gallery/Photos
		Debug.Log( "Permission result: " + NativeGallery.SaveImageToGallery( ss, "Exxars", ScreenshotName ) );

		isProcessing = false;
	}
		
	private void PickImage()
	{
		
		isProcessing = true;

		NativeGallery.Permission permission = NativeGallery.GetImageFromGallery( ( path ) =>
			{
				Debug.Log( "Image path: " + path );
				if( path != null )
				{
					// Create Texture from selected image
					Texture2D texture = new Texture2D( 2, 2 );
					texture.LoadImage( File.ReadAllBytes( path ) );

					// Assign texture to a temporary cube and destroy it after 5 seconds
					GameObject cube = GameObject.CreatePrimitive( PrimitiveType.Cube );
					cube.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 10f;
					cube.transform.forward = -Camera.main.transform.forward;
					cube.GetComponent<Renderer>().material.mainTexture = texture;
					Destroy( cube, 5f );
					// If a procedural texture is not destroyed manually, 
					// it will only be freed after a scene change
					Destroy( texture, 5f );
				}
			}, "Select a PNG image", "image/png" );

		Debug.Log( "Permission result: " + permission );

		isProcessing = false;
	}
}



