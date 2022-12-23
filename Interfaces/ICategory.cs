
namespace InventoryBeginners.Interfaces
{
    public interface ICategory
    {
        PaginatedList<Category> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5); //read all
        Category GetItem(int id); // read particular item

        Category Create(Category unit);

        Category Edit(Category unit);

        Category Delete(Category unit);

        public bool IsItemExists(string name);
        public bool IsItemExists(string name, int Id);


    }
}

