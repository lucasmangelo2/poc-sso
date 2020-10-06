using AutoMapper;
using IdentityServer.SSO.Infra.Atributtes;
using IdentityServer.SSO.ViewModel;
using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.SSO.Controllers
{

    [Authorize(Roles = "Admin")]
    [Route("client")]
    [SecurityHeaders]
    public class ClientController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IClientStore _clientStore;
        private readonly ConfigurationDbContext _dbContext;

        public ClientController(
            IMapper mapper,
            IClientStore clientStore,
            ConfigurationDbContext dbContext)
        {
            _mapper = mapper;
            _clientStore = clientStore;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Client> clients = _dbContext.Clients.AsNoTracking().ToList();

            List<IdentityServer4.Models.Client> auxList = new List<IdentityServer4.Models.Client>();

            foreach (var item in clients)
            {
                var client = await _clientStore.FindClientByIdAsync(item.ClientId);

                auxList.Add(client);
            }

            BaseListViewModel<ClientViewModel> vm = new BaseListViewModel<ClientViewModel>()
            {
                Data = _mapper.Map<List<ClientViewModel>>(auxList)
            };

            return View(vm);
        }

        [HttpGet]
        [Route("insert")]
        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        [Route("insert")]
        public async Task<IActionResult> Insert(ClientViewModel model)
        {
            bool clientExists = await _clientStore.FindClientByIdAsync(model.ClientId) != null;

            if (!ModelState.IsValid || clientExists)
            {
                if (clientExists)
                {
                    ModelState.AddModelError("ClientId", "Identificador da Aplicação já cadastrada");
                }

                return View(model);
            }

            model.AllowedScopes = new List<string>()
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.Email
            };

            var client = _mapper.Map<IdentityServer4.Models.Client>(model);

            await _dbContext.Clients.AddAsync(client.ToEntity());

            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Client");
        }

        [HttpGet]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(string id)
        {
            var application = await _clientStore.FindClientByIdAsync(id);

            if (application != null)
            {
                var viewModel = _mapper.Map<ClientViewModel>(application);

                return View(viewModel);
            }

            return RedirectToAction("Index", "Client");
        }

        [HttpPost]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(string id, ClientViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var client = await _clientStore.FindClientByIdAsync(id);

            if (client != null)
            {
                var entity = _mapper.Map<IdentityServer4.Models.Client>(model).ToEntity();
                entity.Id = _dbContext.Clients.AsNoTracking().FirstOrDefault(x => x.ClientId == entity.ClientId).Id;

                _dbContext.Clients.Update(entity);
                _dbContext.SaveChanges();
            }

            return RedirectToAction("Index", "Client");
        }

        [HttpGet]
        [Route("delete/{clientId}")]
        public async Task<IActionResult> Delete(string clientId)
        {
            var client = await _clientStore.FindClientByIdAsync(clientId);

            if (client != null)
            {
                var entity = client.ToEntity();
                entity.Id = _dbContext.Clients.AsNoTracking().FirstOrDefault(x => x.ClientId == entity.ClientId).Id;
                _dbContext.Clients.Remove(entity);
                _dbContext.SaveChanges();
            }

            return RedirectToAction("Index", "Client");
        }
    }
}