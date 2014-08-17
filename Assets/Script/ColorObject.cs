using UnityEngine;
using System.Collections;

public class ColorObject : MonoBehaviour {
	
	// TODO: Having a public boolean for this is probably lazy
	protected bool avoidRegistration = false;

	private Color color = new Color();

	public void setColor(int colorInt) {
		color = ColorDefinitions.colors[colorInt];
	}

	public void setColor(Color colorV) {
		color = colorV;
	}

	public Color getColor() {
		return color;
	}

	protected void changeRenderedColor() {
		gameObject.renderer.material.SetColor("_SunTint", color);
	}

	public void registerWithTintSource() {
		BeatsEngine.Instance.registerTintable(this);
	}

	void OnDestroy() {
		BeatsEngine.Instance.unregisterTintable(this);
	}

	protected bool isOffscreen() {
		if (!avoidRegistration && (transform.position.x < (Camera.main.transform.position.x - 50)) && (renderer.IsVisibleFrom(Camera.main) == false))
			return true;
		return false;
	}

	protected void poolIfOffscreen() {
		if (isOffscreen())
		{
			ObjectPool.instance.PoolObject(gameObject);
		}
	}

	protected void destroyIfOffscreen() {
		if (isOffscreen())
		{
			Destroy(gameObject);
		}
	}
}
