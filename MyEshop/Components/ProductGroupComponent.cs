using Microsoft.AspNetCore.Mvc;
using MyEshop.Data;
using MyEshop.Data.Repositories;
using MyEshop.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MyEshop.Components
{
    public class ProductGroupComponent : ViewComponent
    {
     
      private  IGroupRepository _groupRepository;

        public ProductGroupComponent( IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;   
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View("/views/Components/ProductGroupeComponent.cshtml" , _groupRepository.GetGroupForShow() );
        }
    }
}
