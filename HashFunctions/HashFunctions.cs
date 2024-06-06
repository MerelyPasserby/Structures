namespace HashFunctions
{
    public class HashFunctions
    {
        static int KeyByDivision(int k, int M)
        {
            return k % M;
        }
        static int KeyByMultiplication(int k, int M)
        {
            double a = (Math.Sqrt(5) - 1) / 2;
            return (int)(M * (k * a - (int)(k * a)));
        }
        static int KeyByString(string str, int M)
        {
            unchecked
            {
                int sum = 0;
                int i = 0;
                foreach (char c in str)
                    sum += c * 31 ^ i++;
                return sum % M;
            }
        }
    }
}