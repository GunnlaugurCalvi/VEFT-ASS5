using System.Collections.Generic;
using System.Linq;
using CoursesAPI.Models;
using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Exceptions;
using CoursesAPI.Services.Models.Entities;
using System;

namespace CoursesAPI.Services.CoursesServices
{
    public class CoursesServiceProvider
    {
        private readonly IUnitOfWork _uow;

        private readonly IRepository<CourseInstance> _courseInstances;
        private readonly IRepository<TeacherRegistration> _teacherRegistrations;
        private readonly IRepository<CourseTemplate> _courseTemplates;
        private readonly IRepository<Person> _persons;

        public CoursesServiceProvider(IUnitOfWork uow)
        {
            _uow = uow;

            _courseInstances = _uow.GetRepository<CourseInstance>();
            _courseTemplates = _uow.GetRepository<CourseTemplate>();
            _teacherRegistrations = _uow.GetRepository<TeacherRegistration>();
            _persons = _uow.GetRepository<Person>();
        }

        /// <summary>
        /// You should implement this function, such that all tests will pass.
        /// </summary>
        /// <param name="courseInstanceID">The ID of the course instance which the teacher will be registered to.</param>
        /// <param name="model">The data which indicates which person should be added as a teacher, and in what role.</param>
        /// <returns>Should return basic information about the person.</returns>
        public PersonDTO AddTeacherToCourse(int courseInstanceID, AddTeacherViewModel model)
        {
            // TODO: implement this logic!
            return null;
        }

        /// <summary>
        /// You should write tests for this function. You will also need to
        /// modify it, such that it will correctly return the name of the main
        /// teacher of each course.
        /// </summary>
        /// <param name="semester"></param>
        /// <returns></returns>
        public Envelope<CourseInstanceDTO> GetCourseInstancesBySemester(string semester = null, string lang = null, int pageNumber = 1)
        {
            if (string.IsNullOrEmpty(semester))
            {
                semester = "20153";
            }

            var pageSize = 2;
            var allCourses = (from c in _courseInstances.All()
                                join ct in _courseTemplates.All() on c.CourseID equals ct.CourseID
                                where c.SemesterID == semester
                                select c).ToList();
            var maxPages = (int)Math.Ceiling(allCourses.Count / (double)pageSize);

            if(lang == "en-US, en; q=0.8, is; q=0.6")
            {
                var Items = (from c in _courseInstances.All()
                            join ct in _courseTemplates.All() on c.CourseID equals ct.CourseID
                            where c.SemesterID == semester
                            select new CourseInstanceDTO
                            {
                                Name = ct.Name_EN   ,
                                TemplateID = ct.CourseID,
                                CourseInstanceID = c.ID,
                                MainTeacher = ""
                            }).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                            

                return new Envelope<CourseInstanceDTO> 
                {
                    Items = Items,
                    TotalPages = maxPages,
                    PageSize = pageSize,
                    CurrentPage = pageNumber,
                    TotalItems = allCourses.Count
                };
            }else
            {
                var Items = (from c in _courseInstances.All()
                            join ct in _courseTemplates.All() on c.CourseID equals ct.CourseID
                            where c.SemesterID == semester
                            select new CourseInstanceDTO
                            {
                                Name = ct.Name,
                                TemplateID = ct.CourseID,
                                CourseInstanceID = c.ID,
                                MainTeacher = ""
                            }).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                return new Envelope<CourseInstanceDTO> 
                {
                    Items = Items,
                    TotalPages = maxPages,
                    PageSize = pageSize,
                    CurrentPage = pageNumber,
                    TotalItems = allCourses.Count
                };
            }
        }
    }
}
