using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;
//using Newtonsoft.Json;

using LAB03;

class Program
{
    static void Main()
    {
        List<Tweet> tweets = LoadTweets("data.jsonl");


    }

    static List<Tweet> LoadTweets(string file)
    {
        List<Tweet> tweets = new List<Tweet>();
        foreach(var line in File.ReadLines(file))
        {
            Tweet ?tweet = JsonSerializer.Deserialize<Tweet>(line);
            tweets.Add(tweet);
        }
        return tweets;
    }

    static void SaveTweetsToXml(List<Tweet> tweets, string file)
    {
        System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<Tweet>));
        using (StreamWriter writer = new StreamWriter(file))
        {
            serializer.Serialize(writer, tweets);
        }
    }

    static List<Tweet> LoadTweetsFromXML(string file)
    {
        List<Tweet> tweets = new List<Tweet>();
        System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<Tweet>));
        using (StreamReader reader = new StreamReader(file))
        {
            tweets = (List<Tweet>)serializer.Deserialize(reader);
        }

        return tweets;
    }
    
    static List<Tweet> SortTweetsByUsername(List<Tweet> tweets)
    {
        return tweets.OrderBy(tweet => tweet.UserName).ToList();
    }
    
    static Dictionary<string, List<Tweet>> SortUsersByTweetDate(List<Tweet> tweets)
    {
        Dictionary<string, List<Tweet>> usersTweets = new Dictionary<string, List<Tweet>>();
        foreach (var tweet in tweets)
        {
            if (!usersTweets.ContainsKey(tweet.UserName))
            {
                usersTweets[tweet.UserName] = new List<Tweet>();
            }
            usersTweets[tweet.UserName].Add(tweet);
        }
        foreach (var userTweets in usersTweets)
        {
            userTweets.Value.Sort((tweet1, tweet2) => DateTime.Compare(DateTime.Parse(tweet1.Date), DateTime.Parse(tweet2.Date)));
        }
        return usersTweets.OrderBy(kv => kv.Value.First().Date).ToDictionary(kv => kv.Key, kv => kv.Value);
    }

    static (Tweet, Tweet) FindLatestAndOldestTweet(List<Tweet> tweets)
    {
        Tweet latestTweet = tweets.OrderByDescending(tweet => DateTime.Parse(tweet.Date)).First();
        Tweet oldestTweet = tweets.OrderBy(tweet => DateTime.Parse(tweet.Date)).First();
        return (latestTweet, oldestTweet);
    }

    static Dictionary<string, List<Tweet>> CreateDictionaryOfTweets(List<Tweet> tweets)
    {
        Dictionary<string, List<Tweet>> userDictionary = new Dictionary<string, List<Tweet>>();
        foreach(var tweet in tweets)
        {
            if (!userDictionary.ContainsKey(tweet.UserName))
            {
                userDictionary[tweet.UserName] = new List<Tweet>();
            }
            userDictionary[tweet.UserName].Add(tweet);
        }

        return userDictionary;
    }
    
    static Dictionary<string, int> CalculateWordFrequency(List<Tweet> tweets)
    {
        Dictionary<string, int> wordFrequency = new Dictionary<string, int>();
        foreach (Tweet tweet in tweets)
        {
            string[] words = tweet.Text.Split(new char[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {
                if (wordFrequency.ContainsKey(word))
                    wordFrequency[word]++;
                else
                    wordFrequency[word] = 1;
            }
        }
        return wordFrequency;
    }
}