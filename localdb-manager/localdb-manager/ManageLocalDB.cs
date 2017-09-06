using System;
using System.Collections.Generic;
using System.Data.SqlLocalDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace localdb_manager
{
    public static class ManageLocalDB
    {
        /// <summary>
        /// Возвращает список всех экземпляров
        /// </summary>
        public static IEnumerable<ISqlLocalDbInstanceInfo> GetInstance
        {
            get
            {
                List<ISqlLocalDbInstanceInfo> instances = new List<ISqlLocalDbInstanceInfo>();
                foreach (string name in SqlLocalDbApi.GetInstanceNames())
                {
                    ISqlLocalDbInstanceInfo info = SqlLocalDbApi.GetInstanceInfo(name);
                    instances.Add(info);
                }
                return instances;
            }
        }
    }
}
