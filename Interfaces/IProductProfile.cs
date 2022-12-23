
namespace InventoryBeginners.Interfaces
{
    public interface IProductProfile
    {
        PaginatedList<ProductProfile> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5); //read all
        ProductProfile GetItem(int id); // read particular item

        ProductProfile Create(ProductProfile unit);

        ProductProfile Edit(ProductProfile unit);

        ProductProfile Delete(ProductProfile unit);

        public bool IsItemExists(string name);
        public bool IsItemExists(string name, int Id);


    }
}


