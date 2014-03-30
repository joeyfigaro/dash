using UnityEngine;
using System.Collections;

public class ColorChangerScript : MonoBehaviour {
	public Sprite green;
	public Sprite red;
	public Sprite blue;
	
	public int colorNew = 0;
	private int colorOld = -1;
	
	public void colorMaintenance() {
		if(colorNew != colorOld) {
			SpriteRenderer sprRenderer = (SpriteRenderer) renderer;
			switch(colorNew) {
			case 0:
				sprRenderer.sprite = red;
				break;
			case 1:
				sprRenderer.sprite = green;
				break;
			case 2:
				sprRenderer.sprite = blue;
				break;
			}
			colorOld = colorNew;
		}
		
		if (renderer.IsVisibleFrom(Camera.main) == false)
		{
			Destroy(gameObject);
		}
	}
}