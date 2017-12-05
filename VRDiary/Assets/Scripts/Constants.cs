using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
	public static class Colors
	{
		public static readonly Color yellow = new Color (182/255.0F, 142/255.0F, 28/255.0F);
		public static readonly Color green = new Color (105/255.0F, 138/255.0F, 8/255.0F);
		public static readonly Color red = new Color (171/255.0F, 34/255.0F, 16/255.0F);
		public static readonly Color blue = new Color (27/255.0F, 67/255.0F, 128/255.0F);
	}

	public static class Passcodes
	{
		public static readonly List<ButtonType> userA = new List<ButtonType>(new ButtonType[] {ButtonType.CIRCLE, ButtonType.CIRCLE, ButtonType.CIRCLE, ButtonType.CIRCLE});
		public static readonly List<ButtonType> userB = new List<ButtonType>(new ButtonType[] {ButtonType.CIRCLE, ButtonType.CIRCLE, ButtonType.CIRCLE, ButtonType.X});
	}
}