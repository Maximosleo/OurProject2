using System.Collections.Generic;
using System.Text.Json;

namespace OurProject2.Pages
{
    public class DataTableData
    {
        public List<RowData> Rows { get; set; }
    }

    public class RowData
    {
        public string ID { get; set; }
        public string Name { get; set; }

        public string lastname { get; set; }

        public string email { get; set; }

        public string password { get; set; }
        

        public string gender { get; set; }

        public string age { get; set; }

        public string admin_user { get; set; }
    }

    public class GlobalDataTable
    {
        private static readonly GlobalDataTable instance = new GlobalDataTable();

        private DataTableData dataTableData;

        private GlobalDataTable()
        {
            dataTableData = new DataTableData
            {
                Rows = new List<RowData>()
            };
        }

        public static GlobalDataTable Instance
        {
            get { return instance; }
        }

        public DataTableData DataTableData
        {
            get { return dataTableData; }
        }

        public string SerializeToJson()
        {
            return JsonSerializer.Serialize(this.dataTableData);
        }

        public static GlobalDataTable DeserializeFromJson(string json)
        {
            try
            {
                DataTableData dataTableData = JsonSerializer.Deserialize<DataTableData>(json);
                GlobalDataTable globalDataTable = new GlobalDataTable
                {
                    dataTableData = dataTableData
                };
                return globalDataTable;
            }
            catch (Exception ex)
            {
                return instance;
            }
        }
    }
}
