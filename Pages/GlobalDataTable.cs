using System.Data;

namespace OurProject2.Pages
{
    public class GlobalDataTable
    {
        private static readonly GlobalDataTable instance = new GlobalDataTable();

        private DataTable dataTable;

        private GlobalDataTable()
        {
            dataTable = new DataTable();
            dataTable.Columns.Add("ID", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));
        }

        public static GlobalDataTable Instance
        {
            get { return instance; }
        }

        public DataTable DataTable
        {
            get { return dataTable; }
        }
    }
}
