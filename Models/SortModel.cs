namespace InventoryBeginners.Models
{
    public enum SortOrder { Ascending = 0, Descending = 1 }
    public class SortModel
    {

        private string UpIcon = "fa fa-arrow-up";
        private string DownIcon = "fa fa-arrow-down";

        public string SortedProperty { get; set; }
        public SortOrder SortedOrder { get; set; }

        public string SortedExpression { get; private set; }

        private List<SortableColumn> sortableColumns = new List<SortableColumn>();

        public void AddColumn(string colname, bool IsDefaultColumn = false)
        {
            SortableColumn tmp = this.sortableColumns.Where(c => c.ColumnName.ToLower() == colname.ToLower()).SingleOrDefault();
            if (tmp == null)
                sortableColumns.Add(new SortableColumn() { ColumnName = colname });

            if (IsDefaultColumn == true || sortableColumns.Count == 1)
            {
                SortedProperty = colname;
                SortedOrder = SortOrder.Ascending;
            }

        }
        public SortableColumn GetColumn(string colname)
        {
            SortableColumn tmp = this.sortableColumns.Where(c => c.ColumnName.ToLower() == colname.ToLower()).SingleOrDefault();
            if (tmp == null)
                sortableColumns.Add(new SortableColumn() { ColumnName = colname });
            return tmp;
        }



        public void ApplySort(string sortExpression)
        {

            if (sortExpression == null)
                sortExpression = "";

            if (sortExpression == "")
                sortExpression = this.SortedProperty;

            sortExpression = sortExpression.ToLower();
            SortedExpression = sortExpression;


            foreach (SortableColumn sortableColumn in this.sortableColumns)
            {
                sortableColumn.SortIcon = "";
                sortableColumn.SortExpression = sortableColumn.ColumnName;

                if (sortExpression == sortableColumn.ColumnName.ToLower())
                {
                    this.SortedOrder = SortOrder.Ascending;
                    this.SortedProperty = sortableColumn.ColumnName;
                    sortableColumn.SortIcon = DownIcon;
                    sortableColumn.SortExpression = sortableColumn.ColumnName + "_desc";
                }
                if (sortExpression == sortableColumn.ColumnName.ToLower() + "_desc")
                {
                    this.SortedOrder = SortOrder.Descending;
                    this.SortedProperty = sortableColumn.ColumnName;
                    sortableColumn.SortIcon = UpIcon;
                    sortableColumn.SortExpression = sortableColumn.ColumnName;
                }
            }
        }


        public class SortableColumn
        {
            public string ColumnName { get; set; }
            public string SortExpression { get; set; }
            public string SortIcon { get; set; }
        }

    }
}
