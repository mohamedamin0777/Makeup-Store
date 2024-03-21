using AutoMapper;
using MakeupStore.BLL.Interfaces;
using MakeupStore.DAL.Entities;
using MakeupStore.PL.Helper;
using MakeupStore.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MakeupStore.PL.Controllers
{
    [Authorize(Roles = "Admin , Normal User")]
    public class ProductController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;
        private readonly IGenericRepository<ProductCategory> _categoryRepo;
        private readonly IGenericRepository<ProductBrand> _brandRepo;
        public ProductController(IMapper mapper, IProductRepository repository, IGenericRepository<ProductCategory> categoryRepo, IGenericRepository<ProductBrand> brandRepo)
        {
            _mapper = mapper;
            _repository = repository;
            _categoryRepo = categoryRepo;
            _brandRepo = brandRepo;
        }
        public IActionResult Index(string SearchValue = "")
        {
            IEnumerable<Product> products;
            IEnumerable<ProductViewModel> mappedProducts;
            if (string.IsNullOrEmpty(SearchValue))
            {
                products = _repository.GetAll();
                mappedProducts = _mapper.Map<IEnumerable<ProductViewModel>>(products);
            }
            else
            {
                products = _repository.Search(SearchValue);
                mappedProducts = _mapper.Map<IEnumerable<ProductViewModel>>(products);
            }

            return View(mappedProducts);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AddProduct()
        {
            ViewBag.categories = _categoryRepo.GetAll();
            ViewBag.brands = _brandRepo.GetAll();

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddProduct(ProductViewModel productVM) 
        {
                ViewBag.categories = _categoryRepo.GetAll();
                ViewBag.brands = _brandRepo.GetAll();
            if(ModelState.IsValid)
            {
                try
                {
                    var product = _mapper.Map<Product>(productVM);
                    product.PictureUrl = DocumentSettings.UploadFile(productVM.Image, "Images");

                    _repository.Add(product);
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }
            return View(productVM);
            
        }
        public IActionResult Details(int? id)
        {
            try
            {
                if (id is null)
                    return NotFound();

                var product = _repository.GetById(id);
                ViewBag.categories = _categoryRepo.GetAll();
                ViewBag.brands = _brandRepo.GetAll();

                if (product is null)
                    return NotFound();

                var productVM = _mapper.Map<ProductViewModel>(product);

                return View(productVM);
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
        [Authorize(Roles = "Admin")]

        public IActionResult Update(int? id)
        {
            if (id is null)
                return RedirectToAction("Error", "Home");

            var product = _repository.GetById(id);
            ViewBag.categories = _categoryRepo.GetAll();
            ViewBag.brands = _brandRepo.GetAll();

            if (product is null)
                return RedirectToAction("Error", "Home");

            var productVM = _mapper.Map<ProductViewModel>(product);

            return View(productVM);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Update(int id, ProductViewModel productVM)
        {
            if (id != productVM.Id)
                return RedirectToAction("Error", "Home");

            try
            {
                if (ModelState.IsValid)
                {
                    var product = _mapper.Map<Product>(productVM);
                    if (productVM.Image != null)
                        product.PictureUrl = DocumentSettings.UploadFile(productVM.Image, "Images");


                    _repository.Update(product);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return View(productVM);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return NotFound();

            var product = _repository.GetById(id);

            if (product is null)
                return RedirectToAction("Error", "Home");

            _repository.Delete(product);

            //var productVM = _mapper.Map<ProductViewModel>(product);

            return RedirectToAction("Index");
        }
    }
}
