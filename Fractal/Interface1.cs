using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Fractal
{
    interface IFractalGenerator
    {
        IBitmap Generate(Point start, Point end);
    }

    class Mandelbrot : IFractalGenerator
    {
        const int MAX_ITER = 100;
        private Complex MandelbrotFunction(Complex z, Complex z0) => Complex.Pow(z, 2) + z0;

        private int InMandelbrot(Complex z, Complex z0)
        {
            var iter = 0;
            var processing = z;
            while (iter < MAX_ITER)
            {
                var res = MandelbrotFunction(processing, z0);
                if (Complex.Abs(res) >= 2.0)
                {
                    return iter;
                }

                processing = res;
            }

            return iter;
        }

        private int InMandelbrot(Complex z0)
        {
            return InMandelbrot(0, z0);
        }

        public IBitmap Generate(PointF start, PointF end, double precision = 0.01)
        {
            var b = new System.Drawing.Bitmap(500, 500);
            
            for (double x = start.X; x <= end.X; x += precision)
            {
                for (double y = start.Y; y <= end.Y; y += precision)
                {
                    var iters = InMandelbrot(new Complex(x, y));
                    var color = iters switch
                    {
                        80 => Color.White,
                        _ => Color.Black
                    };


                    b.SetPixel(x, y, color)
                }
            }

            using (MemoryStream memory = new MemoryStream())
            {
                b.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                //AvIrBitmap is our new Avalonia compatible image. You can pass this to your view
                Avalonia.Media.Imaging.Bitmap AvIrBitmap = new Avalonia.Media.Imaging.Bitmap(memory);
            }

            return new Avalonia.Media.Imaging.Bitmap(b!);
        }
    }
}
