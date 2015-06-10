using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APS.DB
{
    public class NowInfoDAL
    {
        public List<NowInfo> SelectInfo()
        {
            using(var context = new APSEntities())
            {
                var res = from x in context.NowInfoes
                          select x;
                return res.ToList();
            }
        }

        public int getLastId()
        {
            using(var context = new APSEntities())
            {
                var res = from x in context.NowInfoes
                          select x;

                List<NowInfo> list = res.ToList();
                int lastId = 0;

                foreach(var l in list)
                {
                    lastId = l.Id;
                }
                return lastId;
            }
        }

        public void InsertNew(NowInfo ni)
        {
            using(var context = new APSEntities())
            {
                if(context.NowInfoes.Count() > 0)
                {
                    int id = getLastId();

                    NowInfo nowInfo = context.Set<NowInfo>().First(x => x.Id == id);

                    context.Entry(nowInfo).CurrentValues.SetValues(ni);
                    context.SaveChanges();
                }
                else
                {
                    context.NowInfoes.Add(ni);
                    context.SaveChanges();
                }
            }
        }

        public int GetIdByUser()
        {
            using (var context = new APSEntities())
            {
                int id = getLastId();
                NowInfo ni = context.NowInfoes.FirstOrDefault(x => x.Id == id);
                return ni.UserId;
            }
        }

        public void DeleteNowInfo()
        {
            using(var context = new APSEntities())
            {
                int id = getLastId();
                var x = context.NowInfoes.Find(id);
                context.NowInfoes.Remove(x);
                context.SaveChanges();
            }
        }
    }
}
