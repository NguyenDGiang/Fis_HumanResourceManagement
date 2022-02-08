namespace HRM.Services.MEmployee
{
    public class EmployeeMessage
    {
        public enum Error
        {
            IdNotExisted,
            EmployeeInUsed,
            EmployeeNotInUsed,
            NameEmpty,
            NameNotEmpty,
            NameOverLength,
            EmployeeNotExisted



        }
    }
}
