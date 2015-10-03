﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jxGameFramework.Controls;
using jxGameFramework.Components;
using jxGameFramework.Data;
using Microsoft.Xna.Framework;

namespace jxGameFramework.Controls
{
    public class TabBar : Control
    {
        Label _tabtitle;
        Font _fnt;
        Font _fntnoshadow;
        bool _selected;
        public int TabID { get; set; }
        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
                var transcolor = new Color(0, 0, 0, 20);
                if (_tabtitle != null)
                {
                    if (_selected)
                    {
                        _tabtitle.Color = Color.Black;
                        _tabtitle.Font = _fntnoshadow;
                        this.Color = Color.White;
                    }
                    else
                    {
                        _tabtitle.Color = Color.White;
                        _tabtitle.Font = _fnt;
                        this.Color = Color.Red;
                    }
                }
            }
        }
        public string Title
        {
            get
            {
                return _tabtitle.Text;
            }
            set
            {
                _tabtitle.Text = value;
            }
        }
        public TabBar(Font fnt,Font noshadow)
        {
            _fnt = fnt;
            _fntnoshadow = noshadow;
        }
        public override void Initialize()
        {
            this.Texture = Sprite.CreateTextureFromFile(@"Content\selection-tab.png");
            this.Width = Texture.Width;
            this.Height = Texture.Height;
            this.Color = Color.White;

            _tabtitle = new Label()
            {
                Font = _fnt,
                Color = Color.Black,
                Top =3,
                Margin = Origins.TopCenter,
            };

            ChildSprites.Add(_tabtitle);

            base.Initialize();
        }
    }
    public class TabControl : Control
    {
        List<TabBar> _tablist = new List<TabBar>();
        List<Control> _pagelist=new List<Control>();

        Sprite _presentPage;
        TabBar _presentTab;
        Font fnt;
        Font fntNoShadow;
        Origins _tabmargin = Origins.TopLeft;
        public Origins TabMargin
        {
            get
            {
                return _tabmargin;
            }
            set
            {
                _tabmargin = value;
            }
        }

        public override void Initialize()
        {
            fnt = new Font("msyh.ttc", 13)
            {
                EnableShadow = true,
                ShadowColor = Color.Black,
                ShadowYOffset = 1,
            };
            fntNoShadow = new Font("msyh.ttc", 13);
            base.Initialize();
        }
        private void SwitchTab(object sender,EventArgs e)
        {
            _presentTab = (TabBar)sender;
            foreach (TabBar t in _tablist)
            {
                t.Selected = false;
            }
            _presentTab.Selected = true;
            _presentPage = _pagelist[_presentTab.TabID];
        }
        public void SwitchTab(int TabID)
        {
            SwitchTab((object)_tablist[TabID], EventArgs.Empty);
        }
        public void AddTab(Control Panel,string tabTitle)
        {
            if(Panel !=null)
            {
                Panel.Parent = this;
                _pagelist.Add(Panel);
            }
            var bar = new TabBar(fnt,fntNoShadow)
            {
                Parent=this,
                Top=0,
                Left = _tablist.Count * 120,
                Right= _tablist.Count * 120,
                Margin = TabMargin,
            };
            bar.Initialize();
            bar.Parent = this;
            bar.TabID = _tablist.Count;
            bar.Title = tabTitle;
            bar.Selected = true;
            bar.Click += SwitchTab;
            _presentTab = bar;
            
            foreach (TabBar t in _tablist)
            {
                t.Selected = false;
            }
            _tablist.Add(bar);           
        }

        public override void Draw(GameTime gameTime)
        {
            if(_presentPage != null)
                _presentPage.Draw(gameTime);
            foreach (TabBar s in _tablist)
                s.Draw(gameTime);
            if(_presentTab!=null)
                _presentTab.Draw(gameTime);
            base.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {
            foreach (TabBar s in _tablist)
                s.Update(gameTime);
            if(_presentPage!=null)
                _presentPage.Update(gameTime);
        }
    }
}
