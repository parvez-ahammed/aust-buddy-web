namespace AUST_BUDDY_WEB.Models
{

	public class Course
	{
		public string CourseCode { get; set; }
		public string CourseName { get; set; }
		public string DriveLink { get; set; }
		public string CoursePath { get; set; }
	}
	public class Admin
	{
		public string Department { get; set; }
		public string Year { get; set; }
		public string Semester { get; set; }
		public string Email { get; set; }
	}

	public class Announcement
	{
		public string UserId { get; set; }
		public string ProductName { get; set; }
		public string ProductAuthor { get; set; }
		public string ProductCategory { get; set; }
		public string ProductPrice { get; set; }
		public string SellersContactNo { get; set; }
		public string SellersDetails { get; set; }
		public string ProductId { get; set; }
		public string ProductDetails { get; set; }
	}

	public class BugReport
	{
		public string UserId { get; set; }
		public string ReportersDetails { get; set; }
		public string ReportDetails { get; set; }
		public string Key { get; set; }
	}

	public class Group
	{
		public string UserId { get; set; }
		public string UniversityId { get; set; }
		public string GroupName { get; set; }
		public string GroupDetails { get; set; }
		public string Path { get; set; }
		public string GroupId { get; set; }
	}

	public class GroupNotice
	{
		public string Uid { get; set; }
		public string TaskName { get; set; }
		public string TaskDescription { get; set; }
		public string TaskDate { get; set; }
		public string Path { get; set; }
		public string GroupId { get; set; }
	}

	public class Material
	{
		public string UserId { get; set; }
		public string ProductName { get; set; }
		public string ProductAuthor { get; set; }
		public string ProductCategory { get; set; }
		public string ProductPrice { get; set; }
		public string SellersContactNo { get; set; }
		public string SellersDetails { get; set; }
		public string ProductId { get; set; }
		public string ProductDetails { get; set; }
	}

	public class Routine
	{
		public string Image { get; set; }
		public string Path { get; set; }
	}

	public class Teacher
	{
		public string Name { get; set; }
		public string Img { get; set; }
		public string Phone { get; set; }
		public string Designation { get; set; }
		public string Email { get; set; }
	}

	public class UserAll
	{
		public string UserName { get; set; }
		public string UserEmail { get; set; }
		public string UserStudentId { get; set; }
		public string UserSession { get; set; }
		public string UserDepartment { get; set; }
		public string UserSemester { get; set; }
		public string UserSection { get; set; }
	}

	public class User
	{
		public string Username { get; set; }
		public string Email { get; set; }
	}

	public class YouTubeVideo
	{
		public string VideoId { get; set; }
		public string Title { get; set; }
		public string Thumbnail { get; set; }
	}

}