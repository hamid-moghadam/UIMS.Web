﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIMS.Web.Data;
using UIMS.Web.Models;
using UIMS.Web.Models.Interfaces;
using UIMS.Web.DTO;
using UIMS.Web.Extentions;
using System.Linq.Expressions;

namespace UIMS.Web.Services
{
    public abstract class BaseService<TModel, TInsertModel, TUpdateModel, TViewModel> : BaseServiceProvider<TModel>, IBaseService<TModel, TInsertModel, TUpdateModel, TViewModel>
        where TModel : class, IKey<int>,ITracker
        where TViewModel : BaseModel
        where TUpdateModel : BaseModel
    {
        public BaseService(DataContext context, IMapper mapper) : base(context, mapper)
        {
            Entity = context.Set<TModel>();
        }


        public virtual async Task<TViewModel> GetAsync(int id) => await Entity.ProjectTo<TViewModel>().SingleOrDefaultAsync(x => x.Id == id);

        public virtual async Task<TCustomViewModel> GetAsync<TCustomViewModel>(int id) where TCustomViewModel:IKey<int> => await Entity.ProjectTo<TCustomViewModel>().SingleOrDefaultAsync(x => x.Id == id);

        public virtual async Task<TModel> GetAsync(Expression<Func<TModel, bool>> expression) => await Entity.SingleOrDefaultAsync(expression);
        public virtual TModel Get(Expression<Func<TModel, bool>> expression) => Entity.SingleOrDefault(expression);

        //public async Task<TModel> GetAsync(int id) => await Entity.SingleOrDefaultAsync(x => x.Id == id);

        public IEnumerable<TViewModel> GetAll()
        {
            return Entity.OrderByDescending(x => x.Created).ProjectTo<TViewModel>().AsEnumerable();
        }

        public async virtual Task<PaginationViewModel<TViewModel>> GetAllAsync(int page, int pageSize)
        {
            return await Entity.OrderByDescending(x=>x.Created).ProjectTo<TViewModel>().ToPageAsync(pageSize, page);
        }

        

        public async virtual Task<PaginationViewModel<TViewModel>> GetAllAsync(string[] filters ,int page, int pageSize)
        {
            return await Entity.Where(GetFilters(filters)).OrderByDescending(x => x.Created).ProjectTo<TViewModel>().ToPageAsync(pageSize, page);
        }

        public async virtual Task<PaginationViewModel<TViewModel>> GetAllAsync(FilterInputViewModel filterInputVM)
        {
            return await SearchQuery(filterInputVM.SearchQuery).Where(GetFilters(filterInputVM.Filters)).OrderByDescending(x => x.Created).ProjectTo<TViewModel>().ToPageAsync(filterInputVM.PageSize, filterInputVM.Page);
        }

        public async virtual Task<PaginationViewModel<TCustomViewModel>> GetAll<TCustomViewModel>(int page, int pageSize)
        {
            return await Entity.OrderByDescending(x => x.Created).ProjectTo<TCustomViewModel>().ToPageAsync(pageSize, page);
        }

        public async Task<bool> IsExistsAsync(Expression<Func<TModel, bool>> expression)
        {
            return await Entity.AnyAsync(expression);
            //return true;
        }
       

        public virtual async Task<TModel> AddAsync(TInsertModel model)
        {
            TModel baseModel = _mapper.Map<TModel>(model);
            return (await Entity.AddAsync(baseModel)).Entity;

        }

        public virtual TModel Update(TModel model)
        {
            return Entity.Update(model).Entity;
        }

    }


    
}
