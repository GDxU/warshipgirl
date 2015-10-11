using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jxGameFramework.Scene;
using jxGameFramework.Components;
using jxGameFramework.Controls;
using jxGameFramework.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using jxGameFramework;

namespace ClassLibTestbed
{
    class TestScene : BaseScene
    {
        Label lyric;
      
        public override void Initialize()
        {
            this.Texture = Sprite.CreateTextureFromFile(@"Content/landscape2.jpg");
            lyric = new Label()
            {
                Left = 0,
                Top = 0,
                Font = new Font("msyh.ttc", 15)
                {
                    EnableBorder = true,
                    BorderColor = Color.Black
                },
            };
            ChildSprites.Add(lyric);
            base.Initialize();
            lyric.Text = @"下一站 下一站
是锦江乐园站
下一站 下一站
是上海火车站
乘客可换乘轨道交通
3号线 4号线
需要换乘的乘客
请用公交卡
下一站 下一站
是金沙江路站
下一站 下一站
是中二公园站
可换乘中二的2号线
请为弯了的列车让个座
延安西路虹桥路后
是宜山路站
可换乘轨道交通⑨号线
请注意 请注意
本次列车醉了
肇家浜路 徐家绘
换乘七十一号线
本次列车终点站江苏花桥
下一站是花阳路
请为小泉花阳让个座
人民公园 新田地
上海体育馆
请未需要帮助的列车让个路
上海图书馆 上海动物园
乘客们请注噫
上海的海 是海未的海
下一站 下一站
是工口足球场
下一站 下一站
是人民扫广场
乘客可换乘轨道交通
1号线 2号线
需要换乘的乘客
请不要下跪
下一站 下一站
是张江高科站
下一站 下一站
是金科垃路站
可吸收20米下氮磷钾
请为没有UR的非洲乘客们让个座
南惊希露静安寺后
是昌平桥
可换乘轨道交通中央线
乘客们 请注意
请为需要帮助的Nico让个家
欢迎您乘坐轨道交通磁悬浮列车
上海音乃木坂学院向您问好
本次列车终点站
是普通的普通国际姬场
由于下雨
楼梯和地面比较滑
请为需要梆梆的乘客让个座
下车的乘客请您Niconiconi
在屏蔽门完全打开后从穗村下车
欢迎您乘坐乘客
请为需要帮助的列车让个座
注意眼睛安全
从左边醉了的车门下车
当心身后龙爪手
本次列车本站不停靠
严禁携带易爆花阳
下一站西木野医学中心
列车即将启动
星中路
虹桥空港
凛平路到了
本次星空凛号列车终点站
虹桥足球场
下一站 上海Yooooooooooooooooo泳馆
请花阳 真姬 注意水下安全
下一站 漕宝路
从左边下车
请为需要帮助的南小鸟让个路
欢迎您乘坐
4号 2号 9号 8号 6号 1号 7号 3号 5号线
本次列车终点站
辉夜城到了
请全体乘客在安全门完全打开后
从左边车门下车
开门请当心
注意身后安全
We're now at the terminal station: Kaguya City
Please get ready to exit from the left side";
        }
    }
}