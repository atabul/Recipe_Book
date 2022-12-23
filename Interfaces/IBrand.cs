namespace InventoryBeginners.Interfaces
{
    public interface IBrand
    {
        PaginatedList<Brand> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5); //read all
        Brand GetItem(int id); // read particular item

        Brand Create(Brand unit);

        Brand Edit(Brand unit);

        Brand Delete(Brand unit);

        public bool IsItemExists(string name);
        public bool IsItemExists(string name, int Id);


    }
}


