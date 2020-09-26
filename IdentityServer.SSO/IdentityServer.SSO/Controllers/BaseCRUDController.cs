using AutoMapper;
using IdentityServer.SSO.Business.Interfaces;
using IdentityServer.SSO.Infra.Atributtes;
using IdentityServer.SSO.Model;
using IdentityServer.SSO.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServer.SSO.Controllers
{
    [Authorize]
    [SecurityHeaders]
    public class BaseCRUDController<TBusiness, TModel, TViewModel> : Controller
        where TBusiness : IBusiness<TModel>
        where TModel : BaseModel
        where TViewModel : class
    {
        private readonly IMapper _mapper;
        private readonly TBusiness _business;

        public BaseCRUDController(IMapper mapper, TBusiness business)
        {
            _mapper = mapper;
            _business = business;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _business.GetAllAsync();

            BaseListViewModel<TViewModel> vm = new BaseListViewModel<TViewModel>()
            {
                Data = _mapper.Map<List<TViewModel>>(list)
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(TViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var model = _mapper.Map<TModel>(viewModel);

                await _business.InsertAsync(model);

                return await Index();
            }

            return Insert();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _business.GetByIdAsync(id);

            if (model != null)
            {
                return View(_mapper.Map<TViewModel>(model));
            }

            return await Index();
        }

        [HttpPut]
        public async Task<IActionResult> Update(TViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var model = _mapper.Map<TModel>(viewModel);

                await _business.UpdateAsync(model);

                return await Index();
            }

            return await Index();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _business.DeleteAsync(id);

            return await Index();
        }
    }
}
