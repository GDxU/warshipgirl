using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using WarshipGirl.Controls;
using jxGameFramework.Scene;
using jxGameFramework.Audio;
using jxGameFramework.Components;
using jxGameFramework.Animations;
using jxGameFramework.Animations.Curve;
using jxGameFramework.Controls;
using jxGameFramework.Data;

using WarshipGirl.Data;

namespace WarshipGirl.Scene
{
    partial class Harbor : XmlScene
    {
        ResourceLabel oilres;
        ResourceLabel bulres;
        ResourceLabel irores;
        ResourceLabel alures;
    }
    partial class Harbor : XmlScene
    {
        AudioStream bgstream;
        AudioPlayer player;
        public override void LoadContent()
        {
            if (((Game1)ParentGame).isNightMode)
                bgstream = new AudioStream(@"Content\port-night.mp3",true);
            else
                bgstream = new AudioStream(@"Content\port-day.mp3",true);

            oilres = new ResourceLabel()
            {
                X = 500,
                Y = 0,
                OriginType = Origins.TopCenter,
                Type = ResourceType.Oil,
                isBeginning=true
            };
            bulres = new ResourceLabel()
            {
                X = 625,
                Y = 0,
                OriginType = Origins.TopCenter,
                Type=ResourceType.Bullet
            };
            irores = new ResourceLabel()
            {
                X = 750,
                Y = 0,
                OriginType = Origins.TopCenter,
                Type = ResourceType.Iron
            };
            alures = new ResourceLabel()
            {
                X = 875,
                Y = 0,
                OriginType = Origins.TopCenter,
                Type = ResourceType.Aluminum
            };

            
            //flashanim = new Animation()
            //{
            //    BeginTime = new GameTime(new TimeSpan(0, 0, 0,0, 1921), new TimeSpan(1)),
            //    Duration = new TimeSpan(0, 0, 0,0,1000),
            //    ReturnToBeginning=true,
            //    PlayBack=true,
            //    BeginValue = 255,
            //    EndValue = 200,
            //    TargetProperty = typeof(Sprite).GetProperty("ColorA"),
            //    TargetSprite = testbtn,
            //    LoopMode = LoopMode.Forever,
            //    EasingFunction = new Power()
            //};

            LoadXml(@"Xml\UI_Harbor.xml");
            this.CompList.Add(oilres);
            this.CompList.Add(bulres);
            this.CompList.Add(irores);
            this.CompList.Add(alures);

            this.Load += Harbor_Load;
            this.Unload += Harbor_Unload;

            
            base.LoadContent();
        }

        void Harbor_Load(object sender, EventArgs e)
        {
            player = new AudioPlayer(bgstream);
            player.Play(true);
        }

        void Harbor_Unload(object sender, EventArgs e)
        {
            player.Stop();
        }

        public void fact_Click(object sender, EventArgs e)
        {
            var game = (Game1)ParentGame;
            game.Navigate(game.factory);
        }
    }
    partial class Harbor : XmlScene
    {
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
