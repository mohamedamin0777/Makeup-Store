using AutoMapper;
using MakeupStore.BLL.Interfaces;
using MakeupStore.DAL.Entities;
using MakeupStore.PL.Models;
using Microsoft.AspNetCore.Mvc;

namespace MakeupStore.PL.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            IEnumerable<ProductCategory> categories;
            IEnumerable<ProductCategoryViewModel> categoriesVM;

            categories = _categoryRepo.GetAll();
            categoriesVM = _mapper.Map<IEnumerable<ProductCategoryViewModel>>(categories);

            return View(categoriesVM);
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(ProductCategoryViewModel categoryVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var checkedCategory = _categoryRepo.GetByName(categoryVM.Name);

                    if(checkedCategory is null)
                    {
                        var category = _mapper.Map<ProductCategory>(categoryVM);
                        _categoryRepo.Add(category);
                        TempData["Message"] = "Category Created Successfully";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Message"] = "Category Already Exist!!";
                        return RedirectToAction("Index");
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return View(categoryVM);
        }

        public IActionResult Details(int? id)
        {
            try
            {
                if (id is null)
                    return RedirectToAction("Error", "Home");

                var category = _categoryRepo.GetById(id);
                var mappedCategory = _mapper.Map<ProductCategoryViewModel>(category);
                if (category is null)
                    return RedirectToAction("Error", "Home");

                return View(mappedCategory);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IActionResult Update(int? id)
        {
            if (id is null)
                return RedirectToAction("Error", "Home");

            var category = _categoryRepo.GetById(id);
            var mappedCategory = _mapper.Map<ProductCategoryViewModel>(category);
            if (category is null)
                return RedirectToAction("Error", "Home");

            return View(mappedCategory);
        }

        [HttpPost]
        public IActionResult Update(int id, ProductCategoryViewModel categoryVM)
        {
            if (id != categoryVM.Id)
                return RedirectToAction("Error", "Home");

            try
            {
                if (ModelState.IsValid)
                {
                    var category = _mapper.Map<ProductCategory>(categoryVM);
                    _categoryRepo.Update(category);
                    return RedirectToAction("Index");
                }
                else
                    return View(categoryVM);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IActionResult Delete(int? id)
        {
            if (id is null)
                return RedirectToAction("Error", "Home");

            var category = _categoryRepo.GetById(id);
            var mappedCategory = _mapper.Map<ProductCategoryViewModel>(category);

            if (mappedCategory is null)
                return RedirectToAction("Error", "Home");

            _categoryRepo.Delete(category);
            return RedirectToAction("Index");
        }
    }
}
