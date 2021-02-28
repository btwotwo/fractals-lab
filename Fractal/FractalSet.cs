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
    class FractalSet
    {
        private static readonly object BitmapLock = new object();
        const int WIDTH = 1200;
        const int HEIGHT = 800;
        const int MAX_ITER = 100;

        public IBitmap Generate(PointF start, PointF end)
        {
            var b = new System.Drawing.Bitmap(WIDTH, HEIGHT);

            var lX = end.X - start.X;
            var lY = end.Y - start.Y;


            Parallel.ForEach(Enumerable.Range(0, WIDTH), x =>
            {
                Parallel.ForEach(Enumerable.Range(0, HEIGHT), y =>
                {
                    double real = start.X + (lX * ((double)x / WIDTH));
                    double imag = start.Y + (lY * ((double)y / HEIGHT));
                    var iters = InSet(new Complex(real, imag));
                    var color = 255 - (iters * 255 / MAX_ITER);
                    lock (BitmapLock)
                    {
                        b.SetPixel(x, y, Color.FromArgb(color, color, color));
                    }

                });
            });

            using MemoryStream memory = new MemoryStream();
            b.Save(memory, ImageFormat.Png);
            memory.Position = 0;

            //AvIrBitmap is our new Avalonia compatible image. You can pass this to your view
            Avalonia.Media.Imaging.Bitmap AvIrBitmap = new Avalonia.Media.Imaging.Bitmap(memory);
            return AvIrBitmap;
        }

        private static Complex SetFunction(Complex z)
        {
            var cubeZ = Complex.Pow(z, 3);

            return ((3 * cubeZ * z) + 1) / (4 * cubeZ);
        }       
        
        private static int InSet(Complex z)
        {
            var iter = 0;
            bool ShouldContinue() => Complex.Abs(Complex.Pow(z, 4) - 1) > 0.00001;

            while (ShouldContinue() && iter < MAX_ITER)
            {
                z = SetFunction(z);
                iter++;
            }

            return iter;
        }
    }
}
