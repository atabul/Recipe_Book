namespace InventoryBeginners.Controllers;

    [Authorize]
    public class CurrencyController : Controller
    {

        private readonly ICurrency _Repo;
        public CurrencyController(ICurrency repo) // here the repository will be passed by the dependency injection.
        {
            _Repo = repo;
        }


        public IActionResult Index(string sortExpression = "", string SearchText = "", int pg = 1, int pageSize = 5)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("name");
            sortModel.AddColumn("description");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;

            ViewBag.SearchText = SearchText;

            PaginatedList<Currency> items = _Repo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);


            var pager = new PagerModel(items.TotalRecords, pg, pageSize);
            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;


            TempData["CurrentPage"] = pg;
            return View(items);
        }


        public IActionResult Create()
        {
            Currency item = new Currency();
        ViewBag.ExchangeCurrencyId = GetCurrencyList();

            return View(item);
        }

        [HttpPost]
        public IActionResult Create(Currency item)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                bolret = _Repo.Create(item);                                                  
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }


            if (bolret == false)
            {
                errMessage = errMessage + " " + _Repo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage);
                return View(item);
            }
            else
            {
                TempData["SuccessMessage"] = "" + item.Name + " Created Successfully";
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Details(int id) //Read
        {
            Currency item = _Repo.GetItem(id);
        ViewBag.ExchangeCurrencyId = GetCurrencyList();
        return View(item);
        }


        public IActionResult Edit(int id)
        {
            Currency item = _Repo.GetItem(id);
        ViewBag.ExchangeCurrencyId = GetCurrencyList();
        TempData.Keep();
            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(Currency item)
        {
            bool bolret = false;
            string errMessage = "";

            try
            {
            bolret = _Repo.Edit(item);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }


            int currentPage = 1;
            if (TempData["CurrentPage"] != null)
                currentPage = (int)TempData["CurrentPage"];


            if (bolret == false)
            {
                errMessage = errMessage + " " + _Repo.GetErrors();
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage);
                return View(item);
            }
            else
                return RedirectToAction(nameof(Index), new { pg = currentPage });
        }

        public IActionResult Delete(int id)
        {
            Currency item = _Repo.GetItem(id);
        ViewBag.ExchangeCurrencyId = GetCurrencyList();
        TempData.Keep();
            return View(item);
        }


        [HttpPost]
        public IActionResult Delete(Currency item)
        {
            bool bolret = false;
            string errMessage = "";
                try
                {
                bolret = _Repo.Delete(item);
                }
                catch (Exception ex)
                {
                errMessage = ex.Message;
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage);
                return View(item);
                }

                int currentPage = 1;
                if (TempData["CurrentPage"] != null)
                    currentPage = (int)TempData["CurrentPage"];

            
            if (bolret == false)
            {
                errMessage = errMessage + " " + _Repo.GetErrors();
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage);
                return View(item);
            }
            else
            {
                TempData["SuccessMessage"] = item.Name + " Deleted Successfully";
                return RedirectToAction(nameof(Index), new { pg = currentPage });
            }
        }



    private List<SelectListItem> GetCurrencyList()
    {
        var lstItems = new List<SelectListItem>();

        PaginatedList<Currency> items = _Repo.GetItems("Name", SortOrder.Ascending, "", 1, 1000);
        lstItems = items.Select(ut => new SelectListItem()
        {
            Value = ut.Id.ToString(),
            Text = ut.Name
        }).ToList();

        var defItem = new SelectListItem()
        {
            Value = "",
            Text = "----Select Exchange Currency ----"
        };

        lstItems.Insert(0, defItem);

        return lstItems;
    }

}


