using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Web;

namespace Tests3cr3tx
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is a test app for s3cr3tx!");
            Console.WriteLine("Please enter your email address: ");
            string strEmail = Console.ReadLine();
            Console.WriteLine("Please enter your APIToken: ");
            string strAPIToken = Console.ReadLine();
            Console.WriteLine("Please enter your Authorization Code: ");
            string strAuth = Console.ReadLine();
            Console.WriteLine("enter E to encrypt or D to decrypt: ");
            string strEoD = Console.ReadLine();
            Console.WriteLine("Please enter your input: ");
            string strInput = Console.ReadLine();
            string strResult = "";
            if (strAPIToken == strAuth)
            {
                // strResult = putResult(strEmail, strAPIToken, strAuth, "", "");
                postNewEmail(strEmail,strAPIToken, strAuth);
            }
            else
            {
                strResult = getResult(strEmail, strAPIToken, strAuth, strEoD, strInput);
            }
            Console.WriteLine(@"Your output is:");
            Console.WriteLine(strResult);


        }

        private static string getResult(string Email, string strToken, string strAuth, string strEoD, string strInput)
        {
            try
            {
                //HttpClient client = new HttpClient();
                //client.BaseAddress = new Uri(@"https://localhost:7191/Values");

                WebClient wc = new WebClient();
                //wc.Credentials.GetCredential();
                wc.BaseAddress = @"https://localhost:7191/Values";
                WebHeaderCollection webHeader = new WebHeaderCollection();
                webHeader.Add(@"Email:" + Email);
                webHeader.Add(@"AuthCode:" + strAuth);
                webHeader.Add(@"APIToken:" + strToken);
                webHeader.Add(@"Input:" + strInput);
                webHeader.Add(@"EorD:" + strEoD);
                webHeader.Add(@"Def:" + @"z");
                wc.Headers = webHeader;
                string result = wc.DownloadString(@"https://localhost:7191/Values");
                //string result = wc.UploadValues()
                return result;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetBaseException().ToString());
                Console.ReadLine();
                return ex.GetBaseException().ToString();
            }
        }
        private static HttpClient sharedClient = new()
        {
            BaseAddress = new Uri("https://localhost:7191/Values"),
        };
        private static async Task postNewEmail(string Email, string strToken, string strAuth)
        {
           try
            {
                //build JSON string
                NewK nk = new NewK();
                    nk.name = Email;
                nk.pd = strToken;
                nk.pd2 = strAuth;
                string strNk = JsonSerializer.Serialize<NewK>(nk);
                StringContent jsonContent = new(strNk);
                
                using HttpResponseMessage response = await sharedClient.PostAsync(
                    @"https://localhost:7191/Values",
                    jsonContent);

                var jsonResponse = await response.Content.ReadAsStringAsync();

                Console.WriteLine(jsonResponse.ToString());
            }
           catch (Exception ex)
            {
                Console.WriteLine(ex.GetBaseException().ToString());
                Console.ReadLine();
                throw(ex.GetBaseException());
            }
        }
            private static string postResult(string Email, string strToken, string strAuth, string strEoD, string strInput)
            {
                try
            {
                //HttpClient client = new HttpClient();
                //client.BaseAddress = new Uri(@"https://localhost:7191/Values");

                WebClient wc = new WebClient();
                //wc.Credentials.GetCredential();
                wc.BaseAddress = @"https://localhost:7191/Values";
                WebHeaderCollection webHeader = new WebHeaderCollection();
                webHeader.Add(@"Email:" + Email);
                webHeader.Add(@"AuthCode:" + strAuth);
                webHeader.Add(@"APIToken:" + strToken);
                webHeader.Add(@"Input:" + strInput);
                webHeader.Add(@"EorD:" + strEoD);
                webHeader.Add(@"Def:" + @"z");
                wc.Headers = webHeader;
                //string result = wc.DownloadString(@"https://localhost:7191/Values");
                NameValueCollection nameValue = new NameValueCollection();
                nameValue.Set("name", Email);
                nameValue.Set("pwd", strToken);
                nameValue.Set("pwd2", strAuth);
                byte[] result = wc.UploadValues(@"https://localhost:7191/Values", "PUT", nameValue);

                return result.ToString();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetBaseException().ToString());
                Console.ReadLine();
                return ex.GetBaseException().ToString();
            }
        }
        private static string forceCreate(string strEmail)
        {
            string strResult = @"";
            string strUserEmail = strEmail;
            //string strUserEmail = data["name"];
            //string strPw = data["pwd"];
            string strPw = "28A232D6-4E35-4DDF-88B0-5AC64678FD2E";
            string strPw2 = "28A232D6-4E35-4DDF-88B0-5AC64678FD2E";
            //string strPw2 = data["pwd2"];
            string strMpM = @"";
            if (strPw == strPw2)
            {
                strMpM = strPw;
            }
            //                You will use ajax to send the following formData

            // let formData: FormData;
            //                formData = new FormData();
            //                formData.append('imageFile', imageFile);
            //                formData.append('name', name);
            //                Then you will receive it in your controller like this:

            //public string Post(IFormCollection data, IFormFile imageFile)
            //Then you will access the data as you do normally:

            //            var name = data["name"];
            //strUserEmail = Guid.NewGuid().ToString();
            if (strUserEmail != @"")// && IsValidEmail(strUserEmail) && (strMpM != @""))
            {
                //do stuff
                //Authorize user
                int intAuthResult = Bundle.Authorize(strUserEmail);
                //create keys and tokens and API-Key and Authorization Header
                string strGUID1 = Guid.NewGuid().ToString();
                string strGUID2 = Guid.NewGuid().ToString();
                string strKey1Pri = @"";
                string strKey2Pri = @"";
                string strKey1Pub = @"";
                string strKey2Pub = @"";
                string strAuthorizationCode = @"";
                string strAPIKey = @"";
                using (RSACryptoServiceProvider rSA = new RSACryptoServiceProvider(4096))
                {
                    strKey1Pub = System.Convert.ToBase64String(Encoding.Convert(Encoding.GetEncoding(0), Encoding.UTF8, Encoding.GetEncoding(0).GetBytes(rSA.ToXmlString(false))));
                    strKey1Pri = System.Convert.ToBase64String(Encoding.Convert(Encoding.GetEncoding(0), Encoding.UTF8, Encoding.GetEncoding(0).GetBytes(rSA.ToXmlString(true))));
                }
                //using (RSACryptoServiceProvider rSA1 = new RSACryptoServiceProvider(4096))
                //{
                //    strKey2Pub = System.Convert.ToBase64String(Encoding.Convert(Encoding.GetEncoding(0), Encoding.UTF8, Encoding.GetEncoding(0).GetBytes(rSA1.ToXmlString(false))));
                //    strKey2Pri = System.Convert.ToBase64String(Encoding.Convert(Encoding.GetEncoding(0), Encoding.UTF8, Encoding.GetEncoding(0).GetBytes(rSA1.ToXmlString(true))));
                //}
                string strMpMhash = @"";
                using (SHA512 sHA512 = SHA512.Create())
                {
                    byte[] bytHashUserGuid1Guid2mPm = sHA512.ComputeHash(Encoding.Convert(Encoding.GetEncoding(0), Encoding.UTF8, Encoding.GetEncoding(0).GetBytes(strGUID1 + strGUID2 + strUserEmail + strMpM)));
                    byte[] bytHashUsernameGUID2 = sHA512.ComputeHash(Encoding.Convert(Encoding.GetEncoding(0), Encoding.UTF8, Encoding.GetEncoding(0).GetBytes(strGUID2 + strUserEmail)));
                    byte[] bytHashGUID1 = sHA512.ComputeHash(Encoding.Convert(Encoding.GetEncoding(0), Encoding.UTF8, Encoding.GetEncoding(0).GetBytes(strGUID1)));
                    strAuthorizationCode = System.Convert.ToBase64String(Encoding.Convert(Encoding.GetEncoding(0), Encoding.UTF8, bytHashUsernameGUID2));
                    strAPIKey = System.Convert.ToBase64String(Encoding.Convert(Encoding.GetEncoding(0), Encoding.UTF8, bytHashGUID1));
                    strMpMhash = System.Convert.ToBase64String(Encoding.Convert(Encoding.GetEncoding(0), Encoding.UTF8, bytHashUserGuid1Guid2mPm));
                }
                Bundle bundleCurrent = new Bundle(strUserEmail, strAPIKey, strAuthorizationCode, strGUID1, strGUID2, strKey1Pub, strKey1Pri, strMpMhash);
                int didItWork = Bundle.storeBundle(bundleCurrent);
                return didItWork.ToString();
            }
                    return @"something went wrong";
        }
    }
 
public class NewK
{
    public string name { get; set; }
    public string pd { get; set; }
    public string pd2 { get; set; }
}
public class Bundle
    {
        public Bundle() { }
        public string Email { get; set; }
        public string APIKey { get; set; }
        public string Authorization { get; set; }
        public string Guid1 { get; set; }
        public string Guid2 { get; set; }

        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
        public string mPmHash { get; set; }

        public Bundle(string strEmail, string strAPIKey, string strAuthorizationCode, string strGuid1, string strGuid2, string strKeyPub, string strKeyPri, string strMpMhash)
        {
            Email = strEmail;
            APIKey = strAPIKey;
            Authorization = strAuthorizationCode;
            Guid1 = strGuid1;
            Guid2 = strGuid2;
            PrivateKey = strKeyPri;
            PublicKey = strKeyPub;
            mPmHash = strMpMhash;
        }
        public static int storeBundle(Bundle bundleCurrent)
        {
            try
            {
                bool result = false;
                //SQLConnection, Command, ExecuteNonQuery
                SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=s3cr3tx;Integrated Security=SSPI");
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"dbo.InsertM";
                SqlParameter p1 = new SqlParameter(@"Guid1", bundleCurrent.Guid1);
                SqlParameter p2 = new SqlParameter(@"Guid2", bundleCurrent.Guid2);
                SqlParameter p3 = new SqlParameter(@"Email", bundleCurrent.Email);
                SqlParameter p4 = new SqlParameter(@"APIKey", bundleCurrent.APIKey);
                SqlParameter p5 = new SqlParameter(@"Authorization", bundleCurrent.Authorization);
                SqlParameter p6 = new SqlParameter(@"PrivateKey", bundleCurrent.PrivateKey);
                SqlParameter p7 = new SqlParameter(@"PublicKey", bundleCurrent.PublicKey);
                SqlParameter p8 = new SqlParameter(@"PmHash", bundleCurrent.PublicKey);
                command.Parameters.Add(p1);
                command.Parameters.Add(p2);
                command.Parameters.Add(p3);
                command.Parameters.Add(p4);
                command.Parameters.Add(p5);
                command.Parameters.Add(p6);
                command.Parameters.Add(p7);
                command.Parameters.Add(p8);
                con.Open();
                command.Connection = con;
                int intResult = command.ExecuteNonQuery();
                if (intResult.Equals(1))
                {
                    result = true;
                }

                return intResult;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.GetBaseException().ToString());

                SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=s3cr3tx;Integrated Security=SSPI");
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"dbo.insertLog";
                SqlParameter p1 = new SqlParameter(@"Source", @"s3cr3tx_StoreBundle");
                SqlParameter p2 = new SqlParameter(@"logMessage", ex.GetBaseException().ToString());
                command.Parameters.Add(p1);
                command.Parameters.Add(p2);
                con.Open();
                command.Connection = con;
                int intResult = command.ExecuteNonQuery();
                return -7;

            }
        }
        public static int Authorize(string Email)
        {
            try
            {
                if (IsValidEmail(Email))
                {
                    string strGuid = Guid.NewGuid().ToString();
                    string strCreated = @"ADMIN";
                    bool IsValid = true;
                    SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=s3cr3tx;Integrated Security=SSPI");
                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = @"dbo.InsertAuth";
                    SqlParameter p1 = new SqlParameter(@"IsValid", IsValid);
                    SqlParameter p2 = new SqlParameter(@"CreatedBY", strCreated);
                    SqlParameter p3 = new SqlParameter(@"Email", Email);
                    SqlParameter p4 = new SqlParameter(@"AuthCode", strGuid);
                    command.Parameters.Add(p1);
                    command.Parameters.Add(p2);
                    command.Parameters.Add(p3);
                    command.Parameters.Add(p4);
                    con.Open();
                    command.Connection = con;
                    int intResult = command.ExecuteNonQuery();
                    return intResult;
                }
                else
                {

                    return -5;
                }

            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.GetBaseException().ToString());
                SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=s3cr3tx;Integrated Security=SSPI");
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"dbo.insertLog";
                SqlParameter p1 = new SqlParameter(@"Source", @"CreateKeysPKs3cr3tx-Bundle_Authorize");
                SqlParameter p2 = new SqlParameter(@"Message", ex.GetBaseException().ToString());
                command.Parameters.Add(p1);
                command.Parameters.Add(p2);
                con.Open();
                command.Connection = con;
                int intResult = command.ExecuteNonQuery();

                return -7;
            }
        }
        public static int Authorize2(string Email)
        {
            try
            {
                if (true)//IsValidEmail(Email))
                {
                    string strGuid = Guid.NewGuid().ToString();
                    string strCreated = @"ADMIN";
                    bool IsValid = true;
                    SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=s3cr3tx;Integrated Security=SSPI");
                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = @"dbo.InsertAuth";
                    SqlParameter p1 = new SqlParameter(@"IsValid", IsValid);
                    SqlParameter p2 = new SqlParameter(@"CreatedBY", strCreated);
                    SqlParameter p3 = new SqlParameter(@"Email", Email);
                    SqlParameter p4 = new SqlParameter(@"AuthCode", strGuid);
                    command.Parameters.Add(p1);
                    command.Parameters.Add(p2);
                    command.Parameters.Add(p3);
                    command.Parameters.Add(p4);
                    con.Open();
                    command.Connection = con;
                    int intResult = command.ExecuteNonQuery();
                    return intResult;
                }
                else
                {

                    return -5;
                }

            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.GetBaseException().ToString());
                SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=s3cr3tx;Integrated Security=SSPI");
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"dbo.insertLog";
                SqlParameter p1 = new SqlParameter(@"Source", @"CreateKeysPKs3cr3tx-Bundle_Authorize");
                SqlParameter p2 = new SqlParameter(@"Message", ex.GetBaseException().ToString());
                command.Parameters.Add(p1);
                command.Parameters.Add(p2);
                con.Open();
                command.Connection = con;
                int intResult = command.ExecuteNonQuery();

                return -7;
            }
        }
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=s3cr3tx;Integrated Security=SSPI");
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"dbo.insertLog";
                SqlParameter p1 = new SqlParameter(@"Source", @"s3cr3tx_Console_IsValidEmailRegexMatchTimeout");
                SqlParameter p2 = new SqlParameter(@"Message", e.GetBaseException().ToString());
                command.Parameters.Add(p1);
                command.Parameters.Add(p2);
                con.Open();
                command.Connection = con;
                int intResult = command.ExecuteNonQuery();
                return false;
            }
            catch (ArgumentException e)
            {
                SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=s3cr3tx;Integrated Security=SSPI");
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"dbo.InsertM";
                SqlParameter p1 = new SqlParameter(@"Source", @"s3cr3tx_Console_IsValidEmailArgumentException");
                SqlParameter p2 = new SqlParameter(@"Message", e.GetBaseException().ToString());
                command.Parameters.Add(p1);
                command.Parameters.Add(p2);
                con.Open();
                command.Connection = con;
                int intResult = command.ExecuteNonQuery();
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException e)
            {
                SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=s3cr3tx;Integrated Security=SSPI");
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"dbo.insertLog";
                SqlParameter p1 = new SqlParameter(@"Source", @"s3cr3tx_Console_IsValidEmail-RegexMatchTimeout");
                SqlParameter p2 = new SqlParameter(@"Message", e.GetBaseException().ToString());
                command.Parameters.Add(p1);
                command.Parameters.Add(p2);
                con.Open();
                command.Connection = con;
                int intResult = command.ExecuteNonQuery();
                return false;
            }
        }
        public static Bundle GetBundle(string Email, string APIKey, string AuthCode)
        {
            try
            {
                Bundle bundleCurrent = new Bundle();
                if (IsValidEmail(Email))
                {
                    SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=s3cr3tx;Integrated Security=SSPI");
                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = @"dbo.USP_GetBundle_M";
                    SqlParameter p1 = new SqlParameter(@"mAuthorization", AuthCode);
                    SqlParameter p2 = new SqlParameter(@"mAPIKey", APIKey);
                    SqlParameter p3 = new SqlParameter(@"mEmail", Email);
                    command.Parameters.Add(p1);
                    command.Parameters.Add(p2);
                    command.Parameters.Add(p3);
                    con.Open();
                    command.Connection = con;
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(ds);
                    //Bundle bundleCurrent = new Bundle();
                    bundleCurrent.Guid1 = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                    bundleCurrent.Guid2 = ds.Tables[0].Rows[0].ItemArray[2].ToString();
                    bundleCurrent.Email = ds.Tables[0].Rows[0].ItemArray[3].ToString();
                    bundleCurrent.APIKey = ds.Tables[0].Rows[0].ItemArray[4].ToString();
                    bundleCurrent.Authorization = ds.Tables[0].Rows[0].ItemArray[5].ToString();
                    bundleCurrent.PrivateKey = ds.Tables[0].Rows[0].ItemArray[6].ToString();
                    bundleCurrent.PublicKey = ds.Tables[0].Rows[0].ItemArray[7].ToString();

                }
                return bundleCurrent;
            }
            catch (Exception ex)
            {
                Bundle bunEmpty = new Bundle();
                //Console.WriteLine(@"Error: " + ex.GetBaseException().ToString());
                SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=s3cr3tx;Integrated Security=SSPI");
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"dbo.insertLog";
                SqlParameter p1 = new SqlParameter(@"Source", @"s3cr3txConsole_GetBundle");
                SqlParameter p2 = new SqlParameter(@"Message", ex.GetBaseException().ToString());
                command.Parameters.Add(p1);
                command.Parameters.Add(p2);
                con.Open();
                command.Connection = con;
                int intResult = command.ExecuteNonQuery();
                return bunEmpty;
            }
        }
    }
}
