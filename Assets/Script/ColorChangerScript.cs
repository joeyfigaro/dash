using UnityEngine;
using System.Collections;

public class ColorChangerScript : MonoBehaviour {
	public Sprite green;
	public Sprite red;
	public Sprite blue;
	
	public int color = 0;
	private int colorOld = -1;

	public void Update() {
		if(color != colorOld) {
			SpriteRenderer sprRenderer = (SpriteRenderer) renderer;
			switch(color) {
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
			colorOld = color;
		}
	}
}