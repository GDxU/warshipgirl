using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Threading.Tasks;
using jxGameFramework.Components;
using jxGameFramework.Controls;
using jxGameFramework.Data;
using jxGameFramework.Scene;
using WarshipGirl.Controls;
using Microsoft.Xna.Framework;

namespace WarshipGirl.Scene
{
    class XmlScene : BaseScene
    {
        internal void LoadXml(string path)
        {
            var doc = new XmlDocument();
            doc.Load(path);
            var xn = doc.SelectSingleNode("Scene");
            var nodelist = xn.SelectSingleNode("Complist");

            foreach (XmlNode xm in nodelist)
            {
                var xe = (XmlElement)xm;
                switch (xm.Name)
                {
                    case "Sprite":
                        this.CompList.Add(CreateSprite(xm));
                        break;
                    case "Control":
                        this.CompList.Add(CreateControl(xm));
                        break;
                    case "Button":
                        this.CompList.Add(CreateButton(xm));
                        break;
                    case "Text":
                        this.CompList.Add(CreateText(xm));
                        break;
                    default:
                        break;
                }
            }
        }
        private Control CreateControl(XmlNode xm)
        {
            var temp = new Control()
            {
                X = int.Parse(xm.SelectSingleNode("X").InnerText),
                Y = int.Parse(xm.SelectSingleNode("Y").InnerText),
                OriginType = (Origins)Enum.Parse(typeof(Origins),xm.SelectSingleNode("OriginType").InnerText),
                Width = int.Parse(xm.SelectSingleNode("Width").InnerText),
                Height = int.Parse(xm.SelectSingleNode("Height").InnerText),
                Texture=Sprite.CreateTextureFromFile(this.GraphicsDevice,xm.SelectSingleNode("Texture").InnerText)
            };
            var col = ((Color)typeof(Microsoft.Xna.Framework.Color).GetProperty(xm.SelectSingleNode("Color").InnerText).GetValue(temp));
            temp.Color = col;
            if (xm.SelectSingleNode("Click") !=null)
                BindingEvent(ref temp, "Click", xm.SelectSingleNode("Click").InnerText);
            return temp;
        }
        private Button CreateButton(XmlNode xm)
        {
            var temp = new Button()
            {
                X = int.Parse(xm.SelectSingleNode("X").InnerText),
                Y = int.Parse(xm.SelectSingleNode("Y").InnerText),
                OriginType = (Origins)Enum.Parse(typeof(Origins), xm.SelectSingleNode("OriginType").InnerText),
                Text = xm.SelectSingleNode("Text").InnerText
            };
            temp.Color = Color.White;
            if (xm.SelectSingleNode("Click") != null)
                BindingEvent(ref temp, "Click", xm.SelectSingleNode("Click").InnerText);
            return temp;
        }
        private Text CreateText(XmlNode xm)
        {
            var temp = new Text()
            {
                X = int.Parse(xm.SelectSingleNode("X").InnerText),
                Y = int.Parse(xm.SelectSingleNode("Y").InnerText),
                OriginType = (Origins)Enum.Parse(typeof(Origins), xm.SelectSingleNode("OriginType").InnerText),
                text = xm.SelectSingleNode("Text").InnerText,
                Font=new Font(this.GraphicsDevice,xm.SelectSingleNode("Font").Attributes["File"].Value,uint.Parse(xm.SelectSingleNode("Font").Attributes["Size"].Value),int.Parse(xm.SelectSingleNode("Font").Attributes["YOffset"].Value))
            };
            var col = ((Color)typeof(Microsoft.Xna.Framework.Color).GetProperty(xm.SelectSingleNode("Color").InnerText).GetValue(temp));
            temp.Color = col;
            return temp;
        }
        private void BindingEvent(ref Control cont,string Handler,string Method)
        {
            Type textf = typeof(Control);
            EventInfo clickevt = textf.GetEvent(Handler);
            Type tDelegate = clickevt.EventHandlerType;
            MethodInfo medi = typeof(Harbor).GetMethod(Method);
            Delegate d = Delegate.CreateDelegate(tDelegate, this, medi);
            MethodInfo addhandler = clickevt.GetAddMethod();
            Object[] addHandlerArgs = { d };
            addhandler.Invoke(cont, addHandlerArgs);
        }
        private void BindingEvent(ref Button cont,string Handler,string Method)
        {
            Type textf = typeof(Button);
            EventInfo clickevt = textf.GetEvent(Handler);
            Type tDelegate = clickevt.EventHandlerType;
            MethodInfo medi = typeof(Harbor).GetMethod(Method);
            Delegate d = Delegate.CreateDelegate(tDelegate, this, medi);
            MethodInfo addhandler = clickevt.GetAddMethod();
            Object[] addHandlerArgs = { d };
            addhandler.Invoke(cont, addHandlerArgs);
        }
        private Sprite CreateSprite(XmlNode xm)
        {
            var temp = new Sprite()
            {
                X = int.Parse(xm.SelectSingleNode("X").InnerText),
                Y = int.Parse(xm.SelectSingleNode("Y").InnerText),
                OriginType = (Origins)Enum.Parse(typeof(Origins),xm.SelectSingleNode("OriginType").InnerText),
                Width = int.Parse(xm.SelectSingleNode("Width").InnerText),
                Height = int.Parse(xm.SelectSingleNode("Height").InnerText),
                Texture=Sprite.CreateTextureFromFile(this.GraphicsDevice,xm.SelectSingleNode("Texture").InnerText)
            };
            var col = ((Color)typeof(Microsoft.Xna.Framework.Color).GetProperty(xm.SelectSingleNode("Color").InnerText).GetValue(temp));
            temp.Color = col;
            return temp;
        }
    }
}
