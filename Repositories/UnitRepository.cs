
namespace InventoryBeginners.Repositories
{
    public class UnitRepository : IUnit
    {
        private readonly InventoryContext _context; // for connecting to efcore.
        public UnitRepository(InventoryContext context) // will be passed by dependency injection.
        {
            _context = context;
        }
        public Unit Create(Unit unit)
        {
            _context.Units.Add(unit);
            _context.SaveChanges();
            return unit;
        }

        public Unit Delete(Unit unit)
        {
            _context.Units.Attach(unit);
            _context.Entry(unit).State = EntityState.Deleted;
            _context.SaveChanges();
            return unit;
        }

        public Unit Edit(Unit unit)
        {
            _context.Units.Attach(unit);
            _context.Entry(unit).State = EntityState.Modified;
            _context.SaveChanges();
            return unit;
        }


        private List<Unit> DoSort(List<Unit> units, string SortProperty, SortOrder sortOrder)
        {           

            if (SortProperty.ToLower() == "name")
            {
                if (sortOrder == SortOrder.Ascending)
                    units = units.OrderBy(n => n.Name).ToList();
                else
                    units = units.OrderByDescending(n => n.Name).ToList();
            }
            else
            {
                if (sortOrder == SortOrder.Ascending)
                    units = units.OrderBy(d => d.Description).ToList();
                else
                    units = units.OrderByDescending(d => d.Description).ToList();
            }

            return units;
        }

        public PaginatedList<Unit> GetItems(string SortProperty, SortOrder sortOrder,string SearchText="",int pageIndex=1,int pageSize=5)
        {
            List<Unit> units;

            if (SearchText != "" && SearchText!=null)
            {
                units = _context.Units.Where(n => n.Name.Contains(SearchText) || n.Description.Contains(SearchText))
                    .ToList();            
            }
            else
                units= _context.Units.ToList();

            units = DoSort(units,SortProperty,sortOrder);
            
            PaginatedList<Unit> retUnits = new PaginatedList<Unit>(units, pageIndex, pageSize);

            return retUnits;
        }

        public Unit GetUnit(int id)
        {
            Unit unit = _context.Units.Where(u => u.Id == id).FirstOrDefault();
            return unit;
        }
        public bool IsUnitNameExists(string name)
        {
            int ct = _context.Units.Where(n => n.Name.ToLower() == name.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;      
        }

        public bool IsUnitNameExists(string name,int Id)
        {
            int ct = _context.Units.Where(n => n.Name.ToLower() == name.ToLower() && n.Id!=Id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

    }
}
