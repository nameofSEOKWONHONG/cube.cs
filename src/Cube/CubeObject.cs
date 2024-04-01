using System; using System.Collections.Generic; using System.Linq; using System.Text; using System.Threading.Tasks;  namespace Cube {  	public class CubeObject 	{
		private float A, B, C;

		public int Width { get; init; }

		public float IncrementSpeed { get; init; }

		public CubeObject(int width, float incrementSpeed)
		{
			this.Width = width;
			this.IncrementSpeed = incrementSpeed;
		}

		public void SetRotate(float a, float b, float c)
		{
			this.A += a;
			this.B += b;
			this.C += c;
		}

		public float CalculateX(float i, float j, float k)
		{
			return j * (float)Math.Sin(A) * (float)Math.Sin(B) * (float)Math.Cos(C) - k * (float)Math.Cos(A) * (float)Math.Sin(B) * (float)Math.Cos(C) +
				   j * (float)Math.Cos(A) * (float)Math.Sin(C) + k * (float)Math.Sin(A) * (float)Math.Sin(C) + i * (float)Math.Cos(B) * (float)Math.Cos(C);
		}

		public float CalculateY(float i, float j, float k)
		{
			return j * (float)Math.Cos(A) * (float)Math.Cos(C) + k * (float)Math.Sin(A) * (float)Math.Cos(C) -
				   j * (float)Math.Sin(A) * (float)Math.Sin(B) * (float)Math.Sin(C) + k * (float)Math.Cos(A) * (float)Math.Sin(B) * (float)Math.Sin(C) -
				   i * (float)Math.Cos(B) * (float)Math.Sin(C);
		}

		public float CalculateZ(float i, float j, float k)
		{
			return k * (float)Math.Cos(A) * (float)Math.Cos(B) - j * (float)Math.Sin(A) * (float)Math.Cos(B) + i * (float)Math.Sin(B);
		}
	} } 