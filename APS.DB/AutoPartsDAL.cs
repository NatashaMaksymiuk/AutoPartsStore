using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APS.DB
{
    public class AutoPartsDAL
    {
        public List<AutoPart> SelectParts()
        {
            using (var context = new APSEntities())
            {
                var res = from x in context.AutoParts
                          select x;

                return res.ToList();
            }
        }

        public AutoPart SelectPartsFromName(string name)
        {
            using (var context = new APSEntities())
            {
                AutoPart ap = context.AutoParts.FirstOrDefault(x => x.Name == name);
                return ap;
            }
        }

        public AutoPart SelectOnePart(int id)
        {
            using (var context = new APSEntities())
            {
                return context.AutoParts.FirstOrDefault(i => i.Id == id);
            }
        }

        public List<AutoPart> SelectPartsFromCategory(int nCategory)
        {
            using (var context = new APSEntities())
            {
                var res = from x in context.AutoParts
                          where x.CategoryId == nCategory
                          select x;

                return res.ToList();
            }
        }

        public List<AutoPart> SelectPartsFromPrice(int min, int max)
        {
            using (var context = new APSEntities())
            {
                var res = from x in context.AutoParts
                          where x.Price>min && x.Price<max
                          select x;

                return res.ToList();
            }
        }

        public List<AutoPart> SelectPartsFromUser(int usId)
        {
            using (var context = new APSEntities())
            {
                var res = from x in context.AutoParts
                          where x.UserId == usId
                          select x;

                return res.ToList();
            }
        }

        public void InsertNewParts(AutoPart ap)
        {
            using(var context = new APSEntities())
            {
                if(IsAPValid(ap.Name))
                {
                    context.AutoParts.Add(ap);
                    context.SaveChanges();
                }
            }
        }

        public bool IsAPValid(string name)
        {
            using(var context = new APSEntities())
            {
                AutoPart ap = context.AutoParts.FirstOrDefault(x => x.Name == name);
                if(ap == null)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
