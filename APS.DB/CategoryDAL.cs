using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APS.DB
{
    public class CategoryDAL
    {
        public List<Category> SelectCategory()
        {
            using (var context = new APSEntities())
            {
                var res = from x in context.Categories
                          select x;
                return res.ToList();
            }
        }

        public string SelectCategoryById(int id)
        {
            using (var context = new APSEntities())
            {
                Category c = context.Categories.FirstOrDefault(x => x.Id == id);
                return c.Name;
            }
        }

        public int SelectCategoryIdByName(string name)
        {
            using(var context = new APSEntities())
            {
                Category c = context.Categories.FirstOrDefault(x => x.Name == name);
                return c.Id;
            }
        }
    }
}
