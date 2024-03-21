using AutoMapper;
using MakeupStore.BLL.Interfaces;
using MakeupStore.DAL.Entities;
using MakeupStore.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MakeupStore.PL.Controllers
{
    [Authorize(Roles = "Admin")]

    public class BrandController : Controller
    {
        private readonly IBrandRepository _brandRepo;
        private readonly IMapper _mapper;

        public BrandController(IBrandRepository brandRepo, IMapper mapper)
        {
            _brandRepo = brandRepo;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            IEnumerable<ProductBrand> brands;
            IEnumerable<BrandViewModel> brandsVM;

            brands = _brandRepo.GetAll();
            brandsVM = _mapper.Map<IEnumerable<BrandViewModel>>(brands);

            return View(brandsVM);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BrandViewModel brandVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var checkedBrand = _brandRepo.GetByName(brandVM.Name);

                    if (checkedBrand is null)
                    {
                        var brand = _mapper.Map<ProductBrand>(brandVM);
                        _brandRepo.Add(brand);
                        TempData["Message"] = "Brand Created Successfully";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Message"] = "Brand Already Exist!!";
                        return RedirectToAction("Index");
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return View(brandVM);
        }

        public IActionResult Details(int? id)
        {
            try
            {
                if (id is null)
                    return RedirectToAction("Error", "Home");

                var brand = _brandRepo.GetById(id);
                var mappedBrand = _mapper.Map<BrandViewModel>(brand);
                if (brand is null)
                    return RedirectToAction("Error", "Home");

                return View(mappedBrand);
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

            var brand = _brandRepo.GetById(id);
            var mappedBrand = _mapper.Map<BrandViewModel>(brand);
            if (brand is null)
                return RedirectToAction("Error", "Home");

            return View(mappedBrand);
        }

        [HttpPost]
        public IActionResult Update(int id, BrandViewModel brandVM)
        {
            if (id != brandVM.Id)
                return RedirectToAction("Error", "Home");

            try
            {
                if (ModelState.IsValid)
                {
                    var brand = _mapper.Map<ProductBrand>(brandVM);
                    _brandRepo.Update(brand);
                    return RedirectToAction("Index");
                }
                else
                    return View(brandVM);
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

            var brand = _brandRepo.GetById(id);
            var mappedBrand = _mapper.Map<BrandViewModel>(brand);

            if (mappedBrand is null)
                return RedirectToAction("Error", "Home");

            _brandRepo.Delete(brand);
            return RedirectToAction("Index");
        }
    }
}
