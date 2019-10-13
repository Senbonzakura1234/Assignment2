using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using Windows.Storage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace T1807EHello.Entity
{
    internal class MemberServiceImp : IMemberService
    {
        private const string ApiUrl = "https://2-dot-backup-server-003.appspot.com/_api/v2/members";
        private const string LoginUrl = "https://2-dot-backup-server-003.appspot.com/_api/v2/members/authentication";
        private const string InformationUrl = "https://2-dot-backup-server-003.appspot.com/_api/v2/members/information";
        
        public string Login(string email, string password)
        {
            try
            {
                var memberLogin = new MemberLogin
                {
                    email = email,
                    password = password
                };
                if (!ValidateLogin(memberLogin))
                {
                    throw new Exception("Login fails!");
                }
                var token = GetTokenFromApi(memberLogin);
                CreateTokenFile(token);
                return token;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
            
        }

        public Member LoginWithToken()
        {
            try
            {
                return GetInformation(ReadTokenFile());
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public Member Register(Member member)
        {
            try
            {
                var httpClient = new HttpClient();
                HttpContent content = new StringContent(JsonConvert.SerializeObject(member), Encoding.UTF8,
                    "application/json");
                var responseContent = httpClient.PostAsync(ApiUrl, content).Result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Member>(responseContent);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
            
        }
        

        private static void CreateTokenFile(string token)
        {
            var storageFolder = ApplicationData.Current.LocalFolder;
            var tokenFile = storageFolder.CreateFileAsync("token.txt", CreationCollisionOption.ReplaceExisting).GetAwaiter().GetResult();
            FileIO.WriteTextAsync(tokenFile, token).GetAwaiter().GetResult();
        }


        private Member GetInformation(string token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
            var responseContent = client.GetAsync(InformationUrl).Result.Content.ReadAsStringAsync().Result;
            return responseContent == null ? null : JsonConvert.DeserializeObject<Member>(responseContent);
        }
        private string ReadTokenFile()
        {
            const string fileName = "token.txt";
            var storageFolder = ApplicationData.Current.LocalFolder;
            var sampleFile = storageFolder.GetFileAsync(fileName).GetAwaiter().GetResult();
            Debug.WriteLine(sampleFile.Path);
            var token = FileIO.ReadTextAsync(sampleFile).GetAwaiter().GetResult();
            return token;
        }

        private string GetTokenFromApi(MemberLogin memberLogin)
        {

            var dataContent = new StringContent(JsonConvert.SerializeObject(memberLogin),
                Encoding.UTF8, "application/json");
            var client = new HttpClient();
            var responseContent = client.PostAsync(LoginUrl, dataContent).Result.Content.ReadAsStringAsync().Result;
            var jsonJObject = JObject.Parse(responseContent);
            if (jsonJObject["token"] == null)
            {
                throw new Exception("Login fails");
            }
            return jsonJObject["token"].ToString();
        }

        private bool ValidateLogin(MemberLogin memberLogin)
        {
            return true;
        }
    }
}
