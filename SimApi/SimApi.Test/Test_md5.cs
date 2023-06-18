namespace SimApi.Test;


public class Md5Test
{
   
    
    [Test]
    public void Test1()
    {
        string text = "hello";
        string hash = "5d41402abc4b2a76b9719d911017c592";
        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(text);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            var result =  Convert.ToHexString(hashBytes).ToLower();

            Assert.AreEqual(hash, result);
        }

    }
}