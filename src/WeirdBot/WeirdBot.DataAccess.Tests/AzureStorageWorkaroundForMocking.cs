using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeirdBot.DataAccess.Tests
{

    /*
     Yes, this is stupid. Microsoft made their Azure Storage libraries un-mockable, however, so
     this is what we get.
    */
    public class AzureStorageWorkaroundForMocking
    {
        public virtual CloudTableClient GetTableClient()
        {
            return null;
        }

        public virtual CloudTable GetTable(string tableName)
        {
            return null;
        }
    }
}
