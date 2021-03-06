﻿using System;
using Cw5.DTOs.Requests;
using Cw5.Models;
using Cw5.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cw5.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsDbService _dbService;

        public StudentsController(IStudentsDbService iStudentsDbService)
        {
            _dbService = iStudentsDbService;
        }


        [HttpGet]
        public IActionResult GetStudent()
        {
            return Ok(_dbService.GetStudents());
        }


        [HttpGet("secured/{id}")]
        public IActionResult GetStudent(string id)
        {
            if (_dbService.GetStudent(id) != null)
            {
                return Ok(_dbService.GetStudent(id));
            }

            return NotFound("Nie znaleziono studenta o tym id");
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateStudent([FromBody] UpdateStudentRequest request, string id)
        {
            if (_dbService.GetStudent(id) != null)
            {
                _dbService.UpdateStudent(request);
                return Ok("Aktualizacja zakończona");
            }
            else
            {
                return NotFound("Nie znaleziono studenta o tym id");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(string id)
        {
            if (_dbService.GetStudent(id) != null)
            {
                _dbService.DeleteStudent(id);
                return Ok("Usuwanie zakończone");
            }
            return NotFound("Nie znaleziono studenta o tym id");
        }
    }
}