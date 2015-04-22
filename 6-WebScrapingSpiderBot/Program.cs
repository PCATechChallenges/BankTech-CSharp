using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace _6_WebScrapingSpiderBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var webUrlsDoc = "WebsiteURLs.txt";
            var wordsListDoc = "WordsList.txt";

            // Load up the words to search for and the sites to search within first.
            var wordsList = File.ReadLines(wordsListDoc);
            var webUrls = File.ReadLines(webUrlsDoc);

            // Create a dictionary to store our words and future word counts
            var wordCount = InitialiseWordDictionary(wordsList);


            // Cycle through each url within the list and grab the HTML behind each page.
            foreach (var webUrl in webUrls)
            {
                Console.WriteLine("{0}");

                // Install the Html Agility Pack via NuGet for dealing with HTML (see the using statements aove)
                // Don't forget to install XPath for silverlight 4 (available via NuGet) as well!
                var handler = new HtmlWeb();
                
                // Load up the current web URL
                var doc = handler.Load(webUrl);

                // Use XPath strings to search for <a> tags and their inner href attributes 
                // (i.e. <a href="/somelink.html">click</a> )
                // See http://www.w3schools.com/xpath/xpath_syntax.asp for XPath syntax
                var counter = 0;
                foreach (var link in doc.DocumentNode.SelectNodes("//a"))
                {
                    var htmlLink = link.GetAttributeValue("href", "");
                    if(htmlLink == "") continue;

                    Console.WriteLine(htmlLink);
                    counter++;
                    if(counter > 6) break;

                    //convert to pages
                }

                //add word counter
                
            }

            Console.ReadLine();

        }


        /// <summary>
        /// Populates a new dictionary from a list of words and sets their corresponding values all to zero.
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        private static Dictionary<string, int> InitialiseWordDictionary(IEnumerable<string> words)
        {
            var seedDictionary = new Dictionary<string, int>();
            
            foreach (var word in words)
            {
                // Don't forget to trim and lowercase everything to make them uniform.
                var cleanWord = word.ToLower().Trim();

                // We also want to avoid any duplicates
                if(!seedDictionary.ContainsKey(cleanWord)) 
                    seedDictionary.Add(word.ToLower().Trim(), 0);    
            }

            return seedDictionary;
        }
    }
}
