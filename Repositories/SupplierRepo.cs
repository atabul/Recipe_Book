

namespace InventoryBeginners.Repositories
{
    public class SupplierRepo : ISupplier
    {
        private readonly InventoryContext _context;
        public SupplierRepo(InventoryContext context) // will be passed by dependency injection.
        {
            _context = context;
        }

        public Supplier Create(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            _context.SaveChanges();
            return supplier;
        }

        public Supplier Delete(Supplier supplier)
        {
            _context.Suppliers.Attach(supplier);
            _context.Entry(supplier).State = EntityState.Deleted;
            _context.SaveChanges();
            return supplier;
        }

        public Supplier Edit(Supplier supplier)
        {
            _context.Suppliers.Attach(supplier);
            _context.Entry(supplier).State = EntityState.Modified;
            _context.SaveChanges();
            return supplier;
        }

        public Supplier GetItem(int id)
        {
            Supplier supplier = _context.Suppliers.Where(s => s.Id == id).FirstOrDefault();
            return supplier;
        }

        private List<Supplier> DoSort(List<Supplier> suppliers, string SortProperty, SortOrder sortOrder)
        {
            if (SortProperty.ToLower() == "name")
            {
                if (sortOrder == SortOrder.Ascending)
                    suppliers = suppliers.OrderBy(n => n.Name).ToList();
                else
                    suppliers = suppliers.OrderByDescending(n => n.Name).ToList();
            }
            else
            {
                if (sortOrder == SortOrder.Ascending)
                    suppliers = suppliers.OrderBy(d => d.Code).ToList();
                else
                    suppliers = suppliers.OrderByDescending(d => d.Code).ToList();
            }

            return suppliers;
        }

        public PaginatedList<Supplier> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {

            List<Supplier> suppliers;

            if (SearchText != "" && SearchText != null)
            {
                suppliers = _context.Suppliers.Where(n => n.Name.Contains(SearchText) || n.Code.Contains(SearchText))
                    .ToList();
            }
            else
                suppliers = _context.Suppliers.ToList();

            suppliers = DoSort(suppliers, SortProperty, sortOrder);

            PaginatedList<Supplier> retsuppliers = new PaginatedList<Supplier>(suppliers, pageIndex, pageSize);

            return retsuppliers;

        }

        public bool IsSupplierCodeExists(string code)
        {
            
            int ct = _context.Suppliers.Where(s => s.Code.ToLower() == code.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;

        }
        public bool IsSupplierCodeExists(string code, int Id)
        {
            int ct = _context.Suppliers.Where(s => s.Code.ToLower() == code.ToLower() &&  s.Id!=Id).Count();
            if (ct > 0)
                return true;
            else
                return false;

        }
        public bool IsSupplierEmailExists(string email)
        {
            int ct = _context.Suppliers.Where(s => s.EmailId.ToLower() == email.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;

        }
        public bool IsSupplierEmailExists(string email, int Id)
        {
            int ct = _context.Suppliers.Where(s => s.EmailId.ToLower() == email.ToLower() && s.Id != Id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
        public bool IsSupplierNameExists(string name)
        {
            int ct = _context.Suppliers.Where(s => s.Name.ToLower() == name.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;

        }
        public bool IsSupplierNameExists(string name, int Id)
        {
            int ct = _context.Suppliers.Where(s => s.Name.ToLower() == name.ToLower() && s.Id != Id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
