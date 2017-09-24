using System.Collections.Generic;

namespace CoursesAPI.Models
{
	public class Envelope<T>
	{
        /// <summary>
        /// Courses in a given semester
        /// </summary>
        /// <returns></returns>
		public IEnumerable<T> Items { get; set; }

        /// <summary>
        /// Current size of each page
        /// </summary>
        /// <returns></returns>
		public int PageSize { get; set; }

        /// <summary>
        /// Current visible page
        /// </summary>
        /// <returns></returns>
		public int CurrentPage { get; set; }

        /// <summary>
        /// Total number of pages
        /// </summary>
        /// <returns></returns>
		public int TotalPages { get; set; }

        /// <summary>
        /// Total number of items in the course
        /// </summary>
        /// <returns></returns>
        public int TotalItems { get; set; }
	}
}