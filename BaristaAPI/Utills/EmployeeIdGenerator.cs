namespace BaristaAPI.Utills
{
    public class EmployeeIdGenerator
    {
        private static Random random = new Random();

        public static string GenerateEmployeeId()
        {
            const string prefix = "UI";
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var randomChars = new char[7];

            for (int i = 0; i < randomChars.Length; i++)
            {
                randomChars[i] = chars[random.Next(chars.Length)];
            }

            return prefix + new string(randomChars);
        }
    }
}
