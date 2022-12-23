

namespace InventoryBeginners.Interfaces
{
    public interface IProductGroup
    { 
        PaginatedList<ProductGroup> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5); //read all
        ProductGroup GetItem(int id); // read particular item

        ProductGroup Create(ProductGroup unit);

        ProductGroup Edit(ProductGroup unit);

        ProductGroup Delete(ProductGroup unit);

        public bool IsItemExists(string name);
        public bool IsItemExists(string name, int Id);
    }
}


