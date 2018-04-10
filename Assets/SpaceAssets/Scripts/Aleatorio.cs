using UnityEngine;
using System.Collections;

public class Aleatorio : MonoBehaviour {
	SpriteRenderer sp;
	public Color[] colores;
	public Sprite[] sprites;
	public int probabilidadInvisible = 30;
	public SpriteRenderer[] spriteSecundariosAplicar;
	// Use this for initialization
	void Start () {
		sp = GetComponent<SpriteRenderer> ();
		generar ();
	}

	public void generar(){
		if (probabilidadInvisible > 0 && Random.Range (0, 100) < probabilidadInvisible) {
			sp.sprite = null;
			return;
		}
		int colorSel = Random.Range (0, colores.Length);
		if (colores.Length > 0) {
			sp.color = colores[colorSel];
			foreach(SpriteRenderer s in spriteSecundariosAplicar)
				s.color = colores[colorSel];
		}
		if(sprites.Length > 0){
			sp.sprite = sprites[Random.Range(0, sprites.Length)];
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
