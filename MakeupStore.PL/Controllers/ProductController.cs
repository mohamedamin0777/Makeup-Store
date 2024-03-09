using AutoMapper;
using MakeupStore.BLL.Interfaces;
using MakeupStore.DAL.Entities;
using MakeupStore.PL.Helper;
using MakeupStore.PL.Models;
using Microsoft.AspNetCore.Mvc;

namespace MakeupStore.PL.Controllers
{
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

        public IActionResult AddProduct()
        {
            ViewBag.categories = _categoryRepo.GetAll();
            ViewBag.brands = _brandRepo.GetAll();

            return View();
        }
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
    }
}
