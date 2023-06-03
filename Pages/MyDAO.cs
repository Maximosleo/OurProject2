using Microsoft.Extensions.Caching.Memory;

namespace OurProject2.Pages
{
    public class MyDAO
    {
        private static MyDAO instance = null;
        private readonly IMemoryCache _cache11;

        // Make constructor private so it can't be called from outside this class
        private MyDAO(IMemoryCache _cache)
        {
            _cache11 = _cache;

        }

        // Provide a global point of access to the instance
        public static MyDAO GetInstance(IMemoryCache _cache)
        {
            if (instance == null)
            {
                instance = new MyDAO(_cache);
            }
            return instance;
        }

        //save the data
        public void SaveToDB(string firstname, string lastname, string email, string password, string gender, string age, string identification, bool isAdmin)
        {
            var tableJson = _cache11.GetOrCreate("DB", entry => "");
            GlobalDataTable globalDataTable = GlobalDataTable.DeserializeFromJson(tableJson);

            DataTableData dataTableData = globalDataTable.DataTableData;
            dataTableData.Rows.Add(new RowData { ID = identification, Name = firstname, lastname = lastname, email = email, password = password, gender = gender, age = age, isAdmin = isAdmin });

            var tJson = globalDataTable.SerializeToJson();
            _cache11.Set("DB", tJson);
        }
        //return/give data of a table,for acheck
        public DataTableData GetTable()
        {
            var tableJson = _cache11.GetOrCreate("DB", entry => "");
            GlobalDataTable globalDataTable = GlobalDataTable.DeserializeFromJson(tableJson);

            DataTableData dataTableData = globalDataTable.DataTableData;

            return dataTableData;
        }

        public bool CheckUser(string email11, string password)
        {
            DataTableData dataTableData = GetTable();

            for (var i = 0; i < dataTableData.Rows.Count; i++)
            {
                var rowData = dataTableData.Rows[i];
                if (rowData.email.Equals(email11) && rowData.password.Equals(password))
                {
                    return true;
                }
            }

            return false;
        }


        public  bool CheckAdmin(string email)
        {
            DataTableData dataTableData = GetTable();

            for (var i = 0; i < dataTableData.Rows.Count; i++)
            {
                var rowData = dataTableData.Rows[i];
                if (rowData.email.Equals(email))
                {
                    if (rowData.isAdmin == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            return false;
        }

    }
}
