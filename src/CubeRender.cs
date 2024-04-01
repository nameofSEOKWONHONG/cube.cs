using eXtensionSharp;

namespace Cube
{
	internal class CubeRender
	{
        private CubeObject _cubeObject;
		        
        private float _width = 160, _height = 44;
        private float[] _zBuffer = new float[160 * 44];
        private char[] _buffer = new char[160 * 44];

        private char _backgroundASCIICode = '.';
		private float _horizontalOffset;
		
		float _x, _y, _z, _ooz;
		int _xp, _yp;
		float _k1 = 40;
		float _distanceFromCam = 100;
		
		int _idx;

		public CubeRender(CubeObject cube)
        {
            this._cubeObject = cube;
        }

		private void CalculateForSurface(float cubeX, float cubeY, float cubeZ, char ch)
		{
			_x = this._cubeObject.CalculateX(cubeX, cubeY, cubeZ);
			_y = this._cubeObject.CalculateY(cubeX, cubeY, cubeZ);
			_z = this._cubeObject.CalculateZ(cubeX, cubeY, cubeZ) + _distanceFromCam;

			_ooz = 1 / _z;

			_xp = (_width / 2 + _horizontalOffset + _k1 * _ooz * _x * 2).xValue<int>();
			_yp = (_height / 2 + _k1 * _ooz * _y).xValue<int>();

			_idx = (_xp + _yp * _width).xValue<int>();
			if (_idx >= 0 && _idx < _width * _height)
			{
				if (_ooz > _zBuffer[_idx])
				{
					_zBuffer[_idx] = _ooz;
					_buffer [_idx] = ch;
				}
			}
		}

		public void Render()
        {
			Console.Clear();
			while (true)
            {
				Array.Fill(_buffer, (char)_backgroundASCIICode);
				Array.Fill(_zBuffer, 0);

				_horizontalOffset = -2 * this._cubeObject.Width;
				for (float cubeX = -this._cubeObject.Width; cubeX < this._cubeObject.Width; cubeX += this._cubeObject.IncrementSpeed)
                {
                    for(float cubeY = -this._cubeObject.Width; cubeY < this._cubeObject.Width; cubeY += this._cubeObject.IncrementSpeed)
                    {
                        CalculateForSurface(cubeX, cubeY, -this._cubeObject.Width, '@');
                        CalculateForSurface(this._cubeObject.Width, cubeY, cubeX, '$');
                        CalculateForSurface(-this._cubeObject.Width, cubeY, -cubeX, '~');
                        CalculateForSurface(-cubeX, cubeY, this._cubeObject.Width, '#');
                        CalculateForSurface(cubeX, -this._cubeObject.Width, -cubeY, ';');
                        CalculateForSurface(cubeX, this._cubeObject.Width, cubeY, '+');
                    }
				}

				Console.SetCursorPosition(0, 0);
				for (int k = 0; k < _width * _height; k++)
				{
					Console.Write(k % _width == 0 ? '\n' : _buffer[k]);
				}

				this._cubeObject.SetRotate(0.05f, 0.05f, 0.01f);

				Thread.Sleep(30);
			}
        }
	}
}
