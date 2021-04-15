using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace YANMFA.Games.Alex.SpiderFighter.Models
{
    public static class Textures
    {
        public static Dictionary<string,Bitmap> MobBitmaps { get; private set; }
        public static Dictionary<string,Bitmap> ItemBitmaps { get; private set; }
        public static Dictionary<string,Bitmap> UtilitiesBitmaps { get; private set; }

        public static void LoadTextures()
        {
            string rootpath = "./Assets/Alex/SpiderFighter/Textures/";
            
            ItemBitmaps = new Dictionary<string, Bitmap>();
            ItemBitmaps.Add("Stone", new Bitmap(rootpath + "Floortexture1.png"));
            ItemBitmaps.Add("Crate", new Bitmap(rootpath + "crate.png"));
            ItemBitmaps.Add("WoodPlank", new Bitmap(rootpath + "woodplank1.png"));
            ItemBitmaps.Add("Wood",new Bitmap(rootpath + "wood1.png"));
            ItemBitmaps.Add("Dirt",new Bitmap(rootpath + "dirt2.png"));
            ItemBitmaps.Add("Grass", new Bitmap(rootpath + "grass1.png"));
            ItemBitmaps.Add("StoneBrick", new Bitmap(rootpath + "brickstone.png"));
            ItemBitmaps.Add("BookShelf", new Bitmap(rootpath + "bookshelf.png"));
            ItemBitmaps.Add("Torch", new Bitmap(rootpath + "torch.png"));
            ItemBitmaps.Add("Irondoor", new Bitmap(rootpath + "irondoor.png"));
            ItemBitmaps.Add("IrondoorClose", new Bitmap(rootpath +"irondoorclose.png"));
            ItemBitmaps.Add("Window",new Bitmap(rootpath + "window.png"));
            ItemBitmaps.Add("Table",new Bitmap(rootpath + "table.png"));
            ItemBitmaps.Add("DeepDark",new Bitmap(rootpath + "deepdark.png"));
            ItemBitmaps.Add("Brick",new Bitmap(rootpath + "brick.png"));
            ItemBitmaps.Add("WoodWall",new Bitmap(rootpath + "woodwall.png"));
            ItemBitmaps.Add("WoodCorner",new Bitmap(rootpath + "woodcorner.png"));

            MobBitmaps = new Dictionary<string, Bitmap>();
            MobBitmaps.Add("HumanLeft", new Bitmap(rootpath + "humanleft.png"));
            MobBitmaps.Add("HumanLeftWalk", new Bitmap(rootpath + "humanleftwalk.png"));
            MobBitmaps.Add("HumanRight", new Bitmap(rootpath + "humanright.png"));
            MobBitmaps.Add("HumanRightWalk", new Bitmap(rootpath + "humanrightwalk.png"));

            UtilitiesBitmaps = new Dictionary<string, Bitmap>();
            UtilitiesBitmaps.Add("RifleRight" , new Bitmap(rootpath + "rifleright.png"));
            UtilitiesBitmaps.Add("RifleRightShoot", new Bitmap(rootpath + "riflerightshoot.png"));
            UtilitiesBitmaps.Add("RifleLeft" ,new Bitmap(rootpath+ "rifleleft.png") );
            UtilitiesBitmaps.Add("RifleLeftShootTex", new Bitmap(rootpath + "rifleleftshoot.png"));

        }
        
    }
}