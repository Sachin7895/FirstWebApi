using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApis.Models;

namespace WebApis.Controllers
{
    public class StudentController : ApiController
    {
        public StudentController(){

            }

        [HttpGet]

        public IHttpActionResult GetAllStudent()
        {
            IList<StudentViewModel> students = null;

            using (var listStudent = new SchoolDBEntities())
            {
                students = listStudent.Students.Include("StudentAddress").Select(s => new StudentViewModel()
                {
                    Id = s.StudentID,
                    StudentName = s.StudentName
                }).ToList<StudentViewModel>(); ;
            }
            if (students.Count == 0)
            {
                return NotFound();
            }


            return Ok(students);

        }

        [HttpGet]

            public IHttpActionResult GetAllStudentDetails(bool includeAddress = false)
            {

            IList<StudentViewModel> students = null;

            using (var listStudent =new SchoolDBEntities())
            {
                students = listStudent.Students.Include("StudentAddress").Select(s => new StudentViewModel()
                {
                    Id = s.StudentID,
                    StudentName = s.StudentName,
                    Address = s.StudentAddress == null  || includeAddress == false ? null : new AddressViewModel()
                    {
                        StudentId = s.StudentAddress.StudentID,
                        Address1 = s.StudentAddress.Address1,
                        Address2 = s.StudentAddress.Address2,
                        City = s.StudentAddress.City,
                        State=s.StudentAddress.State
                    }

                }).ToList<StudentViewModel>();

            }

            if (students.Count == 0)
            {
                return NotFound();
            }


            return Ok(students);
        }


        [HttpGet]

        public IHttpActionResult GetStudentById(int id)
        {
            StudentViewModel student = null;

            using (var listStudent = new SchoolDBEntities())
            {
                student = listStudent.Students.Include("StudentAddress").Where(s => s.StudentID == id)
                    .Select(s => new StudentViewModel()
                    {
                        Id = s.StudentID,
                        StudentName=s.StudentName
                    }).FirstOrDefault<StudentViewModel>();
            }

            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }


        [HttpGet]

        public IHttpActionResult GetStudentByName(string Name)
        {

            IList<StudentViewModel> student = null;

            using (var listStudent = new SchoolDBEntities())
            {
                student = listStudent.Students.Include("StudentAddress").Where(s => s.StudentName.ToLower() == Name.ToLower()).
                    Select(s => new StudentViewModel()
                    {
                        Id=s.StudentID,
                        StudentName=s.StudentName,
                        Address=s.StudentAddress==null?null:new AddressViewModel()
                        {
                            StudentId = s.StudentAddress.StudentID,
                            Address1 = s.StudentAddress.Address1,
                            Address2 = s.StudentAddress.Address2,
                            City = s.StudentAddress.City,
                            State = s.StudentAddress.State
                        }
                       
                    }).ToList<StudentViewModel>();
            }
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);

        }


        [HttpPost]
        public IHttpActionResult PostStudentDetails(StudentViewModel student)
        {
            if (!ModelState.IsValid)
            {


                return BadRequest("Invalid data.");
            }

            else
            {
                using (var ListStudent = new SchoolDBEntities())
                {
                    ListStudent.Students.Add(new Student()
                    {
                        StudentID = student.Id,
                        StudentName = student.StudentName,
                        StandardId = student.StandardId
                     
                        
                    });


                    ListStudent.SaveChanges();
                }
            }
                return Ok();
            }

        
        [HttpPut]
      

        public IHttpActionResult UpdateStudentDetails(StudentViewModel student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad Request");
            }

            using(var ListStudent =new SchoolDBEntities())
            {
                var existingStudent = ListStudent.Students.Where(s => s.StudentID == student.StandardId).FirstOrDefault<Student>();

                if(existingStudent == null)
                {
                    return NotFound();

                }

                else
                {
                    existingStudent.StudentName = student.StudentName;
                    existingStudent.StandardId = student.StandardId;

                    ListStudent.SaveChanges();
                       
                }
            }

            return Ok();
        }

        [HttpDelete]

        public IHttpActionResult DeleteStudentDetails(int id)
        {
            if (id == 0)
            {
                return BadRequest( "invalid StudentId");
            }

            else
            {
                using(var ListStudent =new SchoolDBEntities())
                {
                    var student = ListStudent.Students.Where(s => s.StudentID == id).FirstOrDefault<Student>();

                    ListStudent.Entry(student).State = System.Data.Entity.EntityState.Deleted;
                    ListStudent.SaveChanges();

                    return Ok("Deletion Successuly");
                }

            }

            return Ok();
        }
       

    }
    }

