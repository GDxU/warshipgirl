using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jxGameFramework;

namespace ClassLibTestbed
{
    public static class EntryPoint
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var game = new Class1();
            game.Run();
        }
    }
    public class Class1 : Game
    {
        public override void Initialize()
        {
            var sel = new TestScene();
            Scenes.Add("test",sel);
            Scenes.Navigate("test");
            base.Initialize();
        }
    }
}
