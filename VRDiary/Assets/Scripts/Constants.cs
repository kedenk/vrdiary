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

    public static class EnvCameraSettings
    {
        public static Dictionary<string, CameraSetting>  envs = new Dictionary<string, CameraSetting>
            {
                { "std", new CameraSetting(new Vector3(0F, 0.696F, -10F), new Vector3(1, 1, 1), new Vector3(0, 0, 0)) },
                { "model", new CameraSetting(new Vector3(0F, 0.696F, -10F), new Vector3(1, 1, 1), new Vector3(0, 0, 0)) },
                { "LivingRoom", new CameraSetting(new Vector3(-1.086F, 0.644F, -11.851F), new Vector3(1, 1, 1), new Vector3(0, 0, 0)) }
            };

        public static CameraSetting getEnvCameraSetting(string envKey)
        {
            return envs[envKey]; 
        }
    }

    public class CameraSetting
    {
        public Vector3 pos;
        public Vector3 scale;
        public Vector3 rotation;

        public CameraSetting(Vector3 pos, Vector3 scale, Vector3 rotation)
        {
            this.pos = pos;
            this.scale = scale;
            this.rotation = rotation; 
        }
    }
}