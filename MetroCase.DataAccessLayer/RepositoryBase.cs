using MetroCase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroCase.DataAccessLayer
{
    public class RepositoryBase
    {
        private static MetroCaseEntities dbcontext;
        private static object loc = new object();

        protected RepositoryBase()
        {

        }
        public static MetroCaseEntities CreateContext()
        {
            if (dbcontext == null)
            {
                lock (loc)
                {
                    if (dbcontext == null)
                    {
                        dbcontext = new MetroCaseEntities();
                    }
                }
            }
            return dbcontext;
        }
    }
}
