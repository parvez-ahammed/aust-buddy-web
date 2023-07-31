namespace AUST_BUDDY_WEB.Features.Course
{
	public class CourseUtility
	{
		public static string GetYearSemester(int semester)
		{
			int year = (semester + 1) / 2;
			int sem = (semester % 2 == 0) ? 2 : 1;
			string yearSem = "year" + year.ToString() + "semester" + sem.ToString();
			string path = "course-list/CSE/" + yearSem;
			return path;
		}

	}
}