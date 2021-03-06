﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UIMS.Web.DTO;
using UIMS.Web.Services;
using AutoMapper;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace UIMS.Web.Controllers
{
    [Produces("application/json")]
    //[Route("api/Field")]
    public class FieldController : ApiController
    {
        private readonly FieldService _fieldService;

        private readonly DegreeService _degreeService;

        private readonly IMapper _mapper;

        public FieldController(IMapper mapper, FieldService fieldService, DegreeService degreeService)
        {
            _mapper = mapper;
            _degreeService = degreeService;
            _fieldService = fieldService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] FieldInsertViewModel fieldInsertVM)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _fieldService.IsExistsAsync(x=>x.Name == fieldInsertVM.Name && x.DegreeId == fieldInsertVM.DegreeId.Value))
            {
                ModelState.AddModelError("Errors", "این رشته قبلا در سیستم ثبت شده است.");
                return BadRequest(ModelState);
            }

            await _fieldService.AddAsync(fieldInsertVM);
            await _fieldService.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, typeof(FieldViewModel))]
        public async Task<IActionResult> Get(int id)
        {
            var field = await _fieldService.GetAsync(id);

            if (field == null)
                return NotFound();

            return Ok(field);
        }

        [HttpPost]
        [SwaggerResponse(200, typeof(PaginationViewModel<FieldViewModel>))]
        public async Task<IActionResult> GetAll([FromBody] FilterInputViewModel filterInputVM)
        {
            //return Ok(await _fieldService.GetAllAsync(filterInputVM.Filters,filterInputVM.Page,filterInputVM.PageSize));
            return Ok(await _fieldService.GetAllAsync(filterInputVM));
        }

        [HttpPost]
        [SwaggerResponse(200, typeof(PaginationViewModel<FieldViewModel>))]
        public async Task<IActionResult> Search([FromBody]SearchViewModel searchVM)
        {
            var results = await _fieldService.SearchAsync(searchVM.Text, searchVM.Page, searchVM.PageSize);
            return Ok(results);
        }


        [HttpPost("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var field = await _fieldService.GetAsync(x => x.Id == id);
            if (field == null)
                return NotFound();

            _fieldService.Remove(field);
            await _fieldService.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] FieldUpdateViewModel fieldUpdateVM)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (fieldUpdateVM.DegreeId.HasValue)
            {
                var isDegreeExists = await _degreeService.IsExistsAsync(x => x.Id == fieldUpdateVM.DegreeId.Value);
                if (!isDegreeExists)
                {
                    ModelState.AddModelError("Errors", "مقطع مورد نظر در سیستم ثبت نشده است.");
                    return BadRequest(ModelState);
                }
            }

            var field = await _fieldService.GetAsync(x => x.Id == fieldUpdateVM.Id);
            if (field == null)
                return NotFound();
            field = _mapper.Map(fieldUpdateVM, field);

            _fieldService.Update(field);
            await _fieldService.SaveChangesAsync();
            return Ok();
        }
    }
}