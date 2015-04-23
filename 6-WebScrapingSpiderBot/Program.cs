using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace _6_WebScrapingSpiderBot
{
    class Program
    {
        // ## Extra challenges ##
        // 1. What would you do to enable this to output wordcounts by each set of links followed?
        // 2. The word count currently takes ALL text from an html file. What would you do to to only focus on the visible on-page text?
        // 3. The URL handling currently needs full http:// addresses to follow links. How could you do this differently to make it more robust?

        // DISCLAIMER: Remember, not all sites like to be scraped by robots. In some places it's probably even illegal. 
        // ALWAYS MAKE SURE YOU HAVE THE SITE OWNER'S PERMISSION BEFORE GOING OUT AND SCRAPING STUFF!!

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
                Console.WriteLine("Word Count for {0}:", webUrl);

                // Install the Html Agility Pack via NuGet for dealing with HTML (see the using statements aove)
                // Don't forget to install XPath for silverlight 4 (available via NuGet) as well!
                var handler = new HtmlWeb();
                
                // Load up the current web URL
                var doc = handler.Load(webUrl);
                var firstPageLinks = GetOnPageUrls(doc, 5);
                

                // Parse the content of the first page
                ScrapeWords(doc, ref wordCount);


                // Parse the content for each of the additional pages
                foreach (var link in firstPageLinks)
                {
                    var subHandler = new HtmlWeb();
                    var nextDoc = subHandler.Load(link);

                    ScrapeWords(nextDoc, ref wordCount);
                }

                // Output the results!
                foreach (var word in wordCount)
                {
                    Console.WriteLine("{0} appears {1} times", word.Key, word.Value);
                }

                // Can you guess what's missing?
                // Hint: Will this be counting words for each individual site, or is it grouping it all together.
                // How could you amend this?

            }

            Console.ReadLine();

        }


        /// <summary>
        /// Takes an HTML document, splits out the on page words and increments the dictionary counter for matching words.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="wordCount"></param>
        private static void ScrapeWords(HtmlDocument doc, ref Dictionary<string, int> wordCount)
        {
            var onpageText = doc.DocumentNode.InnerText;

            // Split the text on the page into individual words.
            foreach (var word in onpageText.Split(' '))
            {
                // See if the word exists in the word list
                if (!wordCount.ContainsKey(word)) continue;
                
                // Increase the word count by 1 if it does!
                wordCount[word]++;
            }
        }


        /// <summary>
        /// Parse an HTML document for all links and return the href destinations.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="numberOfLinks"></param>
        /// <returns></returns>
        private static IEnumerable<string> GetOnPageUrls(HtmlDocument doc, int numberOfLinks)
        {
            var counter = 0;
            var linksList = new List<string>();

            // Use XPath strings to search for <a> tags and their inner href attributes 
            // (i.e. <a href="/somelink.html">click</a> )
            // See http://www.w3schools.com/xpath/xpath_syntax.asp for XPath syntax
            foreach (var link in doc.DocumentNode.SelectNodes("//a"))
            {
                var htmlLink = link.GetAttributeValue("href", "");
                if (htmlLink == "" || htmlLink.Contains("index") || !htmlLink.Contains("http://")) continue;

                linksList.Add(htmlLink);
                counter++;

                if (counter >= numberOfLinks) break;    // What would be the downside of doing it this way?
            }

            return linksList;
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
