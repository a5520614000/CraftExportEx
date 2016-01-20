using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftExport.Helpers
{
    public class MealLibrary
    {
        public List<Meal> Meals;

        public MealLibrary()
        {
            Meals = new List<Meal>
            {
                new Meal {Id = 4666, Name = "盐渍鳕鱼"},
                new Meal {Id = 4667, Name = "生鲜牡蛎"},
                new Meal {Id = 4668, Name = "水煮鲷鱼"},
                new Meal {Id = 4669, Name = "烤黑鳎"},
                new Meal {Id = 4670, Name = "猫魅风味海产烤串"},
                new Meal {Id = 4671, Name = "油炸盐渍鳕鱼丸"},
                new Meal {Id = 6144, Name = "海神汤"},
                new Meal {Id = 4672, Name = "油炸鲯鳅块"},
                new Meal {Id = 10146, Name = "王冠蛋糕"},
                new Meal {Id = 4716, Name = "羊奶麦粥"},
                new Meal {Id = 4717, Name = "石子汤"},
                new Meal {Id = 4718, Name = "翡翠豆汤"},
                new Meal {Id = 4719, Name = "韭菜洋葱汤"},
                new Meal {Id = 7574, Name = "新薯冷汤"},
                new Meal {Id = 9333, Name = "鱼翅汤"},
                new Meal {Id = 4722, Name = "新薯泥"},
                new Meal {Id = 4723, Name = "奶酪烩饭"},
                new Meal {Id = 4724, Name = "奶酪蛋奶酥"},
                new Meal {Id = 4725, Name = "罗兰莓奶酪蛋糕"},
                new Meal {Id = 4726, Name = "薄荷酸奶昔"},
                new Meal {Id = 4727, Name = "罗兰莓酸奶昔"},
                new Meal {Id = 12862, Name = "洛夫坦山羊汤"},
                new Meal {Id = 12863, Name = "绿宝石汤"},
                new Meal {Id = 4720, Name = "白鱼汤"},
                new Meal {Id = 4721, Name = "五海杂烩汤"},
                new Meal {Id = 12861, Name = "洋葱焗奶油汤"},
                new Meal {Id = 12864, Name = "鲜贝羹"},
                new Meal {Id = 12865, Name = "炖番茄"},
            };
        }
    }
}
