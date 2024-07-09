﻿using MyEshop.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyEshop.Data.Repositories
{
    public interface IGroupRepository
    {
        IEnumerable<Category> GetAllCategori();
        IEnumerable<ShowGroupViewModel> GetGroupForShow();

    }
    public class GroupRepository : IGroupRepository
    {
        private MyEshopContext _context;

        public GroupRepository( MyEshopContext context)
        {
                
            _context = context;
        }
        public IEnumerable<Category> GetAllCategori()
        {
            return _context.Categories;     
         }

        public IEnumerable<ShowGroupViewModel> GetGroupForShow()
        {
          return  _context.Categories
                            .Select(c => new ShowGroupViewModel()
                            {
                                GroupId = c.Id,
                                Name = c.Name,
                                ProductCount = _context.CategoryToProducts.Count(g => g.CategoryId == c.Id),
                            }).ToList();
         }
    }
}
