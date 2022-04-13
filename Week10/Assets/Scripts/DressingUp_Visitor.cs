using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DressingUp_Visitor : MonoBehaviour
{

	[SerializeField]
	protected SpriteRenderer spriteRender_Body;
	[SerializeField]
	protected SpriteRenderer spriteRender_Hair;
	[SerializeField]
	protected SpriteRenderer spriteRender_Head;
	[SerializeField]
	protected SpriteRenderer spriteRender_Face;
	[SerializeField]
	protected SpriteRenderer spriteRender_LegsL;
	[SerializeField]
	protected SpriteRenderer spriteRender_LegsR;
	[SerializeField]
	protected SpriteRenderer spriteRender_HandL;
	[SerializeField]
	protected SpriteRenderer spriteRender_HandR;
	[SerializeField]
	protected SpriteRenderer spriteRender_Camera;
	//[SerializeField]
	//   Vector2 hueRange_Body = new Vector2(0, 359.99f);
	//   [SerializeField]
	//   Vector2 hueRange_Hair = new Vector2(-33.33f, 33.33f);
	[SerializeField]
	protected float outlineSize = 20;

	[SerializeField]
	SpriteRenderer[] spritesRender_OutlinePlus;

	[SerializeField]
	bool changedBody = true;


	//For cosplayer and special characters, change character sprites to the target combo
	public void ClothesSetUp(Sprite sprite_Body, Sprite sprite_Hair, Sprite sprite_Head, Sprite sprite_Face, Sprite sprite_Leg_L, Sprite sprite_Leg_R, Sprite sprite_Hand_L, Sprite sprite_Hand_R)
	{
		spriteRender_Body.sprite = sprite_Body;
		spriteRender_Hair.sprite = sprite_Hair;
		spriteRender_Head.sprite = sprite_Head;
		spriteRender_Face.sprite = sprite_Face;
		spriteRender_LegsL.sprite = sprite_Leg_L;
		spriteRender_LegsR.sprite = sprite_Leg_R;
		spriteRender_HandL.sprite = sprite_Hand_L;
		spriteRender_HandR.sprite = sprite_Hand_R;
	}


	//para in standard visitor means best tag interest
	//public virtual void DressingUp(int para = -1)
	//   {
	//	//Setup outline size
	//	spriteRender_Head.material.SetFloat("_OutlineSize", outlineSize);
	//	spriteRender_Hair.material.SetFloat("_OutlineSize", outlineSize);
	//	spriteRender_Body.material.SetFloat("_OutlineSize", outlineSize);
	//	spriteRender_LegsL.material.SetFloat("_OutlineSize", outlineSize);
	//	spriteRender_LegsR.material.SetFloat("_OutlineSize", outlineSize);
	//	spriteRender_HandL.material.SetFloat("_OutlineSize", outlineSize);
	//	spriteRender_HandR.material.SetFloat("_OutlineSize", outlineSize);

	//	float bodyHueChange = Random.Range(hueRange_Body.x, hueRange_Body.y);
	//	if (changedBody)
	//	{
	//		//sprites_Body = ClothesManager.Sp_Visitor_Body;
	//		//Change Color
	//		spriteRender_Body.material.SetFloat("_HueShift", bodyHueChange);
	//		spriteRender_Body.material.SetFloat("_Sat", Random.Range(0, 1.001f));
	//	}
	//	else
	//	{
	//		//sprites_Body = ClothesManager.Sp_Visitor_Body_Interest[para];
	//		spriteRender_Body.material.SetFloat("_Sat", Random.Range(0.5f, 1.001f));
	//	}

	//	int i;
	//       i = Random.Range(0, sprites_Body.Count);
	//       spriteRender_Body.sprite = sprites_Body[i];

	//       i = Random.Range(0, sprites_Body.Count);
	//       spriteRender_LegsL.sprite = sprites_LegsL[i];
	//       spriteRender_LegsR.sprite = sprites_LegsR[i];

	//	if (spriteRender_Camera != null)
	//	{
	//		i = Random.Range(0, sprites_Camera.Count);
	//		spriteRender_Camera.sprite = sprites_Camera[i];
	//		//Random camera color
	//		if (i != 0 && Random.value < 0.5f)
	//		{
	//			spriteRender_Camera.material.SetColor("_Color", new Color(Random.value, Random.value, Random.value));
	//		}
	//	}



	//       i = Random.Range(0, sprites_Hair.Count);
	//       spriteRender_Hair.sprite = sprites_Hair[i];
	//       i = Random.Range(0, sprites_Face.Count);
	//	spriteRender_Face.sprite = sprites_Face[i];

	//	i = Random.Range(0, sprites_HandL.Count);
	//	spriteRender_HandL.sprite = sprites_HandL[i];
	//       spriteRender_HandR.sprite = sprites_HandR[i];


	//       if (Random.value < 0.1f)
	//       {
	//           //Rare hair color, directly using body hue
	//           spriteRender_Hair.material.SetFloat("_HueShift", bodyHueChange);
	//       }
	//       else
	//       {
	//           spriteRender_Hair.material.SetFloat("_HueShift", Random.Range(hueRange_Hair.x, hueRange_Hair.y));
	//       }
	//       spriteRender_Hair.material.SetFloat("_Sat", Random.Range(0, 1.001f));

	//       if (Random.value < 0.3f)
	//       {
	//           //Rare Leg color
	//           bodyHueChange = Random.Range(hueRange_Body.x, hueRange_Body.y);
	//           spriteRender_LegsL.material.SetFloat("_HueShift", bodyHueChange);
	//           spriteRender_LegsR.material.SetFloat("_HueShift", bodyHueChange);
	//           spriteRender_LegsL.material.SetFloat("_Sat", Random.Range(0, 1.001f));
	//           spriteRender_LegsR.material.SetFloat("_Sat", Random.Range(0, 1.001f));
	//       }

	//       spriteRender_Head.material.SetFloat("_Sat", 2.8f);
	//       if (Random.value < 0.1f)
	//       {
	//           //Black People
	//           float skinColor = Random.Range(0.5f, 1.051f);
	//           spriteRender_Head.material.SetFloat("_Val", skinColor);
	//           spriteRender_LegsL.material.SetFloat("_Val", skinColor);
	//           spriteRender_LegsR.material.SetFloat("_Val", skinColor);
	//           spriteRender_HandL.material.SetFloat("_Val", skinColor);
	//           spriteRender_HandR.material.SetFloat("_Val", skinColor);
	//       }
	//       //spriteRender_LegsL.material.SetFloat("_Sat", 2.8f);
	//       //spriteRender_LegsR.material.SetFloat("_Sat", 2.8f);
	//       spriteRender_HandL.material.SetFloat("_Sat", 2.8f);
	//       spriteRender_HandR.material.SetFloat("_Sat", 2.8f);

	//	SaveOriginalSprites();
	//   }

	////Turn on/off outline display
	//public void DisplayOutline(bool OnOrOff)
	//{
	//	float value = 0;
	//	if (OnOrOff)
	//	{
	//		value = 1;
	//	}
	//	else
	//	{
	//		value = 0;
	//	}
	//	spriteRender_Head.material.SetFloat("_Outline", value);
	//	spriteRender_Hair.material.SetFloat("_Outline", value);
	//	spriteRender_Body.material.SetFloat("_Outline", value);
	//	spriteRender_LegsL.material.SetFloat("_Outline", value);
	//	spriteRender_LegsR.material.SetFloat("_Outline", value);
	//	spriteRender_HandL.material.SetFloat("_Outline", value);
	//	spriteRender_HandR.material.SetFloat("_Outline", value);

	//	foreach(SpriteRenderer s in spritesRender_OutlinePlus)
	//	{
	//		s.material.SetFloat("_Outline", value);
	//	}
	//}

	//Changing saturation, e.g. Zombies
	//public void SatChange(float sBody, float sHair, float sHead, float sFace, float sLeg_L, float sLeg_R, float sHand_L, float sHand_R)
	//{
	//	spriteRender_Body.material.SetFloat("_Sat", sBody);
	//	spriteRender_Hair.material.SetFloat("_Sat", sHair);
	//	spriteRender_Head.material.SetFloat("_Sat", sHead);
	//	spriteRender_Face.material.SetFloat("_Sat", sFace);
	//	spriteRender_LegsL.material.SetFloat("_Sat", sLeg_L);
	//	spriteRender_LegsR.material.SetFloat("_Sat", sLeg_R);
	//	spriteRender_HandL.material.SetFloat("_Sat", sHand_L);
	//	spriteRender_HandR.material.SetFloat("_Sat", sHand_R);
	//}

	//public void ResetClothesProperties()
	//{
	//	spriteRender_Body.material.SetFloat("_Sat", 1);
	//	spriteRender_Hair.material.SetFloat("_Sat", 1);
	//	spriteRender_Head.material.SetFloat("_Sat", 1);
	//	spriteRender_Face.material.SetFloat("_Sat", 1);
	//	spriteRender_LegsL.material.SetFloat("_Sat", 1);
	//	spriteRender_LegsR.material.SetFloat("_Sat", 1);
	//	spriteRender_HandL.material.SetFloat("_Sat", 1);
	//	spriteRender_HandR.material.SetFloat("_Sat", 1);

	//	spriteRender_Body.material.SetFloat("_HueShift", 0);
	//	spriteRender_Hair.material.SetFloat("_HueShift", 0);
	//	spriteRender_Head.material.SetFloat("_HueShift", 0);
	//	spriteRender_Face.material.SetFloat("_HueShift", 0);
	//	spriteRender_LegsL.material.SetFloat("_HueShift", 0);
	//	spriteRender_LegsR.material.SetFloat("_HueShift", 0);
	//	spriteRender_HandL.material.SetFloat("_HueShift", 0);
	//	spriteRender_HandR.material.SetFloat("_HueShift", 0);

	//}

	//public Sprite GetBody()
	//{
	//	return spriteRender_Body.sprite;
	//}

	//public Sprite GetHair()
	//{
	//	return spriteRender_Hair.sprite;
	//}
}
