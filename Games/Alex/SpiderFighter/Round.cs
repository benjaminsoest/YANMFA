using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Dynamic;
using System.IO;
using System.Windows.Forms;
using YANMFA.Core;
using YANMFA.Games.Alex.SpiderFighter.Models;
using YANMFA.Games.Alex.SpiderFighter.Models.Decorations;
using YANMFA.Games.Alex.SpiderFighter.Models.Mobs;

namespace YANMFA.Games.Alex.SpiderFighter
{
    public static class Round
    {
        public static float QuotientWidth = 1;
        public static float QuotientHeight = 1;
        public static float StartWidth;
        public static float StartHeight;
        public static Level CurrentLevel { get; set; }
        public static List<Bullet> Bullets { get; set; }
        public static List<Level> Levels { get; set; }

        public static Human PlayerOne { get; set; }

        public static void Update()
        {
            PlayerOne.Update();
            for (int i = CurrentLevel.Items.Count - 1; i >= 0; i--)
            {
                CurrentLevel.Items[i].Update();
            }
            for (int i = CurrentLevel.Mobs.Count - 1; i >= 0; i--)
            {
                CurrentLevel.Mobs[i].Update();
            }
            for (int i = Bullets.Count -1; i >= 0; i--)
            {
                Bullets[i].Update();
            }            
        }

        public static void Render(Graphics g)
        {
                                       
            Matrix scaleMatrix = new Matrix();
            scaleMatrix.Scale(QuotientWidth, QuotientHeight, MatrixOrder.Append); 
            
            g.Transform = scaleMatrix;
            g.TranslateTransform((-PlayerOne.Hitbox.X) + ((StaticDisplay.DisplayWidth / 2 )), (-PlayerOne.Hitbox.Y * QuotientHeight) + (StaticDisplay.DisplayHeight / 2));  

            foreach (var item in CurrentLevel.Items) { item.Render(g); }
            foreach (var mobs in CurrentLevel.Mobs) { mobs.Render(g); }
            foreach (var bullet in Bullets) { bullet.Render(g); }

            PlayerOne.Render(g);
        }

        public static void MouseDown(MouseEventArgs e)
        {
            foreach (var item in CurrentLevel.Items){item.MouseDown(e);}
            foreach (var mobs in CurrentLevel.Mobs){mobs.MouseDown(e);}
            PlayerOne.MouseDown(e);
        }

        public static void KeyDown(KeyEventArgs e)
        {
            foreach (var item in CurrentLevel.Items){item.KeyDown(e);}
            foreach (var mobs in CurrentLevel.Mobs){mobs.KeyDown(e);}
            PlayerOne.KeyDown(e);
        }
           
        public static void Resize(int width, int height)
        {
            QuotientHeight = StaticDisplay.DisplayHeight /StartHeight ;
            QuotientWidth = StaticDisplay.DisplayWidth / StartWidth;
        }

        public static void LoadLevels()
        {
            Levels = new List<Level>();
            Bullets = new List<Bullet>();

            DirectoryInfo info = new DirectoryInfo("./Assets/Alex/SpiderFighter/Levels/");
            foreach (FileInfo item in info.GetFiles())
            {
                string currentstring = "";
                List<string> lines = new List<string>();
                Level level = new Level();

                StreamReader stream = item.OpenText();
                string fullText = stream.ReadToEnd();

                for (int i = 0; i < fullText.Length; i++)
                {
                    if (fullText[i] != '>')
                    {
                        currentstring = currentstring + fullText[i];
                    }
                    else
                    {
                        currentstring.Replace('>', '\0');
                        currentstring = currentstring.Replace("\r\n", "");
                        lines.Add(currentstring);
                        currentstring = "";
                    }
                }

                foreach (string line in lines)
                {
                    string currentdata = "";
                    List<string> data = new List<string>();

                    if (line == lines[0])
                    {
                        for (int i = 0; i < line.Length; i++)
                        {
                            if (line[i] != ';')
                            {
                                currentdata = currentdata + line[i];
                            }
                            else
                            {
                                currentdata.Replace(';', '\0');
                                data.Add(currentdata);
                                currentdata = "";
                            }
                        }
                        level.Index = (Convert.ToInt32(data[0]), Convert.ToInt32(data[1]));
                    }
                    else
                    {
                        
                        for (int i = 0; i < line.Length; i++)
                        {
                            if (line[i] != ';')
                            {
                                currentdata = currentdata + line[i];
                            }
                            else
                            {
                                currentdata.Replace(';', '\0');
                                data.Add(currentdata);
                                currentdata = "";
                            }
                        }
                        Textures.ItemBitmaps.TryGetValue("IronDoorClose", out Bitmap bitmap1);
                        switch (data[0])
                        {
                            case "Block":
                                if (data[6] == "True")
                                {
                                    Textures.ItemBitmaps.TryGetValue(data[5], out Bitmap bitmap);
                                    var barrier = new BarrierBlock(new RectangleF((float)Convert.ToDouble(data[1]), (float)Convert.ToDouble(data[2]), (float)Convert.ToDouble(data[3]), (float)Convert.ToDouble(data[4])), bitmap);
                                    level.Items.Add(barrier);
                                }
                                else if (data[6] == "False")
                                {
                                    Textures.ItemBitmaps.TryGetValue(data[5], out Bitmap bitmap);
                                    var decoblock = new DecoBlock(new RectangleF((float)Convert.ToDouble(data[1]), (float)Convert.ToDouble(data[2]), (float)Convert.ToDouble(data[3]), (float)Convert.ToDouble(data[4])), bitmap);
                                    level.Items.Add(decoblock);
                                }
                                break;
                            case "Door":
                                var door = new Door(
                                    new RectangleF((float)Convert.ToDouble(data[1]), (float)Convert.ToDouble(data[2]),
                                        (float)Convert.ToDouble(data[3]), (float)Convert.ToDouble(data[4])), bitmap1);

                                level.Items.Add(door);
                                break;
                            default:
                                break;
                        }
                    }                    
                }
                Levels.Add(level);               
            }
        }
    }
}