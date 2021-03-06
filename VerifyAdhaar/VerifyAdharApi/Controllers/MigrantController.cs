﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using VerifyAdharApi.Filters;
using VerifyAdharApi.Models;
using VerifyAdharApi.Services;

namespace VerifyAdharApi.Controllers
{
    [Route("api/migrants")]
    [ApiController]
    public class MigrantController : ControllerBase
    {
        private readonly MigrantService _migrantService;

        public MigrantController(MigrantService migrantService)
        {
            _migrantService = migrantService;
        }

        [HttpGet]
        [Route("GetStatus")]
        public ActionResult<string> GetStatus()
        {
            return "Hello, Web Service is working.";
        }

        [HttpGet]
        public ActionResult<List<Migrant>> Get()
        {
            return _migrantService.Get();
        }

        [Route("{id}")]
        [HttpGet("{id:length(24)}", Name = "GetMigrant")]
        public ActionResult<Migrant> Get(string id)
        {
            var migrant = _migrantService.Get(id);

            if (migrant == null)
            {
                return NotFound();
            }

            return migrant;
        }

        [HttpPost]
        public ActionResult<bool> Create([FromBody] Migrant migrant)
        {
            try
            {
                AdhaarIdFilter af = new AdhaarIdFilter();
                if (!af.IsValidAadhaarNumber(migrant.AadharNumber))
                {
                    return false;
                }
                _migrantService.UpdateMigrantWithStateAndDistrict(out migrant, migrant);
                _migrantService.Create(migrant);

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        [Route("{id}")]
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Migrant migrantIn)
        {
            var book = _migrantService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _migrantService.Update(id, migrantIn);

            return NoContent();
        }

        [Route("{id}")]
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var migrant = _migrantService.Get(id);

            if (migrant == null)
            {
                return NotFound();
            }

            _migrantService.Remove(migrant.Id);

            return NoContent();
        }

        [Route("{pincode}")]
        [HttpGet()]
        public ActionResult<Coordinates> Get(long pincode)
        {
            var data = _migrantService.GetLatitudeLongitudeWithMigrantsCount(pincode);

            if (data == null)
            {
                return NotFound();
            }

            return data;
        }

        [Route("statewise/{state}")]
        [HttpGet()]
        public ActionResult<List<Migrant>> GetMigrantsDataStateWise(string state)
        {
            var data = _migrantService.GetStateWiseData(state);

            if (data == null)
            {
                return NotFound();
            }

            return data;
        }
    }
}