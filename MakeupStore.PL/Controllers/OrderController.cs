using AutoMapper;
using MakeupStore.BLL.Interfaces;
using MakeupStore.DAL.Entities;
using MakeupStore.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace MakeupStore.PL.Controllers
{
	[Authorize(Roles = "Admin , Normal User")]

	public class OrderController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IGenericRepository<ProductCategory> _categoryRepo;
        private readonly IGenericRepository<ProductBrand> _brandRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(IMapper mapper, UserManager<ApplicationUser> userManager, IGenericRepository<Order> order, IProductRepository productRepository, IGenericRepository<ProductCategory> categoryRepo, IGenericRepository<ProductBrand> brandRepo)
        {
            _mapper = mapper;
            _orderRepository = order;
            _productRepository = productRepository;
            _categoryRepo = categoryRepo;
            _brandRepo = brandRepo;
            _userManager = userManager;    
        }


        public IActionResult Index()
        {
            IEnumerable<Order> orders;
            IEnumerable<OrderDetailsViewModel> mappedOrders;
            string username = User.Identity.Name;
             orders = _orderRepository.GetAll().Where(o => o.Username == username);   
             mappedOrders = _mapper.Map<IEnumerable<OrderDetailsViewModel>>(orders);
            
            foreach (var order in mappedOrders)
            {
                order.ProductName = _productRepository.GetById(order.ProductId).Name;
            }

            return View(mappedOrders);
        }

        public IActionResult AddOrder(int? id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            var orderVM = new OrderViewModel
            {
                ProductId = product.Id,
                Quantity = 1,
                Username = User.Identity.Name,
            };

            return View(orderVM);
        }
        [HttpPost]
        public IActionResult AddOrder(OrderViewModel orderVM)
        {
            var productToOrder = _productRepository.GetById(orderVM.ProductId);

            if (productToOrder == null)
            {
                ModelState.AddModelError("ProductId", "Invalid ProductId");
            }

            if (ModelState.IsValid)
            {
                orderVM.SubTotal = productToOrder.Price * orderVM.Quantity;
                orderVM.TotalPrice = orderVM.SubTotal + orderVM.ShippingPrice;
                orderVM.Username = User.Identity.Name;
                orderVM.DeliveryDate = orderVM.OrderDate.AddDays(3);

                var order = _mapper.Map<Order>(orderVM);

                _orderRepository.Add(order);

                return RedirectToAction(nameof(Index));
            }

            return View(orderVM);
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
