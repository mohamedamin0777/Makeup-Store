using AutoMapper;
using MakeupStore.BLL.Interfaces;
using MakeupStore.DAL.Entities;
using MakeupStore.PL.Models;
using Microsoft.AspNetCore.Mvc;

namespace MakeupStore.PL.Controllers
{
    public class OrderController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IGenericRepository<ProductCategory> _categoryRepo;
        private readonly IGenericRepository<ProductBrand> _brandRepo;

        public OrderController(IMapper mapper, IGenericRepository<Order> order, IProductRepository productRepository, IGenericRepository<ProductCategory> categoryRepo, IGenericRepository<ProductBrand> brandRepo)
        {
            _mapper = mapper;
            _orderRepository = order;
            _productRepository = productRepository;
            _categoryRepo = categoryRepo;
            _brandRepo = brandRepo;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddOrder(int? id)
        {
            if (id is null)
                return RedirectToAction("Error", "Home");

            var product = _productRepository.GetById(id);
            ViewBag.categories = _categoryRepo.GetAll();
            ViewBag.brands = _brandRepo.GetAll();

            if (product is null)
                return RedirectToAction("Error", "Home");

            return View();
        }

        [HttpPost]
        public IActionResult AddOrder(OrderViewModel orderVM)
        {
            if(ModelState.IsValid)
            {
                orderVM.SubTotal = ((_productRepository.GetById(orderVM.ProductId)).Price)*((orderVM.Quantity));
                var order = _mapper.Map<Order>(orderVM);
                _orderRepository.Add(order);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id is null)
                return NotFound();

            var order = _orderRepository.GetById(id);

            if (order is null)
                return RedirectToAction("Error", "Home");

            _orderRepository.Delete(order);

            //var orderVM = _mapper.Map<OrderViewModel>(order);

            return RedirectToAction("Index");
        }
    }
}
