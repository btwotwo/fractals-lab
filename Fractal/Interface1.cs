using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Fractal
{
    struct Offset
    {
        public double X;
        public double Y;

        public Offset(double x, double y)
        {
            X = x;
            Y = y;
        }
    }

    interface IFractalGenerator
    {
        IBitmap Generate(PointF start, PointF end, Offset offset, double zoom);
    }

    class Mandelbrot : IFractalGenerator
    {
        const int WIDTH = 800;
        const int HEIGHT = 300;
        const int MAX_ITER = 50;
        private Complex MandelbrotFunction(Complex z, Complex z0) => Complex.Pow(z, 2) + z0;

        private int InMandelbrot(Complex z, Complex z0)
        {
            var iter = 0;
            while (Complex.Abs(z) <= 2 && iter < MAX_ITER)
            {
                z = MandelbrotFunction(z, z0);
                iter++;
            }

            return iter;
        }

        private int InMandelbrot(Complex z0)
        {
            return InMandelbrot(0, z0);
        }

        public IBitmap Generate(PointF start, PointF end, Offset offset, double zoom)
        {
            var b = new System.Drawing.Bitmap(WIDTH, HEIGHT);
            if (zoom <= 0)
            {
                zoom = 1;
            }

            var somethingX = end.X - start.X;
            var somethingY = end.Y - start.Y;


            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {
                    double real = start.X + 2 + (somethingX) * ((double)x / WIDTH) * 1/5;
                    double imag = start.Y + 2 + (somethingY) * ((double)y / HEIGHT) * 1/5;
                    var iters = InMandelbrot(new Complex(real, imag));
                    var color = 255 - (iters * 255 / MAX_ITER);


                    b.SetPixel(x, y, Color.FromArgb(color, color, color));
                }
            }

            using MemoryStream memory = new MemoryStream();
            b.Save(memory, ImageFormat.Png);
            memory.Position = 0;

            //AvIrBitmap is our new Avalonia compatible image. You can pass this to your view
            Avalonia.Media.Imaging.Bitmap AvIrBitmap = new Avalonia.Media.Imaging.Bitmap(memory);
            return AvIrBitmap;

        }
    }
}
