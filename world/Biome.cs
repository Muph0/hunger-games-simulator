using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace HungerGames.world
{
    public class CharInfo
    {
        public CharInfo(Color Fore, Color Back, char chr)
        {
            this.Fore = Fore;
            this.Back = Back;
            this.Char = chr;
        }
        public Color Fore, Back;
        public char Char;

        internal CharInfo Clone()
        {
            return new CharInfo(Fore, Back, Char);
        }
    }
    public class Biome
    {
        public const int _tiles_wide = 28;
        public const int _tiles_high = 20;

        public enum Type
        {
            Woods = 1,
            DeepWoods,
            Plains,
            Rocks,
            Desert,
        }

        public Zone[] zony;
        public int[] tilesOwner;
        public int[] heightMap;
        public float[] lightMap;
        public float[] blurred;

        public CharInfo CharInfoAt(int i)
        {
            Zone z = zony[tilesOwner[i]];
            i = Array.IndexOf(z.mojeDlazdicky, i);
            return z.dlazdickyInfo[i].Clone();
        }
        public CharInfo getCharInfoWithShadowAt(int i)
        {
            Zone z = zony[tilesOwner[i]];
            float lightness = lightMap[i] + 0.0f;

            i = Array.IndexOf(z.mojeDlazdicky, i);

            CharInfo result = z.dlazdickyInfo[i].Clone();

            result.Fore = Color.FromArgb(clamp(result.Fore.R * lightness), clamp(result.Fore.G * lightness), clamp(result.Fore.B * lightness));
            result.Back = Color.FromArgb(clamp(result.Back.R * lightness), clamp(result.Back.G * lightness), clamp(result.Back.B * lightness));

            return result;
        }
        private byte clamp(double d)
        {
            return d < 0 ? (byte)0 : d > 255 ? (byte)255 : (byte)d;
        }

        public void Populate(Random rnd)
        {
            Type[] biomy = new Type[] { Type.Woods, Type.Plains, Type.DeepWoods, Type.Rocks };

            bool hills = rnd.Next() % 3 == 0;
        }
        public void generateHeightMap(Random rnd, int hills_max_height)
        {
            this.heightMap = new int[_tiles_wide * _tiles_high];
            Bitmap Hmap = new Bitmap(_tiles_wide, _tiles_high);
            Graphics g_Hmap = Graphics.FromImage(Hmap);
            int startDepth = 0;
            int endDepth = 8;
            double alphaFactor = 2.2;
            for (var depth = 1; depth <= endDepth - startDepth; depth++)
            {
                int dynW = 1 << (depth + startDepth);
                int dynH = dynW;

                if (dynW > Hmap.Width)
                    dynW = Hmap.Width;
                if (dynH > Hmap.Height)
                    dynH = Hmap.Height;

                Bitmap noiseBMP = new Bitmap(dynW, dynH);
                Graphics dynCx = Graphics.FromImage(noiseBMP);

                for (int y = 0; y < dynH; y++)
                    for (int x = 0; x < dynW; x++)
                    {
                        byte clr = (byte)(rnd.Next() % 256);
                        double alpha = (2 / Math.Pow(depth, alphaFactor));
                        alpha = alpha > 1 ? 1 : alpha < 0 ? 0 : alpha;
                        Brush b = new SolidBrush(Color.FromArgb((byte)(alpha * 255), clr, clr, clr));
                        dynCx.FillRectangle(b, new Rectangle(x, y, 1, 1));
                    }

                g_Hmap.DrawImage(noiseBMP, 0, 0, Hmap.Width, Hmap.Height);
            }

            int max_peak = 0;
            for (int y = 0; y < Hmap.Height; y++)
                for (int x = 0; x < Hmap.Width; x++)
                {
                    int c = Hmap.GetPixel(x, y).R;
                    max_peak = c > max_peak ? c : max_peak;
                }
            for (int y = 0; y < Hmap.Height; y++)
                for (int x = 0; x < Hmap.Width; x++)
                {
                    int c = Hmap.GetPixel(x, y).R;
                    if (c < max_peak - hills_max_height)
                    {
                        c = (int)((c - max_peak + hills_max_height) / 8f + max_peak - hills_max_height);
                    }
                    heightMap[x + y * _tiles_wide] = c;
                }
        }
        public void generateShadowMap(int time_int, float flatten)
        {
            this.lightMap = new float[_tiles_wide * _tiles_high];

            // LIGHT + 

            float time = time_int / 20000F;
            Microsoft.Xna.Framework.Vector3 light = new Microsoft.Xna.Framework.Vector3(0, 0, -.5f);
            light.X = (float)(-.6 * Math.Cos(Math.PI * (time * 0.7 + 0.15)));
            light.Y = (float)(.6 * Math.Sin(Math.PI * (time * 0.7 + 0.15)));
            light.Z = -(float)(.05 * Math.Sin(Math.PI * time) + .00);


            light.Normalize();
            light = -light;

            for (int y = 0; y < _tiles_high; y++)
                for (int x = 0; x < _tiles_wide; x++)
                {
                    int local_h = heightMap[x + y * _tiles_wide];

                    Microsoft.Xna.Framework.Vector3 n = new Microsoft.Xna.Framework.Vector3(0, -1, 0);
                    Microsoft.Xna.Framework.Vector3 e = new Microsoft.Xna.Framework.Vector3(1, 0, 0);
                    Microsoft.Xna.Framework.Vector3 s = new Microsoft.Xna.Framework.Vector3(0, 1, 0);
                    Microsoft.Xna.Framework.Vector3 w = new Microsoft.Xna.Framework.Vector3(-1, 0, 0);

                    Microsoft.Xna.Framework.Vector3 N = new Microsoft.Xna.Framework.Vector3(0, -1, 0);
                    Microsoft.Xna.Framework.Vector3 E = new Microsoft.Xna.Framework.Vector3(1, 0, 0);
                    Microsoft.Xna.Framework.Vector3 S = new Microsoft.Xna.Framework.Vector3(0, 1, 0);
                    Microsoft.Xna.Framework.Vector3 W = new Microsoft.Xna.Framework.Vector3(-1, 0, 0);

                    Microsoft.Xna.Framework.Vector3 normal = new Microsoft.Xna.Framework.Vector3(0, 0, 0);
                    Microsoft.Xna.Framework.Vector3 pre = new Microsoft.Xna.Framework.Vector3(0, 0, 0);

                    if (y > 0)
                        n.Z = (heightMap[x + (y - 1) * _tiles_wide] - local_h) / flatten;
                    if (x < _tiles_wide - 1)
                        e.Z = (heightMap[x + 1 + y * _tiles_wide] - local_h) / flatten;
                    if (y < _tiles_high - 1)
                        s.Z = (heightMap[x + (y + 1) * _tiles_wide] - local_h) / flatten;
                    if (x > 0)
                        w.Z = (heightMap[x - 1 + y * _tiles_wide] - local_h) / flatten;

                    pre = Microsoft.Xna.Framework.Vector3.Cross(W, n);
                    pre.Normalize();
                    normal += pre;

                    pre = Microsoft.Xna.Framework.Vector3.Cross(N, e);
                    pre.Normalize();
                    normal += pre;

                    pre = Microsoft.Xna.Framework.Vector3.Cross(E, s);
                    pre.Normalize();
                    normal += pre;

                    pre = Microsoft.Xna.Framework.Vector3.Cross(S, w);
                    pre.Normalize();
                    normal += pre;

                    normal.Normalize();

                    float brightness = Microsoft.Xna.Framework.Vector3.Dot(light, normal);

                    //brightness = .2f * (float)Math.Pow(1.7 * brightness, 3) + brightness;
                    //brightness *= .5f + (float)(3 * Math.Sin(Math.PI * time));
                    //brightness = .2f * (float)Math.Pow(1.7 * brightness, 3) + brightness;
                    
                    brightness = brightness < 0 ? 0 : brightness;
                    lightMap[x + y * _tiles_wide] = brightness + .05f;
                }
            //////////////////////////////////////////
            // end calculating normals/shadow map
            //////////////////////////////////////////

            // AVERAGE 3x3 BLUR
            blurred = new float[lightMap.Length];
            for (int i = 0; i < 28 * 20; i++)
            {
                float this_pix = 0;
                int miss = 0;
                for (int j = 0; j < 9; j++)
                {
                    float src = 0;
                    if (i % 28 + j % 3 - 1 >= 0 && i % 28 + j % 3 - 1 < 28 &&
                        i / 28 + j / 3 - 1 >= 0 && i / 28 + j / 3 - 1 < 20)
                    {
                        src = lightMap[i % 28 + j % 3 - 1 + (i / 28 + j / 3 - 1) * 28];
                    }
                    else
                    {
                        miss++;
                    }
                    this_pix += src / 9f;
                }
                this_pix = this_pix / (9 - miss) * 9;
                blurred[i] = this_pix;
            }

            for (int i = 0; i < 28 * 20; i++)
            {
                float d = 0.20f;
                lightMap[i] = lightMap[i] * d + blurred[i] * (1 - d);
            }

            //////////////////////////////////////////
            // HDR eye-adaptation
            //////////////////////////////////////////
            if (true)
            {
                float max = 0;
                for (int i = 0; i < 28 * 20; i++)
                {
                    if (lightMap[i] > max)
                        max = lightMap[i];
                }
                max = (float)(0.8 + 0.3 * Math.Sin(Math.PI * time)) / max;
                for (int i = 0; i < 28 * 20; i++)
                {
                    lightMap[i] *= max;
                }
            }
        }
    }
}
