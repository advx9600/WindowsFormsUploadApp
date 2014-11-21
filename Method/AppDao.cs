using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NHibernateGenLib.Base;
using NHibernate;
using NHibernateGenLib.Domain.AppUpload;
using System.IO;

namespace WindowsFormsUploadApp.Method
{
    public class AppDao:Dao
    {
        public static void addOrUpdate(TbApp app)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var list = session.QueryOver<TbApp>().Where(c => c.Pkg == app.Pkg).And(c=>c.BoardId == app.BoardId).List();
                if (list.Count == 0)
                {
                    session.Save(app);
                }
                else
                {
                    var list0 = list.ElementAt(0);
                    app.Id = list0.Id;
                    Update(app);
                    MyUtil.DelAppFiles(list0);                    
                }
            }
        }

        public static IList<TbApp> QAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.QueryOver<TbApp>().List();
            }
        }

        public static void Del(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var app = session.QueryOver<TbApp>().Where(c => c.Id == id).List().ElementAt(0);
                MyUtil.DelAppFiles(app);
                Remove(app);
            }
        }

    }
}
