using CleanLearning.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace CleanLearning.Controllers
{
    public class GitHubApiController : Controller
    {
        public List<Language> languages = new List<Language>();
        private michiyf248_clEntities db = new michiyf248_clEntities();
        public ActionResult Experience(int teacherID)
        {
            List<string> tempurl = new List<string>();
            List<string> urls = new List<string>();
            Teacher teacher = db.Teacher.Find(teacherID);
            string githubName = "";
            if (teacher.GithubUser != null)
            {
                githubName = teacher.GithubUser;
            }
            HttpWebRequest githubRequest = (HttpWebRequest)System.Net.WebRequest.Create("https://api.github.com/users/" + githubName + "/repos");
            githubRequest.UseDefaultCredentials = true;
            githubRequest.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            githubRequest.UserAgent = @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106 Safari/537.36";
            githubRequest.Headers.Add("Authorization", string.Format("Token {0}", "cd2419a5a09301feea93a33a079ddac288160a5b"));
            githubRequest.Accept = "application/json";
            try
            {
                WebResponse responseGithubRepos = githubRequest.GetResponse();
                using (StreamReader sr1 = new StreamReader(responseGithubRepos.GetResponseStream()))
                {
                    string _line;
                    while ((_line = sr1.ReadLine()) != null)
                    {
                        string[] splitted = _line.Split(':');
                        for (int i = 0; i < splitted.Length; i++)
                        {
                            if (splitted[i].Contains("full_name"))
                            {
                                tempurl.Add(splitted[i + 1]);
                            }
                        }
                    }
                }
            }
            catch
            { 
            }
            foreach (string s in tempurl)
            {
                string temp = s.Replace("\"", "");
                string temp2 = temp.Replace(" ", "");
                string[] temp1 = temp2.Split(',');
                urls.Add(temp1[0]);
            }
            foreach (string url in urls)
            {
                HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create("https://api.github.com/repos/" + url + "/languages");
                request.UseDefaultCredentials = true;
                request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
                request.UserAgent = @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106 Safari/537.36";
                request.Accept = "application/json";
                request.Headers.Add("Authorization", string.Format("Token {0}", "cd2419a5a09301feea93a33a079ddac288160a5b"));
                WebResponse response = request.GetResponse();
                //StreamReader reader = new StreamReader(response.GetResponseStream());
                //string Joke_JSON = reader.ReadToEnd();
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    string _line;
                    while ((_line = sr.ReadLine()) != null)
                    {
                        char[] chars = { '{', '}' };
                        string resultSring = _line.Trim(chars);
                        resultSring = resultSring.Replace("\"", "");
                        string[] rows = resultSring.Split(',');
                        foreach (string row in rows)
                        {
                            string[] keyValue = row.Split(':');
                            Language find = languages.Find(x => x.language == keyValue[0]);
                            if (find == null)
                            {
                                if (keyValue.Length == 2)
                                {
                                    Language lang = new Language();
                                    lang.language = keyValue[0];
                                    lang.numOfRows = Int32.Parse(keyValue[1]);
                                    languages.Add(lang);
                                }
                            }
                            else
                            {
                                find.numOfRows += Int32.Parse(keyValue[1]);
                            }
                        }

                    }
                }
            }
            List<Language> SortedList = languages.OrderByDescending(l => l.numOfRows).ToList();
            return View(SortedList);
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}