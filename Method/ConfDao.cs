using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernateGenLib.Base;
using NHibernate;
using NHibernateGenLib.Domain.AppUpload;

namespace WindowsFormsUploadApp.Method
{
    public class ConfDao:Dao
    {
        private static String Config_LastUploadFilePath = "last_upload_file_path";
        private static String Config_UploadPath = "upload_path";

        public static String QueryConfig(String name)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var list = session.QueryOver<TbConfig2>().Where(c => c.Name == name).List();
                if (list.ElementAt(0).Val == null) return "";
                return list.ElementAt(0).Val.ToString();
            }
        }
        private static void updateConfig(String name, String val)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var list = session.QueryOver<TbConfig2>().Where(c => c.Name == name).List();
                var entry = list.ElementAt(0);
                entry.Val = val;
                Update(entry);
            }
        }
        public static String QLastUploadFilePath()
        {
            return QueryConfig(Config_LastUploadFilePath);
        }
        public static void UpdateLastUploadFilePath(String val)
        {
            updateConfig(Config_LastUploadFilePath, val);
        }
        public static String QUploadPath()
        {
            return QueryConfig(Config_UploadPath);
        }
        public static void UpdateUploadPath(String val)
        {
            updateConfig(Config_UploadPath, val);
        }

    }
}
